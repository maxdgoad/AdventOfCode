using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023
{
    internal class Advent4x1
    {
        public static string Run()
        {
            var stringArr = FileReader.ReadFile("Advent4.txt", " ");

            var ans = 0;

            foreach (var line in stringArr)
            {
                var numsInCard = new List<int>();
                var beforePipe = true;
                int runningCount = 0;
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
                                if (runningCount == 0)
                                {
                                    runningCount = 1;
                                }
                                else
                                {
                                    runningCount *= 2;
                                }
                            }
                        }
                    }
                }
                ans += runningCount;
            }

            return ans.ToString();
        }
    }
}
