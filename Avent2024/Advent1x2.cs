using AdventOfCode.Utils;

namespace AdventOfCode.Advent2024;
internal class Advent1x2
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent1.txt", " ");

        var ans = 0;

        var leftList = new List<int>();
        var rightList = new Dictionary<int, int>();

        for (int i = 0; i < stringArr.Count; i++)
        {
            leftList.Add(int.Parse(stringArr[i][0]));

            if (rightList.ContainsKey(int.Parse(stringArr[i][1])))
            {
                rightList[int.Parse(stringArr[i][1])]++;
            }
            else
            {
                rightList.Add(int.Parse(stringArr[i][1]), 1);
            }
        }

        for (int i = 0; i < leftList.Count(); i++)
        {
            if (rightList.ContainsKey(leftList[i]))
            {
                ans += leftList[i] * rightList[leftList[i]]; 
            }
        }

        return ans.ToString();
    }
}
