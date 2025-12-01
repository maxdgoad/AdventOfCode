using AdventOfCode.Utils;

namespace AdventOfCode.Advent2025;

internal class Day01x01
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent1.txt", " ");

        var ans = 0;

        var leftList = new List<int>();
        var rightList = new List<int>();

        for (int i = 0; i < stringArr.Count; i++)
        {
            leftList.Add(int.Parse(stringArr[i][0]));
            rightList.Add(int.Parse(stringArr[i][1]));
        }

        leftList.Sort();
        rightList.Sort();

        for (int i = 0; i < leftList.Count(); i++)
        {
            ans += Math.Abs(leftList[i] - rightList[i]);
        }

        return ans.ToString();
    }
}
