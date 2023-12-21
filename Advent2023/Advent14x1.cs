using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent14x1
{

    public static string Run()
    {
        long ans = 0;
        var stringArr = FileReader.ReadFile("Advent14.txt");

        for (int i = 1; i < stringArr.Count; i++)
        {         
            for (int j = 0; j < stringArr[i][0].Length; j++)
            {
                int gamer = i;
                while (gamer > 0 && stringArr[gamer - 1][0][j] == '.' && stringArr[gamer][0][j] == 'O')
                {
                    // move the rock down
                    stringArr[gamer - 1][0] = stringArr[gamer - 1][0].Substring(0, j) + "O" + stringArr[gamer - 1][0].Substring(j + 1);
                    // replace rock with dang-ol empty
                    stringArr[gamer][0] = stringArr[gamer][0].Substring(0, j) + "." + stringArr[gamer][0].Substring(j + 1);
                    gamer--;
                }
            }
        }

        for (int i = stringArr.Count; i > 0; i--)
        {
            ans += i * stringArr[stringArr.Count - i][0].Where(s => s == 'O').Count();
        }

        return ans.ToString();
    }
}
