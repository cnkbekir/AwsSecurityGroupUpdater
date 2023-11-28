using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Microsoft.Extensions.Configuration;

class Program
{
    static async Task Main()
    {
        // Get IP address
        string publicIp;
        using (var client = new HttpClient())
        {
            publicIp = await client.GetStringAsync("http://checkip.amazonaws.com/");
        }

        Console.WriteLine("Your IP Address: " + publicIp);

        // Build configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Get AWS settings
        var awsConfig = configuration.GetSection("AWS");
        var accessKeyId = awsConfig["AccessKeyId"];
        var secretAccessKey = awsConfig["SecretAccessKey"];
        
        var region = awsConfig["Region"];
        var regionEndpoint = RegionEndpoint.GetBySystemName(region);
        
        var securityGroupId = awsConfig["SecurityGroupId"];

        string userDescription = Environment.MachineName; // use machine name as description
        
        // Get Port settings
        var portsConfig = configuration.GetSection("Ports");
        int fromPort  = Convert.ToInt32(portsConfig["FromPort"]);
        int toPort = Convert.ToInt32(portsConfig["ToPort"]);
        
        using (var ec2Client = new AmazonEC2Client(accessKeyId, secretAccessKey, regionEndpoint))
        {
            try
            {
                // Get security groups
                var describeResponse = await ec2Client.DescribeSecurityGroupsAsync(new DescribeSecurityGroupsRequest
                {
                    GroupIds = new List<string> { securityGroupId }
                });

                var group = describeResponse.SecurityGroups[0];

                // Find user existing rule
                var existingRule = group.IpPermissions.FirstOrDefault(p =>
                    p.Ipv4Ranges.Any(ipRange => ipRange.Description == userDescription));

                if (existingRule != null)
                {
                    var revokeRequest = new RevokeSecurityGroupIngressRequest
                    {
                        GroupId = securityGroupId,
                        IpPermissions = new List<IpPermission> { existingRule }
                    };

                    await ec2Client.RevokeSecurityGroupIngressAsync(revokeRequest);
                }
                
                var ingressRequest = new AuthorizeSecurityGroupIngressRequest
                {
                    GroupId = securityGroupId,
                    IpPermissions = new List<IpPermission>
                    {
                        new()
                        {
                            IpProtocol = "tcp",
                            FromPort = fromPort,
                            ToPort = toPort,
                            Ipv4Ranges = new List<IpRange>
                            {
                                new() { CidrIp = $"{publicIp.Trim()}/32", Description = userDescription }
                            }
                        }
                    }
                };

                await ec2Client.AuthorizeSecurityGroupIngressAsync(ingressRequest);

                Console.WriteLine("Successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}