using Lab2.Common.Math;

namespace Lab2.Tests.Math;

[TestClass]
public class PrimeGeneratorTests
{
    [TestMethod]
    public void ShouldBeCorrect()
    {
        const int input = 11;
        List<int> expected = [2, 3, 5, 7, 11];
        var result = Number.GeneratePrimes(input);
        Assert.AreEqual(expected.Count, result.Count);
        var count = System.Math.Min(expected.Count, result.Count);
        for (var i = 0; i < count; i++)
        {
            var first = expected.ElementAt(i);
            var second = result.ElementAt(i);
            Assert.AreEqual(first, second);
        }
    }
}