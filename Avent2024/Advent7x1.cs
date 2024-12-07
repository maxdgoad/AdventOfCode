using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024;
internal class Advent7x1
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent7.txt").Select((str) => str[0]).ToList();

        BigInteger ans = 0;

        foreach (var line in stringArr)
        {
            var colonIndex = line.IndexOf(":");
            var goal = BigInteger.Parse(line.Substring(0, colonIndex));

            var vals = line.Substring(colonIndex + 2).Split(" ").Select(int.Parse).ToList();

            var canMakeGoal = CanMakeGoal(goal, vals, 0);

            if (canMakeGoal)
            {
                ans += goal;
            }

        }

        
        return ans.ToString();
    }

    public static bool CanMakeGoal(BigInteger goal, List<int> vals, BigInteger runningTotal)
    {
        if (vals.Count == 1)
        {
            return runningTotal + vals[0] == goal || runningTotal * vals[0] == goal;
        }

        return CanMakeGoal(goal, vals[1..], runningTotal + vals[0]) || CanMakeGoal(goal, vals[1..], runningTotal * vals[0]);
    }
}
