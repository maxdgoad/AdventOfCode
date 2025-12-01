using AdventOfCode.Utils;

namespace AdventOfCode.Advent2024;
internal class Advent4x1
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent4.txt").Select(str => str[0]).ToList();

        var ans = 0;

        for (int i = 0; i < stringArr.Count; i++)
        {
            for (int j = 0; j < stringArr[i].Length; j++)
            {
                
                if (stringArr[i][j] == 'X')
                {
                    // Check the 8 directions

                    // horizontal
                    try
                    {
                        if (stringArr[i].Substring(j, 4) == "XMAS")
                        {
                            ans++;
                        }
                    } 
                    catch (Exception ex)
                    {
                        /* swallow exception */
                    }

                    try
                    {
                        if (stringArr[i].Substring(j - 3, 4) == "SAMX")
                        {
                            ans++;
                        }
                    }
                    catch (Exception ex)
                    {
                        /* swallow exception */
                    }



                    // veritcal

                    try
                    {
                        var bot1 = stringArr[i + 1][j];
                        var bot2 = stringArr[i + 2][j];
                        var bot3 = stringArr[i + 3][j];

                        if (bot1 == 'M' && bot2 == 'A' && bot3 == 'S')
                        {
                            ans++;
                        }
                    }
                    catch (Exception ex)
                    {
                        /* swallow exception */
                    }

                    

                    

                    try
                    {
                        var top1 = stringArr[i - 1][j];
                        var top2 = stringArr[i - 2][j];
                        var top3 = stringArr[i - 3][j];

                        if (top1 == 'M' && top2 == 'A' && top3 == 'S')
                        {
                            ans++;
                        }
                    }
                    catch (Exception ex)
                    {
                        /* swallow exception */
                    }

                    try
                    {
                        var topL1 = stringArr[i - 1][j - 1];
                        var topL2 = stringArr[i - 2][j - 2];
                        var topL3 = stringArr[i - 3][j - 3];

                        if (topL1 == 'M' && topL2 == 'A' && topL3 == 'S')
                        {
                            ans++;
                        }
                    }
                    catch (Exception ex)
                    {
                        /* swallow exception */
                    }

                    // diagonal top


                    try
                    {
                        var topR1 = stringArr[i - 1][j + 1];
                        var topR2 = stringArr[i - 2][j + 2];
                        var topR3 = stringArr[i - 3][j + 3];

                        if (topR1 == 'M' && topR2 == 'A' && topR3 == 'S')
                        {
                            ans++;
                        }
                    }
                    catch (Exception ex)
                    {
                        /* swallow exception */
                    }


                    // diagonal bottom

                    try
                    {
                        var botL1 = stringArr[i + 1][j - 1];
                        var botL2 = stringArr[i + 2][j - 2];
                        var botL3 = stringArr[i + 3][j - 3];

                        if (botL1 == 'M' && botL2 == 'A' && botL3 == 'S')
                        {
                            ans++;
                        }
                    }
                    catch (Exception ex)
                    {
                        /* swallow exception */
                    }

                    try
                    {
                        var botR1 = stringArr[i + 1][j + 1];
                        var botR2 = stringArr[i + 2][j + 2];
                        var botR3 = stringArr[i + 3][j + 3];

                        if (botR1 == 'M' && botR2 == 'A' && botR3 == 'S')
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
