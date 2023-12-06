using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent6x2
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent6.txt", " ");

        long ans = 0;

        var time = long.Parse(stringArr[0].Aggregate("", (agg, x) =>
        {
            if (long.TryParse(x, out var cali))
            {
                agg = agg + x;
            }
            return agg;
        }));
        long distance = long.Parse(stringArr[1].Aggregate("", (agg, x) =>
        {
            if (long.TryParse(x, out var cali))
            {
                agg = agg + x;
            }
            return agg;
        }));

        List<long> winningTimes = new List<long>();
        var startingIndex = (long)Math.Ceiling((double)time / 2) - 1;
        int winningTimesCount = time % 2 == 0 ? 1 : 0;
        bool previousWasFewerThanDistance = false;
        for (long i = startingIndex; !previousWasFewerThanDistance; i--)
        {
            if (i * (time - i) > distance)
            {
                winningTimesCount += 2;
            }
            else
            {
                previousWasFewerThanDistance = true;
                break;
            }
        }
        winningTimes.Add(winningTimesCount);
        ans = winningTimes.Aggregate(1L, (acc, x) => acc * x);
        return ans.ToString();
    }
}
