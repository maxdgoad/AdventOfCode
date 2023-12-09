using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent8x1
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent8.txt", " ");

        long ans = 0;

        var path = new Dictionary<string, (string, string)>();
        var instructions = stringArr[0][0];

        for (int rep = 2; rep < stringArr.Count; rep++)
        {
            var key = stringArr[rep][0];
            var left = stringArr[rep][2].Substring(1, 3);
            var right = stringArr[rep][3].Substring(0, 3);

            path.Add(key, (left, right));
        }

        int count = 0;
        bool foundZZZ = false;
        string currentNode = "AAA";
        while (!foundZZZ)
        {
            char instr = instructions[count % instructions.Length];
            count++;
            if (instr == 'L')
            {
                currentNode = path[currentNode].Item1;
            } 
            else
            {
                currentNode = path[currentNode].Item2;
            }
            if (currentNode == "ZZZ")
            {
                foundZZZ = true;
            }
        }
        ans = count;
        return ans.ToString();
    }
}
