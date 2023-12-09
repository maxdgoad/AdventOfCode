using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent9x1
{

    public static List<long> FindDiffs(List<long> vals)
    {
        List<long> diffs = new List<long>();

        for (int rep = 1; rep < vals.Count-1; rep++) 
        {
            diffs.Add(vals[rep+1] - vals[rep]);
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
                diffPyramid[rep - 1].Add(diffPyramid[rep - 1][diffPyramid[rep-1].Count-1] + diffPyramid[rep][diffPyramid[rep].Count -1]);
            }
            ans += diffPyramid[0][diffPyramid[0].Count - 1];
        }
        return ans.ToString();
    }
}
