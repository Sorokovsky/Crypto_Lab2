using Lab2.Common.Interfaces;
using Lab2.Common.Tools;
using Lab2.Hill;

namespace Lab2.Tests.Hill;

[TestClass]
public class HillTests
{
    private readonly string _decrypted = "help";
    private readonly string _encrypted = "hiat";
    private readonly IEncryptor _encryptor = new HillEncryptor(Alphabets.En, 2);
    private readonly string _key = "ddcf";

    [TestMethod]
    public void ShouldCorrectEncrypt()
    {
        var result = _encryptor.Encrypt(_decrypted, _key);
        Assert.AreEqual(_encrypted.ToUpper(), result.ToUpper());
    }

    [TestMethod]
    public void ShouldCorrectDecrypt()
    {
        var result = _encryptor.Decrypt(_encrypted, _key);
        Assert.AreEqual(_decrypted.ToUpper(), result.ToUpper());
    }
}