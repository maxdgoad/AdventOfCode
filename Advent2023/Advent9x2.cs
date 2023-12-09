using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent9x2
{
    public static List<long> FindDiffs(List<long> vals)
    {
        List<long> diffs = new List<long>();

        for (int rep = 0; rep < vals.Count-1; rep++)
        {
            diffs.Add(vals[rep] - vals[rep+1]);
        }
        return diffs;
    }
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent9.txt", " ");

        long ans = 0;

        foreach (var sequence in stringArr)
        {
            var longSequence = sequence.Select(val => long.Parse(val)).ToList();
            // evaluate diffs until all zeroes
            List<List<long>> diffPyramid = new List<List<long>>();
            diffPyramid.Add(longSequence);
            while (!FindDiffs(longSequence).All(val => val == 0))
            {
                longSequence = FindDiffs(longSequence);
                diffPyramid.Add(longSequence);
            }
            // extrapolate

            for (int rep = diffPyramid.Count - 1; rep >= 1; rep--)
            {
                diffPyramid[rep - 1].Insert(0, (diffPyramid[rep - 1][0] + diffPyramid[rep][0]));
            }
            ans += diffPyramid[0][0];
        }
        return ans.ToString();
    }
}
