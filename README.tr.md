# AWS Security Group Updater

Bu proje, kullanıcının IP adresini AWS Security Group'a güncellemek için kullanılır. Uygulama, kullanıcının mevcut IP adresini alır ve AWS EC2 Security Group içinde belirli bir port aralığı için bir kural olarak ayarlar.

## Kurulum

Projeyi kullanmadan önce aşağıdaki adımları tamamlamanız gerekmektedir:

1. **AWS CLI Yükleme ve Yapılandırma**:
   AWS CLI'nin yüklü olduğundan ve AWS kimlik bilgilerinizin (`AWS_ACCESS_KEY_ID`, `AWS_SECRET_ACCESS_KEY` ve `AWS_REGION`) yapılandırıldığından emin olun.

2. **.NET Runtime Yükleme**:
   Uygulamayı çalıştırmak için gerekli olan .NET runtime'ı yükleyin. Uygulama .NET Core 3.1 veya daha yeni bir sürümü kullanmaktadır.

## Yapılandırma

Uygulamayı yapılandırmak için `appsettings.json` dosyasını kullanın. Örnek yapılandırma dosyası (`appsettings.json.example`) proje dizininde bulunmaktadır. Gerçek ayarlarınızı içeren bir `appsettings.json` dosyası oluşturun ve aşağıdaki yapıyı kullanın:

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

## Kullanım
Uygulamayı çalıştırmak için, projenin kök dizinindeki terminal veya komut satırı üzerinden aşağıdaki komutu kullanın:

```bash
dotnet run
```

Bu komut, uygulamayı başlatır ve IP adresinizi belirtilen AWS Security Group içinde günceller.

## Lisans
Bu proje MIT lisansı altında lisanslanmıştır.
