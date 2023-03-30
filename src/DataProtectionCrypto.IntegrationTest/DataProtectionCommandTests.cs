using System.Reflection;
using DataProtectionCrypto.Commands;
using NUnit.Framework;

namespace DataProtectionCrypto.IntegrationTest;

public class DataProtectionCommandTests
{
    #region Properties

    private static string CurrentDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;

    private static string KeyPath => Path.Combine(CurrentDirectory, "TestData", "Keys");

    #endregion
    
    [Test]
    public void EncryptSuccess()
    {
        var command = new DataProtectionCommand();

        const string sensitiveData = ":P";
        
        var protectedData = command.Encrypt(sensitiveData, "Service", "Application", KeyPath, 7);
        
        Assert.That(protectedData, Is.Not.EqualTo(sensitiveData));
    }
    
    [Test]
    public void DecryptFail()
    {
        var command = new DataProtectionCommand();

        const string svcName = "Service";
        const string appName = "Application";

        var protectedData = command.Encrypt(":P", svcName, appName, KeyPath, 7);

        var decryptedData = command.Decrypt(protectedData, "xxx", appName, KeyPath);

        Assert.That(decryptedData, Is.Null);
    }
    
    [Test]
    public void DecryptSuccess()
    {
        var command = new DataProtectionCommand();

        const string sensitiveData = ":P";
        const string svcName = "Service";
        const string appName = "Application";

        var protectedData = command.Encrypt(sensitiveData, svcName, appName, KeyPath, 7);

        var decryptedData = command.Decrypt(protectedData, svcName, appName, KeyPath);

        Assert.That(decryptedData, Is.EqualTo(sensitiveData));
    }

    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(KeyPath)) Directory.Delete(KeyPath, true);
    }
}