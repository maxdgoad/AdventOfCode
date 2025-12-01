using AdventOfCode.Utils;

namespace AdventOfCode.Advent2024;
internal class Advent2x1
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent2.txt");

        var ans = 0;

        foreach (var report in stringArr)
        {
            var levels = report[0].Split(" ");

            var safe = true;
            var lastLevel = int.Parse(levels[0]);
            bool? increasing = lastLevel < int.Parse(levels[1]);
            for (int i = 1; i < levels.Length; i++)
            {
                if (increasing.HasValue && (increasing.Value && lastLevel > int.Parse(levels[i])
                    || (!increasing.Value && lastLevel < int.Parse(levels[i]))))
                {
                    safe = false;
                    break;
                }
                else if ((Math.Abs(int.Parse(levels[i]) - lastLevel)) < 1
                    || Math.Abs(int.Parse(levels[i]) - lastLevel) > 3)
                {
                    safe = false;
                    break;
                }
                lastLevel = int.Parse(levels[i]);
            }

            if (safe)
            {
                ans++;
            }
        }

        return ans.ToString();
    }
}
