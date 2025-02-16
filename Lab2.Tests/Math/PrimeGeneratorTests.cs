using Lab2.Common.Math;
using Lab2.Tests.Extensions;

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
        ListAsserting.AreListEqual(expected, result);
    }
}