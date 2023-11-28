# AWS Security Group Updater

This project is used to update the user's IP address in an AWS Security Group. The application retrieves the current IP address of the user and sets it as a rule in the AWS EC2 Security Group for a specific port range.

## Setup

Before using the project, complete the following steps:

1. **Install and Configure AWS CLI**:
   Ensure that the AWS CLI is installed and your AWS credentials (`AWS_ACCESS_KEY_ID`, `AWS_SECRET_ACCESS_KEY`, and `AWS_REGION`) are configured.

2. **Install .NET Runtime**:
   Install the .NET runtime required to run the application. The application uses .NET Core 3.1 or a newer version.

## Configuration

Use the `appsettings.json` file to configure the application. A sample configuration file (`appsettings.json.example`) is available in the project directory. Create a `appsettings.json` file with your real settings and use the following structure:

```json
{
  "AWS": {
    "AccessKeyId": "your_access_key_id",
    "SecretAccessKey": "your_secret_access_key",
    "Region": "your_region",
    "SecurityGroupId": "your_security_group_id"
  },
  "Ports": {
    "FromPort": 3389,
    "ToPort": 3389
  }
}
```

## Usage
To run the application, use the following command in the terminal or command prompt from the root directory of the project:
```bash
dotnet run
```

This command will start the application and update your IP address in the specified AWS Security Group.


## License
This project is licensed under the MIT License.
