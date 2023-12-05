using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023
{
    internal class Advent5x2
    {
        public static int[] vals = new int[218];
        public static void RunOnI(List<string> line, int i)
        {
            var numsInCard = new List<int>();
            var beforePipe = true;
            int matchCount = 0;
            foreach (var str in line)
            {
                if (str == "|")
                {
                    beforePipe = false;
                }

                if (beforePipe)
                {
                    if (int.TryParse(str, out int val))
                    {
                        numsInCard.Add(val);
                    }
                }
                else
                {
                    if (int.TryParse(str, out int val))
                    {
                        if (numsInCard.Contains(val))
                        {
                            matchCount++;
                        }
                    }
                }
            }
            for (int x = 1; x < 1+matchCount; x++)
            {
                vals[i+x] += vals[i];
            }
        }

        public static string Run()
        {
            var stringArr = FileReader.ReadFile("Advent4.txt", " ");

            var ans = 0;
            vals[0] = 0;

            for (int i = 0; i < stringArr.Count; i++)
            {
                vals[i]++;
                var line = stringArr[i];

                RunOnI(line, i);
                
            }
            foreach (var val in vals)
            {
                ans += val;
            }
            return ans.ToString();
        }
    }
}
