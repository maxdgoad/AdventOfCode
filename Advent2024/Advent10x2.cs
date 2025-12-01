using AdventOfCode.Utils;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode.Advent2024;
internal class Advent10x2
{
    public static string Run()
    {
        var map = FileReader.ReadFileCharArray("Advent10.txt");

        BigInteger ans = 0;

        var visitedPositions = new Dictionary<(int, int), int>(); // true if leads to a value of 9

        var trailheads = new List<(int, int)>();

        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == '0')
                {
                    trailheads.Add((i, j));
                }
            }
        }

        foreach (var trailhead in trailheads)
        {
            var val = Traverse(map, trailhead, visitedPositions, new Dictionary<(int, int), bool>());
            Console.WriteLine(trailhead + " " + val);
            ans += val;
        }

        return ans.ToString();
    }

    public static int Traverse(char[][] map, (int, int) currentPos, Dictionary<(int, int), int> visitedPositions, Dictionary<(int, int), bool> mountainTops)
    {
        if (map[currentPos.Item1][currentPos.Item2] == '9')
        {
            mountainTops[currentPos] = true;
            return 1;
        }
        else
        {
            var countFromHere = 0;
            // traverse
            if (currentPos.Item1 + 1 < map.Length && map[currentPos.Item1 + 1][currentPos.Item2] == map[currentPos.Item1][currentPos.Item2] + 1)
            {
                countFromHere += Traverse(map, (currentPos.Item1 + 1, currentPos.Item2), visitedPositions, mountainTops);
            }
            if (currentPos.Item2 + 1 < map[0].Length && map[currentPos.Item1][currentPos.Item2 + 1] == map[currentPos.Item1][currentPos.Item2] + 1)
            {
                countFromHere += Traverse(map, (currentPos.Item1, currentPos.Item2 + 1), visitedPositions, mountainTops);
            }
            if (currentPos.Item1 - 1 >= 0 && map[currentPos.Item1 - 1][currentPos.Item2] == map[currentPos.Item1][currentPos.Item2] + 1)
            {
                countFromHere += Traverse(map, (currentPos.Item1 - 1, currentPos.Item2), visitedPositions, mountainTops);
            }
            if (currentPos.Item2 - 1 >= 0 && map[currentPos.Item1][currentPos.Item2 - 1] == map[currentPos.Item1][currentPos.Item2] + 1)
            {
                countFromHere += Traverse(map, (currentPos.Item1, currentPos.Item2 - 1), visitedPositions, mountainTops);
            }

            visitedPositions[currentPos] = countFromHere;

            return countFromHere;
        }
    }
}
