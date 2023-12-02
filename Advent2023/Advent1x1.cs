using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023
{
    internal class Advent1x1
    {
        public static string Run()
        {
            var stringArr = FileReader.ReadFile("Advent1.txt");

            var ans = 0;

            foreach(var line in stringArr)
            {
                int? first = null;
                int? second = null;

                string lineStr = (line.FirstOrDefault() ?? "");
                for (int i = 0; i < lineStr.Length; i++)
                {
                    if (!first.HasValue && int.TryParse(lineStr.Substring(i, 1), out var posFirst))
                    { 
                        first = posFirst;
                    }
                    if (!second.HasValue && int.TryParse(lineStr.Substring(lineStr.Length - i - 1, 1), out var posSecond))
                    {
                        second = posSecond;
                    }
                }

                if (first.HasValue && second.HasValue)
                {
                    Console.WriteLine(string.Join("", line), first.Value, second.Value);
                    ans += 10 * first.Value + second.Value;
                }
                else
                {
                    Console.WriteLine($"Could not find value for {line}");
                    return "help";
                }
            }
            return ans.ToString();
        }
    }
}
