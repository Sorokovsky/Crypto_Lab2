using Lab2.Common.Tools;
using Lab2.Visionary;

namespace Lab2.Tests.Visionary;

[TestClass]
public class TableTests
{
    private readonly string _decrypted =
        "Many traces we found of him in the bog girt island where he had hid his savage ally a huge driving wheel and a shaft half filled with rubbish showed the position of an abandoned mine beside it were the crumbling remains of the cottages of the miners driven away no doubt by the foul reek of the surrounding swamp in one of these a staple and chain with a quantity of gnawed bones showed where the animal had been confined a skeleton with a tangle of brown hair adhering to it lay among the debris.";

    private readonly string _encrypted =
        "Mrgf niatxz qv ffnux ff ybt ce tyx iix gzka cjlrgk qyeix oy yau apx yij lhprgv tsfp a ynny urzophx wyxlf rnu t zbrfk ahfw fzesyu wzmo llbsbzb jhfplx khv ivmztzhu iw ae tiuedfglx diex iyjiux pn neix abv cintvciez yydazgz iw tyx jiktrzlm ff kal gznvkz xiimxu unap gv xfusm is khv yvoc rvxr iw tyx zoirfnuxznx ldudp zg vhv ow moyje r lauglv tux thrbu qzty t xornkbas ff xghqvd shuyj syhdyu wyxyy khv tucdac ahx sevg jiefzglx r sbxsykoe ppny a ktuace fy ilfwe ahci auallznx mv ck lrr hgfnx moy ueskpm.";

    [TestMethod]
    public void ShouldEnglishCorrect()
    {
        var output =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ\nBCDEFGHIJKLMNOPQRSTUVWXYZA\nCDEFGHIJKLMNOPQRSTUVWXYZAB\nDEFGHIJKLMNOPQRSTUVWXYZABC\nEFGHIJKLMNOPQRSTUVWXYZABCD\nFGHIJKLMNOPQRSTUVWXYZABCDE\nGHIJKLMNOPQRSTUVWXYZABCDEF\nHIJKLMNOPQRSTUVWXYZABCDEFG\nIJKLMNOPQRSTUVWXYZABCDEFGH\nJKLMNOPQRSTUVWXYZABCDEFGHI\nKLMNOPQRSTUVWXYZABCDEFGHIJ\nLMNOPQRSTUVWXYZABCDEFGHIJK\nMNOPQRSTUVWXYZABCDEFGHIJKL\nNOPQRSTUVWXYZABCDEFGHIJKLM\nOPQRSTUVWXYZABCDEFGHIJKLMN\nPQRSTUVWXYZABCDEFGHIJKLMNO\nQRSTUVWXYZABCDEFGHIJKLMNOP\nRSTUVWXYZABCDEFGHIJKLMNOPQ\nSTUVWXYZABCDEFGHIJKLMNOPQR\nTUVWXYZABCDEFGHIJKLMNOPQRS\nUVWXYZABCDEFGHIJKLMNOPQRST\nVWXYZABCDEFGHIJKLMNOPQRSTU\nWXYZABCDEFGHIJKLMNOPQRSTUV\nXYZABCDEFGHIJKLMNOPQRSTUVW\nYZABCDEFGHIJKLMNOPQRSTUVWX\nZABCDEFGHIJKLMNOPQRSTUVWXY\n";
        var result = VisionaryTable.Generate(Alphabets.En.Letters).ToString();
        Assert.AreEqual(output, result);
    }

    [TestMethod]
    public void ShouldCorrectEnGetting()
    {
        var table = VisionaryTable.Generate(Alphabets.En.Letters);
        const char expected = 'N';
        VisionaryKey key = new('E', 'J');
        var result = table.GetByKey(key);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void ShouldCorrectEncrypt()
    {
        var encryptor = new VisionaryEncryptor(Alphabets.En);
        const string key = "ARTHUR";
        var result = encryptor.Encrypt(_decrypted, key);
        Assert.AreEqual(_encrypted, result);
    }

    [TestMethod]
    public void ShouldCorrectDecrypt()
    {
        var encryptor = new VisionaryEncryptor(Alphabets.En);
        const string key = "ARTHUR";
        var result = encryptor.Decrypt(_encrypted, key);
        Assert.AreEqual(_decrypted, result);
    }
}