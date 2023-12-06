using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023
{
    internal class Advent5x1
    {
        public static string Run()
        {
            var stringArr = FileReader.ReadFile("Advent5.txt", " ");

            long ans = 0;

            var seeds = stringArr[0][1..].Select(x => long.Parse(x)).ToList();

            var maps = new List<(List<(long, long)>, List<(long, long)>)>();

            var headers = stringArr.Where(x => !long.TryParse(x[0], out var blah)).Select(x => (x[0], stringArr.IndexOf(x))).ToList();

            for (int headerNum = 0; headerNum < headers.Count; headerNum++)
            {
                var currentHeaderIndex = headers[headerNum].Item2;
                var nextHeaderIndex = headerNum < headers.Count - 1 ? headers[headerNum + 1].Item2 : stringArr[1..].Count - 1;

                var inputRanges = new List<(long, long)>();
                var outputRanges = new List<(long, long)>();
                var headerName = headers[headerNum].Item1;
                for (int i = currentHeaderIndex + 1; i < nextHeaderIndex; i++)
                {
                    var isOutputStart = long.TryParse(stringArr[i][0], out var outputStart);
                    var isInputStart = long.TryParse(stringArr[i][1], out var inputStart);
                    var isRange = long.TryParse(stringArr[i][2], out var range);

                    if (isOutputStart && isInputStart && isRange)
                    {
                        inputRanges.Add((inputStart, inputStart + range));
                        outputRanges.Add((outputStart, outputStart + range));
                    }
                }
                maps.Add((inputRanges, outputRanges));
            }

            foreach (var map in maps)
            {
                var newSeeds = new List<long>();
                var seedsCopy = seeds.ToList();
                for (int i = 0; i < map.Item1.Count; i++)
                {
                    var (inputStart, inputEnd) = map.Item1[i];
                    var (outputStart, outputEnd) = map.Item2[i];
                    foreach (var seed in seeds)
                    {
                        if (seed >= inputStart && seed <= inputEnd)
                        {
                            newSeeds.Add(outputStart + seed - inputStart);
                            seedsCopy.Remove(seed);
                        }
                    }
                }
                foreach (var seed in seedsCopy)
                {
                    newSeeds.Add(seed);
                }
                seeds = newSeeds;
            }
            ans = seeds.Min(x => x);
            return ans.ToString();
        }
    }
}
