using AdventOfCode.Utils;
using System.Numerics;
using System.Runtime.InteropServices;
using static AdventOfCode.Advent2024.Advent19x1;

namespace AdventOfCode.Advent2024
{
    internal class Advent19x2
    {
        public static HashSet<string> towels = new HashSet<string>();
        public static string Run()
        {
            var stringArr = FileReader.ReadFile("Advent19.txt").Select((str) => str[0]).ToList();

            BigInteger ans = 0;

            var patterns = stringArr[0].Split(", ");
            for (int i = 0; i < patterns.Length; i++)
            {
                towels.Add(patterns[i]);
            }

            for (int x = 1; x < stringArr.Count; x++)
            {
                ans += FindMatch(stringArr[x]);
            }
            return ans.ToString();
        }

        public static Dictionary<string, BigInteger> memo = new Dictionary<string, BigInteger>();

        public static BigInteger FindMatch(string str)
        {
            if (str == "")
            {
                return 1;
            }
            if (memo.ContainsKey(str))
            {
                return memo[str];
            }
            BigInteger found = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (towels.Contains(str.Substring(0, i+1)))
                {
                    found += FindMatch(str.Substring(i+1));
                }
            }
            memo[str] = found;
            return found;
        }
    }
}
