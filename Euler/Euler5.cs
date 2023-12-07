using AdventOfCode.Utils;

namespace AdventOfCode.Euler;
internal static class Euler5
{
    public static string Run()
    {
        var ans = 1;

        for (int i = 2; i <= 20; i++)
        {
            if (Primes.IsPrime(i))
            {
                ans = ans * i;
            }
        }

        return ans.ToString();
    }
}
