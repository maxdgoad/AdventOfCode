using AdventOfCode.Utils;

namespace AdventOfCode.Advent2025.Day03;

internal class Part1
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Day03\\advent.txt", " ");
        var banks = stringArr.Select(x => x[0].Select(c =>
        {
            var s = c.ToString();
            return int.Parse(s);
        }).ToList()).ToList();
        var ans = 0;

        foreach (var bank in banks)
        {
            var maxTens = 0;
            var maxOnes = 0;
            var max = 0;
            for (int i = 0; i < bank.Count; i++)
            {
                for (int j = bank.Count - 1; j > i; j--)
                {
                    if (bank[i] * 10 + bank[j] > max)
                    {
                        maxTens = bank[i];
                        maxOnes = bank[j];
                        max = bank[i] * 10 + bank[j];
                    }
                }
            }
            Console.WriteLine($"Max tens: {maxTens}, Max ones: {maxOnes}, Max: {max}");
            ans += max;
        }

        return ans.ToString();
    }
}
