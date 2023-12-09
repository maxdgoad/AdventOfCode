using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent8x2Better
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent8.txt", " ");
        long ans = 0;

        var path = new Dictionary<string, (string, string)>();
        for (int rep = 2; rep < stringArr.Count; rep++)
        {
            var key = stringArr[rep][0];
            var left = stringArr[rep][2].Substring(1, 3);
            var right = stringArr[rep][3].Substring(0, 3);

            path.Add(key, (left, right));
        }

        var instructions = stringArr[0][0];
        var currentNodes = path.Keys.Where(key => key[2] == 'A').ToList();
        var frequencies = new long[currentNodes.Count];

        int count = 0;
        while (frequencies.Any(val => val == 0))
        {
            var nextNodes = new List<string>();
            char instr = instructions[count % instructions.Length];
            count++;
            for (int rep = 0; rep < currentNodes.Count; rep++)
            {
                var node = currentNodes[rep];
                var nextNode = instr == 'L' ? path[node].Item1 : path[node].Item2;
                nextNodes.Add(nextNode);
                if (nextNode[2] == 'Z')
                {
                    frequencies[rep] = (count / instructions.Length);
                }
            }
            currentNodes = nextNodes;
        }
        ans = instructions.Length * frequencies.Aggregate((a, f) => a * f);
        return ans.ToString();
    }
}
