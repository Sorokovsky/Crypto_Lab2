using Lab2.Common.Math;

namespace Lab2.Tests.Math;

[TestClass]
public class GcdTests
{
    [TestMethod]
    public void ShouldCorrect()
    {
        List<int> input = [156, 210];
        const int expected = 6;
        var result = Number.Gcd(input);
        Assert.AreEqual(expected, result);
    }
}