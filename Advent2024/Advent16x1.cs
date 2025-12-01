using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024;
internal class Advent16x1
{
    public static int total = 0;
    public static int maxDepth = 0;
    public static string Run()
    {
        var stringArr = FileReader.ReadFileCharArray("Advent16.txt");

        BigInteger ans = 0;

        var reindeerPos = (0, 0);
        var reindeerDirection = (1, 0);

        for (int i = 0; i < stringArr.Length; i++)
        {
            for (int j = 0; j < stringArr[i].Length; j++)
            {
                if (stringArr[i][j] == 'S')
                {
                    reindeerPos = (j, i);
                }
            }
        }


        ans = Traverse(reindeerPos, reindeerPos, reindeerDirection, stringArr, 0, new HashSet<(int, int)>(), "", 0, new Dictionary<(int, int), BigInteger>());

        return ans.ToString();
    }

    public static BigInteger Traverse((int, int) reindeerPos, (int, int) previousPos, (int, int) reindeerDirection, char[][] stringArr, 
        BigInteger runningTotal, HashSet<(int, int)> seen, string path, int depth, Dictionary<(int, int), BigInteger> totalSeen)
    {
        total++;
        if (depth > maxDepth)
        {
            maxDepth = depth;
        }
        if (depth > 800)
        {
            return BigInteger.Pow(2, 32);
        }
        

        if (totalSeen.ContainsKey(reindeerPos) && totalSeen[reindeerPos] < runningTotal)
        {
            return BigInteger.Pow(2, 32);
        }
        if (stringArr[reindeerPos.Item2][reindeerPos.Item1] == 'E')
        {
            return runningTotal;
        }

        var up = (reindeerPos.Item1, reindeerPos.Item2 - 1);
        var right = (reindeerPos.Item1 + 1, reindeerPos.Item2);
        var down = (reindeerPos.Item1, reindeerPos.Item2 + 1);
        var left = (reindeerPos.Item1 - 1, reindeerPos.Item2);

        seen.Add(reindeerPos);
        totalSeen[reindeerPos] = runningTotal;
        var min = BigInteger.Pow(2, 32);
        if (up != previousPos && stringArr[up.Item2][up.Item1] != '#' && !seen.Contains(up))
        {
            var upVal = BigInteger.Pow(2, 32);
            var diff = ((up.Item1 - reindeerPos.Item1), (up.Item2 - reindeerPos.Item2));
            if (diff == reindeerDirection)
            {
                var newSeen = new HashSet<(int, int)>(seen);
                upVal = Traverse(up, reindeerPos, diff, stringArr, runningTotal + 1, newSeen, path + "UP(1) ", depth+1, totalSeen);
                if (upVal < min)
                {
                    min = upVal;
                }
            }
            else
            {
                var newSeen = new HashSet<(int, int)>(seen);
                upVal = Traverse(up, reindeerPos, diff, stringArr, runningTotal + 1001, newSeen, path + "UP(1001) ", depth + 1, totalSeen);
                if (upVal < min)
                {
                    min = upVal;
                }
            }
        }
        if (down != previousPos && stringArr[down.Item2][down.Item1] != '#' && !seen.Contains(down))
        {
            var downVal = BigInteger.Pow(2, 32);
            var diff = ((down.Item1 - reindeerPos.Item1), (down.Item2 - reindeerPos.Item2));
            if (diff == reindeerDirection)
            {
                var newSeen = new HashSet<(int, int)>(seen);
                downVal = Traverse(down, reindeerPos, diff, stringArr, runningTotal + 1, newSeen, path + "DOWN(1) ", depth + 1, totalSeen);
                if (downVal < min)
                {
                    min = downVal;
                }
            }
            else
            {
                var newSeen = new HashSet<(int, int)>(seen);
                downVal = Traverse(down, reindeerPos, diff, stringArr, runningTotal + 1001, newSeen, path + "DOWN(1001) ", depth + 1, totalSeen);
                if (downVal < min)
                {
                    min = downVal;
                }
            }
        }

        if (right != previousPos && stringArr[right.Item2][right.Item1] != '#' && !seen.Contains(right))
        {
            var rightVal = BigInteger.Pow(2, 32);
            var diff = ((right.Item1 - reindeerPos.Item1), (right.Item2 - reindeerPos.Item2));
            if (diff == reindeerDirection)
            {
                var newSeen = new HashSet<(int, int)>(seen);
                rightVal = Traverse(right, reindeerPos, diff, stringArr, runningTotal + 1, newSeen, path + "RIGHT(1) ", depth + 1, totalSeen);
                if (rightVal < min)
                {
                    min = rightVal;
                }
            }
            else
            {
                var newSeen = new HashSet<(int, int)>(seen);
                rightVal = Traverse(right, reindeerPos, diff, stringArr, runningTotal + 1001, newSeen, path + "RIGHT(1001) ", depth + 1, totalSeen);
                if (rightVal < min)
                {
                    min = rightVal;
                }
            }
        }

        if (left != previousPos && stringArr[left.Item2][left.Item1] != '#' && !seen.Contains(left))
        {
            var leftVal = BigInteger.Pow(2, 32);
            var diff = ((left.Item1 - reindeerPos.Item1), (left.Item2 - reindeerPos.Item2));
            if (diff == reindeerDirection)
            {
                var newSeen = new HashSet<(int, int)>(seen);
                leftVal = Traverse(left, reindeerPos, diff, stringArr, runningTotal + 1, newSeen, path + "LEFT(1) ", depth + 1, totalSeen);
                if (leftVal < min)
                {
                    min = leftVal;
                }
            }
            else
            {
                var newSeen = new HashSet<(int, int)>(seen);
                leftVal = Traverse(left, reindeerPos, diff, stringArr, runningTotal + 1001, newSeen, path + "LEFT(1001) ", depth + 1, totalSeen);
                if (leftVal < min)
                {
                    min = leftVal;
                }
            }
        }

        return min;
    }
}
