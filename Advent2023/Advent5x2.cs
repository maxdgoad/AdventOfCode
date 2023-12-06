using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent5x2
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent5.txt", " ");

        long ans = 0;

        var initRanges = stringArr[0][1..].Select(x => long.Parse(x)).ToList();

        List<(long, long)> seeds = new List<(long, long)>();
        for (int i = 0; i < initRanges.Count; i += 2)
        {
            seeds.Add((initRanges[i], initRanges[i] + initRanges[i + 1]));
        }

        var maps = new List<(List<(long, long)>, List<(long, long)>)>();

        var headers = stringArr.Where(x => !long.TryParse(x[0], out var blah)).Select(x => (x[0], stringArr.IndexOf(x))).ToList();

        for (int headerNum = 1; headerNum < headers.Count; headerNum++)
        {
            var currentHeaderIndex = headers[headerNum].Item2;
            var nextHeaderIndex = headerNum < headers.Count - 1 ? headers[headerNum + 1].Item2 : stringArr[1..].Count - 1;

            var inputRanges = new List<(long, long)>();
            var outputRanges = new List<(long, long)>();
            var headerName = headers[headerNum].Item1;
            var indexes = new List<(long, long, int)>();
            for (int i = currentHeaderIndex + 1; i < nextHeaderIndex; i++)
            {
                var isOutputStart = long.TryParse(stringArr[i][0], out var outputStart);
                var isInputStart = long.TryParse(stringArr[i][1], out var inputStart);
                var isRange = long.TryParse(stringArr[i][2], out var range);

                if (isOutputStart && isInputStart && isRange)
                {
                    inputRanges.Add((inputStart, inputStart + range - 1));
                    outputRanges.Add((outputStart, outputStart + range - 1));
                }
                indexes.Add((inputStart, inputStart + range - 1, i - currentHeaderIndex - 1));
            }
            // need to sort the input ranges
            inputRanges = inputRanges.OrderBy(x => x.Item1).ToList();
            var sortedOutputRanges = new List<(long, long)>();
            foreach (var input in inputRanges)
            {
                var index = indexes.FirstOrDefault(x => x.Item1 == input.Item1 && x.Item2 == input.Item2);
                var matchingOutput = outputRanges[index.Item3];
                sortedOutputRanges.Add(matchingOutput);
            }
            maps.Add((inputRanges, sortedOutputRanges));
        }

        foreach (var map in maps)
        {
            var newSeeds = new List<(long, long)>();
            for (int s = 0; s < seeds.Count; s++)
            {
                var seed = seeds[s];
                bool mapped = false;
                for (int i = 0; i < map.Item1.Count; i++)
                {
                    var (inputStart, inputEnd) = map.Item1[i];
                    var (outputStart, outputEnd) = map.Item2[i];

                    if (inputEnd >= seed.Item2 && inputStart <= seed.Item1)
                    {
                        newSeeds.Add((seed.Item1 - inputStart + outputStart, seed.Item2 - inputStart + outputStart));
                        mapped = true;
                        break;
                    }
                    else if (inputStart > seed.Item2)
                    {
                        newSeeds.Add(seed);
                        mapped = true;
                        break;
                    }
                    else if (inputEnd < seed.Item1)
                    {
                        // Nothing
                        mapped = false;
                    }
                    else
                    {
                        if (inputStart < seed.Item1)
                        {
                            newSeeds.Add((seed.Item1 - inputStart + outputStart, outputEnd));
                            seed.Item1 = inputEnd + 1;
                            newSeeds.Add(seed);
                            mapped = true;
                        }
                        else if (inputEnd < seed.Item2)
                        {
                            newSeeds.Add((outputStart, seed.Item2 - inputStart + outputStart));
                            seed.Item2 = inputStart - 1;
                            mapped = false;
                            break;
                        }
                    }
                }
                if (!mapped)
                {
                    newSeeds.Add(seed);
                }
            }
            seeds = newSeeds;
            var distance = seeds.Select(x => x.Item2 - x.Item1).Sum();
        }
        var sortedList = seeds.OrderBy(x => x.Item1).ToList();
        ans = seeds.Min(x => x.Item1);
        return ans.ToString();
    }
}
