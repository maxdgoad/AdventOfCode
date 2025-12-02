using AdventOfCode.Utils;

namespace AdventOfCode.Advent2025.Day02;

internal class Part2
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

                var barcodes = new List<long>();
                for (int split = 1; split <= str.Length/2; split++)
                {
                    if (str.Length % split == 0)
                    {
                        var parts = new List<string>();
                        for (int cut = 0; cut < str.Length / split; cut++)
                        {
                            parts.Add(str.Substring(cut * split, split));
                        }

                        if (parts.All(p => p == parts[0]))
                        {
                            barcodes.Add(i);
                        }
                    }
                }

                barcodes = barcodes.Distinct().ToList();
                foreach (var barcode in barcodes)
                {
                    ans += barcode;
                    Console.WriteLine($"{min}-{max}: {i}");
                }
            }
        }

        return ans.ToString();
    }
}
