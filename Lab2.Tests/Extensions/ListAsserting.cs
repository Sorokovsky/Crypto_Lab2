namespace Lab2.Tests.Extensions;

public static class ListAsserting
{
    public static void AreListEqual<T>(IEnumerable<T> expected, IEnumerable<T> result)
    {
        var expectedArray = expected as T[] ?? expected.ToArray();
        var resultArray = result as T[] ?? result.ToArray();
        Assert.AreEqual(expectedArray.Length, resultArray.Length);
        var count = System.Math.Min(expectedArray.Length, resultArray.Length);
        for (var i = 0; i < count; i++)
        {
            var first = expectedArray.ElementAt(i);
            var second = resultArray.ElementAt(i);
            Assert.AreEqual(first, second);
        }
    }
}