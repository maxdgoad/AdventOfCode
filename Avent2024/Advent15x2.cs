using AdventOfCode.Utils;
using System.Numerics;
using System.Text;

namespace AdventOfCode.Advent2024;
internal class Advent15x2
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent15.txt").Select((str) => str.Count == 0 ? "" : str[0]).ToList();

        BigInteger ans = 0;

        var robotPos = (0, 0);

        var map = new Dictionary<(int, int), char>();

        var inputSb = new StringBuilder();

        var isMap = true;

        for (int y = 0; y < stringArr.Count; y++)
        {
            if (stringArr[y].Length == 0)
            {
                isMap = false;
            }
            if (!isMap)
            {
                inputSb.Append(stringArr[y]);
                continue;
            }
            for (int x = 0; x < stringArr[y].Length; x++)
            {
                map[(x, y)] = stringArr[y][x];

                if (stringArr[y][x] == '@')
                {
                    robotPos = (x, y);
                }
            }
        }

        var input = inputSb.ToString();

        foreach (var direction in input)
        {
            int y = 0;
            int x = 0;
            if (direction == '^')
            {
                y = -1;
            }
            else if (direction == 'v')
            {
                y = 1;
            }
            else if (direction == '<')
            {
                x = -1;
            }
            else if (direction == '>')
            {
                x = 1;
            }

            if (map.TryGetValue((robotPos.Item1 + x, robotPos.Item2 + y), out var value))
            {
                if (value == '.')
                {
                    map[robotPos] = '.';
                    robotPos = (robotPos.Item1 + x, robotPos.Item2 + y);
                    map[robotPos] = '@';

                }
                else if (value == 'O')
                {
                    var pos = (robotPos.Item1 + x, robotPos.Item2 + y);
                    var firstPos = pos;
                    while (map[pos] == 'O')
                    {
                        pos = (pos.Item1 + x, pos.Item2 + y);
                    }
                    if (map[pos] == '.')
                    {
                        map[robotPos] = '.';
                        map[pos] = 'O';
                        map[firstPos] = '@';
                        robotPos = firstPos;
                    }
                }
            }
            Console.WriteLine(direction);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (map.ContainsKey((j, i)))
                    {
                        Console.Write(map[(j, i)]);
                    }
                }
                Console.WriteLine();
            }
        }

        foreach (var key in map.Keys)
        {
            if (map[key] == 'O')
            {
                ans += 100 * key.Item2 + key.Item1;
            }
        }

        return ans.ToString();
    }
}
