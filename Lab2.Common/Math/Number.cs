namespace Lab2.Common.Math;

public static class Number
{
    public static List<int> Simplify(int number)
    {
        List<int> result = [];
        
        return result;
    }

    public static List<int> GeneratePrimes(int limit)
    {
        List<int> result = [];
        for (var i = 0; i <= limit; i++)
        {
            if(IsPrime(i)) result.Add(i);
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
}