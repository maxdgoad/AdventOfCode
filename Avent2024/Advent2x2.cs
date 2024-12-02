using AdventOfCode.Utils;

namespace AdventOfCode.Advent2024;
internal class Advent2x2
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent2.txt");

        var ans = 0;

        foreach (var report in stringArr)
        {
            var levels = report[0].Split(" ");

            var safeFound = false;

            for (int removed = -1; removed < levels.Length; removed++)
            {
                var levelsToTry = levels.ToList();
                if (removed >= 0)
                {
                    levelsToTry = levelsToTry.Where((source, index) => index != removed).ToList();
                }


                var safe = true;
                var lastLevel = int.Parse(levelsToTry[0]);
                bool? increasing = lastLevel < int.Parse(levelsToTry[1]);
                for (int i = 1; i < levelsToTry.Count; i++)
                {
                    if (increasing.HasValue && (increasing.Value && lastLevel > int.Parse(levelsToTry[i])
                        || (!increasing.Value && lastLevel < int.Parse(levelsToTry[i]))))
                    {
                        safe = false;
                        break;
                    }
                    else if ((Math.Abs(int.Parse(levelsToTry[i]) - lastLevel)) < 1
                        || Math.Abs(int.Parse(levelsToTry[i]) - lastLevel) > 3)
                    {
                        safe = false;
                        break;
                    }
                    lastLevel = int.Parse(levelsToTry[i]);
                }
                if (safe)
                {
                    safeFound = true; break;
                }
            }

            if (safeFound)
            {
                ans++;
            }
        }

        return ans.ToString();
    }
}
