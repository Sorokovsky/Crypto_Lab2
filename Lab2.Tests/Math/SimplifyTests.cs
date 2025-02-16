using Lab2.Common.Math;
using Lab2.Tests.Extensions;

namespace Lab2.Tests.Math;

[TestClass]
public class SimplifyTests
{
    [TestMethod]
    public void ShouldBeCorrect()
    {
        const int input = 20;
        List<int> expected = [2, 2, 5];
        var result = Number.Simplify(input);
        ListAsserting.AreListEqual(expected, result);
    }
}