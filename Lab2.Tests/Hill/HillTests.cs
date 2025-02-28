using Lab2.Common.Interfaces;
using Lab2.Common.Tools;
using Lab2.Hill;

namespace Lab2.Tests.Hill;

[TestClass]
public class HillTests
{
    [TestMethod]
    public void ShouldCorrectEncrypt()
    {
        const string text = "help";
        const string key = "ddcf";
        const string expected = "hiat";
        IEncryptor encryptor = new HillEncryptor(Alphabets.En, 2);
        var result = encryptor.Encrypt(text, key);
        Assert.AreEqual(expected.ToUpper(), result);
    }
}