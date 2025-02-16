using Lab2.Common.Math;

namespace Lab2.Tests.Math;

[TestClass]
public class PrimeCheckerTests
{
    [TestMethod]
    public void ShouldBeCorrect()
    {
        const int input = 5;
        var result = Number.IsPrime(input);
        Assert.IsTrue(result);
    }
    
    [TestMethod]
    public void ShouldBeWrong()
    {
        const int input = 4;
        var result = Number.IsPrime(input);
        Assert.IsFalse(result);
    }
}