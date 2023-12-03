using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023
{
    internal class Advent3x2
    {
        public static bool ColorVal(string str, string color, out int val)
        {
            if (str.Contains(color))
            {
                var tempColor = str.Substring(0, str.Length - color.Length).Trim();

                if (int.TryParse(tempColor, out int colorCount))
                {
                    val = colorCount;
                    return true;
                }
            }
            val = -1;
            return false;
        }

        public static string Run()
        {
            var stringArr = FileReader.ReadFile("Advent2.txt");

            var ans = 0;

            foreach (var line in stringArr)
            {

                string lineStr = (line.FirstOrDefault() ?? "");
                lineStr = lineStr.Substring(lineStr.IndexOf(":") + 1);

                var secondParse = lineStr.Split(new char[] { ',', ';' });

                int maxRed = 0;
                int maxGreen = 0;
                int maxBlue = 0;

                foreach (var str in secondParse)
                {
                    if (ColorVal(str, "red", out var redCount))
                    {
                        if (redCount > maxRed)
                        {
                            maxRed = redCount;
                        }
                    }

                    if (ColorVal(str, "green", out var greenCount))
                    {
                        if (greenCount > maxGreen)
                        {
                            maxGreen = greenCount;
                        }
                    }

                    if (ColorVal(str, "blue", out var blueCount))
                    {
                        if (blueCount > maxBlue)
                        {
                            maxBlue = blueCount;
                        }
                    }
                }
                ans += maxRed * maxGreen * maxBlue;
            }
            return ans.ToString();
        }

    }
}
