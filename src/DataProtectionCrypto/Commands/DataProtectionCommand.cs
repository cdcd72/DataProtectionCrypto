using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace DataProtectionCrypto.Commands;

public class DataProtectionCommand : ConsoleAppBase
{
    private const string DefaultAppName = "APP";
    private const string DefaultKeyPath = @"C:\keys";
    private const int DefaultKeyLifetime = 90;
    
    [Command("encrypt", "Encrypt with data protection.")]
    public string Encrypt(
        [Option("dt", "Data.")] string data,
        [Option("sn", "Service name.")] string svcName,
        [Option("an", "Application name.")] string appName = DefaultAppName,
        [Option("kp", "Key path.")] string keyPath = DefaultKeyPath,
        [Option("kt", "Key lifetime.")] int keyLifetime = DefaultKeyLifetime)
    {
        try
        {
            var protectedData = GetDataProtectionProvider(appName, keyPath, keyLifetime)
                .CreateProtector(svcName)
                .Protect(data);

            Console.WriteLine(protectedData);

            return protectedData;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Encrypt fail! {ex.Message}");
        }
        
        return null;
    }
    
    [Command("decrypt", "Decrypt with data protection.")]
    public string Decrypt(
        [Option("pdt", "Protected data.")] string protectedData,
        [Option("sn", "Service name.")] string svcName,
        [Option("an", "Application name.")] string appName = DefaultAppName,
        [Option("kp", "Key path.")] string keyPath = DefaultKeyPath)
    {
        try
        {
            var data = GetDataProtectionProvider(appName, keyPath)
                .CreateProtector(svcName)
                .Unprotect(protectedData);

            Console.WriteLine(data);

            return data;
        }
        catch (Exception)
        {
            Console.WriteLine("Decrypt fail! Please check your inputs is correct.");
        }
        
        return null;
    }

    private IDataProtectionProvider GetDataProtectionProvider(string appName, string keyPath, int? keyLifetime = null)
    {
        var services = new ServiceCollection();
    
        if (!Directory.Exists(keyPath))
            Directory.CreateDirectory(keyPath);

        var dataProtectionBuilder = services
            .AddDataProtection()
            .SetApplicationName(appName)
            .PersistKeysToFileSystem(new DirectoryInfo(keyPath));

        if (keyLifetime.HasValue) 
            dataProtectionBuilder.SetDefaultKeyLifetime(TimeSpan.FromDays(keyLifetime.Value));

        var sp = services.BuildServiceProvider();

        return sp.GetRequiredService<IDataProtectionProvider>();
    }
}