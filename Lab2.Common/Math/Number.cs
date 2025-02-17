namespace Lab2.Common.Math;

public static class Number
{
    public static List<int> Simplify(int number)
    {
        var resultNumber = number;
        List<int> result = [];
        var primes = GeneratePrimes(number);
        do
        {
            var divider = primes.FirstOrDefault(x => resultNumber % x == 0);
            if (divider == 0) break;
            resultNumber /= divider;
            result.Add(divider);
        } while (true);

        return result;
    }

    public static List<int> GeneratePrimes(int limit)
    {
        List<int> result = [];
        for (var i = 0; i <= limit; i++)
        {
            if (IsPrime(i)) result.Add(i);
        }

        return result;
    }

    public static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        for (var i = 2; i < number; i++)
        {
            if (number % i == 0) return false;
        }

        return true;
    }

    public static int Gcd(IEnumerable<int> numbers)
    {
        return numbers.Aggregate(Gcd);
    }

    private static int Gcd(int a, int b)
    {
        if(b > a) (a, b) = (b, a);
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}