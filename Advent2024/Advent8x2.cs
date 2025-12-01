using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024;
internal class Advent8x2
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
                    signals[stringArr[i][j]].Add((j, i));
                }
                if (stringArr[i][j] != '.' && !antinodes.TryGetValue((j, i), out var value))
                {
                    antinodes.Add((j, i), true);
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

                    var antinode1Pos = signal.Value[i];
                    var antinode2Pos = signal.Value[j];

                    var positiveSlope = xDiff != 0 && (double)yDiff / (double)xDiff > 0;

                    // if slope is positive
                    if (positiveSlope)
                    {
                        antinode1Pos = (antinode1Pos.Item1 + Math.Abs(xDiff), antinode1Pos.Item2 - Math.Abs(yDiff));
                        antinode2Pos = (antinode2Pos.Item1 - Math.Abs(xDiff), antinode2Pos.Item2 + Math.Abs(yDiff));
                    }
                    else
                    {
                        antinode1Pos = (antinode1Pos.Item1 - Math.Abs(xDiff), antinode1Pos.Item2 - Math.Abs(yDiff));
                        antinode2Pos = (antinode2Pos.Item1 + Math.Abs(xDiff), antinode2Pos.Item2 + Math.Abs(yDiff));
                    }


                    while (antinode1Pos.Item1 >= 0 && antinode1Pos.Item1 < stringArr[0].Length && antinode1Pos.Item2 >= 0 && antinode1Pos.Item2 < stringArr.Count)
                    {                      

                        if (!antinodes.TryGetValue((antinode1Pos.Item1, antinode1Pos.Item2), out var value))
                        {
                            antinodes.Add((antinode1Pos.Item1, antinode1Pos.Item2), true);
                        }

                        if (positiveSlope)
                        {
                            antinode1Pos = (antinode1Pos.Item1 + Math.Abs(xDiff), antinode1Pos.Item2 - Math.Abs(yDiff));
                        }
                        else
                        {
                            antinode1Pos = (antinode1Pos.Item1 - Math.Abs(xDiff), antinode1Pos.Item2 - Math.Abs(yDiff));
                        }
                    }

                    while (antinode2Pos.Item1 >= 0 && antinode2Pos.Item1 < stringArr[0].Length && antinode2Pos.Item2 >= 0 && antinode2Pos.Item2 < stringArr.Count)
                    {                     

                        if (!antinodes.TryGetValue((antinode2Pos.Item1, antinode2Pos.Item2), out var value))
                        {
                            antinodes.Add((antinode2Pos.Item1, antinode2Pos.Item2), true);
                        }

                        if (positiveSlope)
                        {
                            antinode2Pos = (antinode2Pos.Item1 - Math.Abs(xDiff), antinode2Pos.Item2 + Math.Abs(yDiff));
                        }
                        else
                        {
                            antinode2Pos = (antinode2Pos.Item1 + Math.Abs(xDiff), antinode2Pos.Item2 + Math.Abs(yDiff));
                        }
                    }
                }
            }
        }

        ans = antinodes.Keys.Count;

        for (int i = 0; i < stringArr.Count; i++)
        {
            for (int j = 0; j < stringArr[i].Length; j++)
            {
                if (antinodes.TryGetValue((j, i), out var value))
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine();
        }

        //  

        return ans.ToString();
    }
}
