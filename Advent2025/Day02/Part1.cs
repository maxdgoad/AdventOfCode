using AdventOfCode.Utils;

namespace AdventOfCode.Advent2025.Day02;

internal class Part1
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Day02\\advent.txt", ",");
        var ranges = stringArr[0].Select(s => (long.Parse(s.Split("-")[0]), long.Parse(s.Split("-")[1]))).ToArray();

        long ans = 0;
        foreach (var (min, max) in ranges)
        {
            for (long i = min; i <= max; i++)
            {
                var str = i.ToString();
                if (str.Length % 2 == 1)
                {
                    i += ((long)Math.Pow(10, str.Length) - i);
                }
                else
                {
                    var firstHalf = str.Substring(0, str.Length / 2);
                    var secondHalf = str.Substring(str.Length / 2, str.Length / 2);
                    if (firstHalf == secondHalf)
                    {
                        ans += i;
                        Console.WriteLine($"{min}-{max}: {i}");
                    }
                }
            }
        }

        return ans.ToString();
    }
}
