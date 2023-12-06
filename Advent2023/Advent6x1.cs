using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent6x1
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent6.txt", " ");

        long ans = 0;

        var times = stringArr[0].Where(x => int.TryParse(x, out var cali)).Select(x => int.Parse(x)).ToList();
        var distances = stringArr[1].Where(x => int.TryParse(x, out var cali)).Select(x => int.Parse(x)).ToList();

        List<long> winningTimes = new List<long>();

        for (int y = 0; y < distances.Count; y++)
        {
            var startingIndex = (int)Math.Ceiling((double)times[y] / 2) - 1;
            int winningTimesCount = times[y] % 2 == 0 ? 1 : 0;
            bool previousWasFewerThanDistance = false;
            for (int i = startingIndex; !previousWasFewerThanDistance; i--)
            {
                if (i * (times[y] - i) > distances[y])
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
        }

        ans = winningTimes.Aggregate(1L, (acc, x) => acc * x);
        return ans.ToString();
    }
}
