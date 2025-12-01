using AdventOfCode.Utils;
using System.Linq;

namespace AdventOfCode.Advent2024;
internal class Advent12x2
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFileCharArray("Advent12.txt", " ");

        var plots = new List<(char, List<(int, int)>)>();

        var visited = new List<(int, int)>();

        var ans = 0;

        for (int i = 0; i < stringArr.Length; i++)
        {
            for (int j = 0; j < stringArr[i].Length; j++)
            {
                if (!visited.Contains((i, j)))
                {
                    var plot = new List<(int, int)>();
                    DeterminePlot(stringArr, i, j, plot, stringArr[i][j]);
                    plots.Add((stringArr[i][j], plot));
                    visited.Add((i, j));
                    visited.AddRange(plot);
                }
            }
        }

        foreach (var plot in plots)
        {
            var area = plot.Item2.Count();

            var sides = new List<(int, int, int)>();
            for (int k = 0; k < plot.Item2.Count; k++)
            {
                var i = plot.Item2[k].Item1;
                var j = plot.Item2[k].Item2;

                if (i + 1 >= stringArr.Length || (i + 1 < stringArr.Length && stringArr[i + 1][j] != plot.Item1))
                {
                    sides.Add((i+1, j, 0));
                }
                if (i - 1 < 0 || (i - 1 >= 0 && stringArr[i - 1][j] != plot.Item1))
                {
                    sides.Add((i - 1, j, 1));
                }

                if (j + 1 >= stringArr[0].Length || (j + 1 < stringArr[0].Length && stringArr[i][j + 1] != plot.Item1))
                {
                    sides.Add((i, j + 1, 2));
                }
                if (j - 1 < 0 || (j - 1 >= 0 && stringArr[i][j - 1] != plot.Item1))
                {
                    sides.Add((i, j - 1, 3));
                }
            }

            var numberOfSides = 0;

            // count horizontal sides
            for (int i = -1; i <= stringArr.Length; i++)
            {
                var count = 0;
                var lastIsChar = false;
                for (int d = 0; d <= 1; d++)
                {
                    for (int j = -1; j <= stringArr[0].Length; j++)
                    {
                        if (sides.Contains((i, j, d)))
                        {
                            if (!lastIsChar)
                            {
                                count++;
                            }
                            lastIsChar = true;
                        }
                        else
                        {
                            lastIsChar = false;
                        }
                    }
                }
                numberOfSides += count;
            }

            // count vertical sides
            for (int j = -1; j <= stringArr[0].Length; j++)
            {
                var count = 0;
                var lastIsChar = false;
                for (int d = 2; d <= 3; d++)
                {
                    for (int i = -1; i <= stringArr.Length; i++)
                    {
                        if (sides.Contains((i, j, d)))
                        {
                            if (!lastIsChar)
                            {
                                count++;
                            }
                            lastIsChar = true;
                        }
                        else
                        {
                            lastIsChar = false;
                        }
                    }
                }
                numberOfSides += count;
            }

            Console.WriteLine($"Plot: {plot.Item1}, Area: {area}, Sides: {numberOfSides}");
            ans += area * numberOfSides;
        }

        return ans.ToString();
    }

    public static void DeterminePlot(char[][] map, int i, int j, List<(int, int)> plot, char plant)
    {
        if (plot.Contains((i, j)))
        {
            return;
        }
        else
        {
            plot.Add((i, j));
            if (i + 1 < map.Length && map[i + 1][j] == plant)
            {
                DeterminePlot(map, i + 1, j, plot, plant);
            }
            if (i - 1 >= 0 && map[i - 1][j] == plant)
            {
                DeterminePlot(map, i - 1, j, plot, plant);
            }
            if (j + 1 < map[i].Length && map[i][j + 1] == plant)
            {
                DeterminePlot(map, i, j + 1, plot, plant);
            }
            if (j - 1 >= 0 && map[i][j - 1] == plant)
            {
                DeterminePlot(map, i, j - 1, plot, plant);
            }
        }
    }
}
