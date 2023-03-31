# DataProtectionCrypto

A CLI that crypto your data with [data protection](https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-6.0). ( Windows )

## Usage

### Encrypt

1.  Encrypt your data with service name.

    ```sh
    .\DataProtectionCrypto.exe encrypt -dt "data" -sn "serviceName"
    ```

2.  Get encrypted data.

    ```sh
    CfDJ8KiFdkktLYZLhhnnejE-siWiMI7uvqroHcK2y7QPHbMN-Zy06xqNOYYQP8CGANCilSeSAWoDNeAcplglCFIyBJnSyhYul5kHXmB7s7G33WtcXVX7FO0rrwWwyj8kV7OSEg
    ```

3.  Your can typing `.\DataProtectionCrypto.exe encrypt help` see more options.

    ```sh
    Usage: encrypt [options...]

    Encrypt with data protection.

    Options:
      -dt, --data <String>           Data. (Required)
      -sn, --svc-name <String>       Service name. (Required)
      -an, --app-name <String>       Application name. (Default: APP)
      -kp, --key-path <String>       Key path. (Default: C:\keys)
      -kt, --key-lifetime <Int32>    Key lifetime. (Default: 90)
    ```

### Decrypt

1.  Decrypt your data with service name.

    ```sh
    .\DataProtectionCrypto.exe decrypt -pdt "CfDJ8KiFdkktLYZLhhnnejE-siWiMI7uvqroHcK2y7QPHbMN-Zy06xqNOYYQP8CGANCilSeSAWoDNeAcplglCFIyBJnSyhYul5kHXmB7s7G33WtcXVX7FO0rrwWwyj8kV7OSEg" -sn "serviceName"
    ```

2.  Get decrypted data.

    ```sh
    data
    ```

3.  Your can typing `.\DataProtectionCrypto.exe decrypt help` see more options.

    ```sh
    Usage: decrypt [options...]

    Decrypt with data protection.

    Options:
      -pdt, --protected-data <String>    Protected data. (Required)
      -sn, --svc-name <String>           Service name. (Required)
      -an, --app-name <String>           Application name. (Default: APP)
      -kp, --key-path <String>           Key path. (Default: C:\keys)
    ```

## Other reference

- [Configure ASP.NET Core Data Protection](https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/configuration/overview?view=aspnetcore-6.0)
