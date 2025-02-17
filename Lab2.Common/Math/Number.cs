namespace Lab2.Common.Math;

public static class Number
{
    public static int Gcd(IEnumerable<int> numbers)
    {
        return numbers.Aggregate(Gcd);
    }

    private static int Gcd(int a, int b)
    {
        if (b > a) (a, b) = (b, a);
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}