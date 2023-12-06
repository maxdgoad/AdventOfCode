using AdventOfCode.Utils;

namespace AdventOfCode.Euler;
internal static class Euler67
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Triangle.txt");

        var intArr = new List<List<int>>();

        for (int i = 0; i < stringArr.Count; i++)
        {
            intArr.Add(new List<int>());
            for (int j = 0; j < stringArr[i].Count; j++)
            {
                intArr[i].Add(int.Parse(stringArr[i][j]));
            }
        }

        for (int i = intArr.Count - 2; i >= 0; i--)
        {
            for (int j = 0; j < intArr[i].Count; j++)
            {
                if (intArr[i + 1][j] > intArr[i + 1][j + 1])
                {
                    intArr[i][j] = intArr[i + 1][j] + intArr[i][j];
                }
                else
                {
                    intArr[i][j] = intArr[i + 1][j + 1] + intArr[i][j];
                }
            }
        }

        return intArr[0][0].ToString();
    }
}
