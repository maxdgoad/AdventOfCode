using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024;
internal class Advent8x1
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent8.txt").Select((str) => str[0]).ToList();

        var ans = 0;

        var antinodes = new Dictionary<(int, int), bool>();

        var signals = new Dictionary<char, List<(int, int)>>();

        // find character and their positions
        for (int i = 0; i < stringArr.Count; i++)
        {
            for (int j = 0; j < stringArr[i].Length; j++)
            {
                // log character and position
                if (stringArr[i][j] != '.' && !signals.ContainsKey(stringArr[i][j]))
                {
                    signals[stringArr[i][j]] = new List<(int, int)>();
                    signals[stringArr[i][j]].Add((j, i));
                }
                else if (signals.ContainsKey(stringArr[i][j]))
                {
                    signals[stringArr[i][j]].Add((j,i));
                }        

            }
        }

        // find antinodes
        foreach (var signal in signals)
        {
            // assign antinodes
            for (int i = 0; i < signal.Value.Count; i++)
            {
                for (int j = i + 1; j < signal.Value.Count; j++)
                {
                    var yDiff = signal.Value[j].Item2 - signal.Value[i].Item2;
                    var xDiff = signal.Value[i].Item1 - signal.Value[j].Item1;

                    var antinode1Pos = (1, 1);
                    var antinode2Pos = (1, 1);

                    // if slope is positive
                    if (xDiff != 0 && (double)yDiff / (double)xDiff > 0)
                    {
                        antinode1Pos = (signal.Value[i].Item1 + Math.Abs(xDiff), signal.Value[i].Item2 - Math.Abs(yDiff));
                        antinode2Pos = (signal.Value[j].Item1 - Math.Abs(xDiff), signal.Value[j].Item2 + Math.Abs(yDiff));
                    }
                    else
                    {
                        antinode1Pos = (signal.Value[i].Item1 - Math.Abs(xDiff), signal.Value[i].Item2 - Math.Abs(yDiff));
                        antinode2Pos = (signal.Value[j].Item1 + Math.Abs(xDiff), signal.Value[j].Item2 + Math.Abs(yDiff));
                    }


                    if (antinode1Pos.Item1 >= 0 && antinode1Pos.Item1 < stringArr[0].Length && antinode1Pos.Item2 >= 0 && antinode1Pos.Item2 < stringArr.Count)
                    {
                        //antinodes.Add((antinode1Pos.Item1, antinode1Pos.Item2));
                        //if (stringArr[antinode1Pos.Item1][antinode1Pos.Item2] == '.')
                        //{
                        if (!antinodes.TryGetValue((antinode1Pos.Item1, antinode1Pos.Item2), out var value))
                        {
                            antinodes.Add((antinode1Pos.Item1, antinode1Pos.Item2), true);
                        }                   
                    }

                    if (antinode2Pos.Item1 >= 0 && antinode2Pos.Item1 < stringArr[0].Length && antinode2Pos.Item2 >= 0 && antinode2Pos.Item2 < stringArr.Count)
                    {
                        //antinodes.Add((antinode1Pos.Item1, antinode1Pos.Item2));
                        //if (stringArr[antinode2Pos.Item1][antinode2Pos.Item2] == '.')
                        //{
                        if (!antinodes.TryGetValue((antinode2Pos.Item1, antinode2Pos.Item2), out var value))
                        {
                            antinodes.Add((antinode2Pos.Item1, antinode2Pos.Item2), true);
                        }
                        //}
                    }
                }
            }
        }

        ans = antinodes.Keys.Count;

        //  

        return ans.ToString();
    }
}
