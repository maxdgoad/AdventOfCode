using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024;
internal class Advent17x2
{
    public static string Run()
    {
        BigInteger ans = 0;

        var values = new List<int>() { 2, 4, 1, 3, 7, 5, 0, 3, 1, 4, 4, 7, 5, 5, 3, 0 };
        BigInteger previous = 0;
        for (int k = values.Count - 1; k >= 0; k--)
        {
            for (BigInteger i = 8 * previous; i < 9 * previous + 10; i++)
            {
                var a0 = i;
                var lhs = (((a0 % 8) ^ 3) ^ 4);
                var rhs = a0 / BigInteger.Pow(2, (int)(a0 % 8) ^ 3);
                if ((lhs ^ rhs) % 8 == values[k] )
                {
                    if (i / 8 == previous)
                    {
                        Console.WriteLine(values[k] + ": " + i);
                        previous = i;
                        break;
                    }          
                }
            }
        }

        return ans.ToString();
    }
}
