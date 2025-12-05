using AdventOfCode.Utils;

namespace AdventOfCode.Advent2025.Day03;

internal class Part2
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Day03\\advent.txt", " ");
        var banks = stringArr.Select(x => x[0].Select(c => int.Parse(c.ToString())).ToList()).ToList();
        long ans = 0;

        foreach (var bank in banks)
        {
            var max = GetMaxRecursive(-1, 12, bank);
            ans += max;

            Console.WriteLine($"Max: {max}");
            memo.Clear();
        }

        return ans.ToString();
    }

    private static Dictionary<(int, int), long> memo = new();
    public static long GetMaxRecursive(int startIndex, int place, List<int> bank)
    {
        if (place == 0) return 0;

        if (memo.ContainsKey((startIndex, place))) return memo[(startIndex, place)];

        long max = 0;
        for (int i = bank.Count - place; i > startIndex; i--)
        {
            var currentMax = bank[i] * (long)Math.Pow(10, place - 1) + GetMaxRecursive(i, place - 1, bank);
            if (currentMax > max) max = currentMax;
        }

        memo[(startIndex, place)] = max;

        return max;
    }
}
