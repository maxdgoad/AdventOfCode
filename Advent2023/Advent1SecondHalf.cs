using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023
{
    internal class Advent1SecondHalf
    {

        static List<string> validDigits = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        
        public static bool CurrentIndexIsDigit(string substr, out int val)
        {
            for (int i = 0; i < validDigits.Count; i++) 
            {
                if (substr.IndexOf(validDigits[i]) == 0)
                {
                    val = i + 1;
                    return true;
                }
            }
            return int.TryParse(substr.Substring(0, 1), out val);
        }
        
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
                    if (!first.HasValue && CurrentIndexIsDigit(lineStr.Substring(i), out var posFirst))
                    { 
                        first = posFirst;
                    }
                    if (!second.HasValue && CurrentIndexIsDigit(lineStr.Substring(lineStr.Length - i - 1), out var posSecond))
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
