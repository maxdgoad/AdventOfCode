using AdventOfCode.Utils;

namespace AdventOfCode.Advent2024;
internal class Advent12x1
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
            int area = plot.Item2.Count();
            int perimeter = 0;

            for (int k = 0; k < plot.Item2.Count; k++)
            {
                var i = plot.Item2[k].Item1;
                var j = plot.Item2[k].Item2;

                if (i + 1 >= stringArr.Length || (i + 1 < stringArr.Length && stringArr[i + 1][j] != plot.Item1))
                {
                    perimeter++;
                }
                if (i - 1 < 0 || (i - 1 >= 0 && stringArr[i - 1][j] != plot.Item1))
                {
                    perimeter++;
                }

                if (j + 1 >= stringArr[0].Length || (j + 1 < stringArr[0].Length && stringArr[i][j + 1] != plot.Item1))
                {
                    perimeter++;
                }
                if (j - 1 < 0 || (j - 1 >= 0 && stringArr[i][j - 1] != plot.Item1))
                {
                    perimeter++;
                }
            }

            ans += area * perimeter;
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
            if (i + 1 < map.Length && map[i+1][j] == plant)
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
