using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent2x1
{

    public static bool StillGood(string str, string color, int maxCount)
    {
        if (str.Contains(color))
        {
            var tempColor = str.Substring(0, str.Length - color.Length).Trim();

            if (int.TryParse(tempColor, out int colorCount))
            {
                if (colorCount > maxCount)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent2.txt");

        var ans = 0;
        var gameId = 0;

        foreach (var line in stringArr)
        {
            var goodGame = true;
            gameId = gameId + 1;


            string lineStr = (line.FirstOrDefault() ?? "");
            lineStr = lineStr.Substring(lineStr.IndexOf(":") + 1);

            var secondParse = lineStr.Split(new char[] { ',', ';' });

            foreach (var str in secondParse)
            {
                goodGame = StillGood(str, "red", 12) && StillGood(str, "green", 13) && StillGood(str, "blue", 14);
                if (!goodGame)
                {
                    break;
                }
            }

            if (goodGame)
            {
                ans += gameId;
            }

        }
        return ans.ToString();
    }
}
