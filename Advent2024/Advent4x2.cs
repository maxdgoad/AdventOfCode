using AdventOfCode.Utils;

namespace AdventOfCode.Advent2024;
internal class Advent4x2
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent4.txt").Select(str => str[0]).ToList();

        var ans = 0;

        for (int i = 0; i < stringArr.Count; i++)
        {
            for (int j = 0; j < stringArr[i].Length; j++)
            {

                if (stringArr[i][j] == 'A')
                {

                    // Find the 4 diags and check if corresponding ones are S or M
                    try
                    {
                        var topLeft = stringArr[i - 1][j - 1];
                        var topRight = stringArr[i - 1][j + 1];
                        var botLeft = stringArr[i + 1][j - 1];
                        var botRight = stringArr[i + 1][j + 1];

                        if (topLeft == 'S' && topRight == 'M' && botLeft == 'S' && botRight == 'M')
                        {
                            ans++;
                        }
                        else if (topLeft == 'M' && topRight == 'S' && botLeft == 'M' && botRight == 'S')
                        {
                            ans++;
                        }
                        else if (topLeft == 'S' && topRight == 'S' && botLeft == 'M' && botRight == 'M')
                        {
                            ans++;
                        }
                        else if (topLeft == 'M' && topRight == 'M' && botLeft == 'S' && botRight == 'S')
                        {
                            ans++;
                        }
                    }
                    catch (Exception ex)
                    {
                        /* swallow exception */
                    }
                }

            }
        }

        return ans.ToString();
    }
}
