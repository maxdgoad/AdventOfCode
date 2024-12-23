using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024
{
    internal class Advent20x1
    {
        public static string Run()
        {
            var stringArr = FileReader.ReadFile("Advent20.txt").Select((str) => str[0]).ToList();

            BigInteger ans = 0;

            var map = new Dictionary<(int, int), char>();

            for (int i = 0; i < stringArr.Count; i++)
            {
                for (int j = 0; j < stringArr[i].Length; j++)
                {
                    map[(i, j)] = stringArr[i][j];
                }
            }

            var path = new Dictionary<(int, int), int>();
            var initPos = map.First((kvp) => kvp.Value == 'S').Key;
            Traverse(map, initPos, path, 0);

            List<int> diffs = new List<int>();
            foreach (var kvp in path)
            {
                var loc = kvp.Key;
                var locX = loc.Item1;
                var locY = loc.Item2;

                if (path.TryGetValue((locX-2, locY), out var val))
                {
                    if (val - kvp.Value - 2 >= 100)
                        diffs.Add(val - kvp.Value - 2);
                }
                if (path.TryGetValue((locX + 2, locY), out var val2))
                {
                    if (val2 - kvp.Value - 2 >= 100)
                        diffs.Add(val2 - kvp.Value - 2);
                }
                if (path.TryGetValue((locX, locY - 2), out var val3))
                {
                    if (val3 - kvp.Value - 2 >= 100)
                        diffs.Add(val3 - kvp.Value - 2);
                }
                if (path.TryGetValue((locX, locY + 2), out var val4))
                {
                    if (val4 - kvp.Value - 2 >= 100)
                        diffs.Add(val4 - kvp.Value - 2);
                }
            }
            diffs.Sort();
            foreach (var diff in diffs)
            {
                Console.WriteLine(diff);
            }
            ans = diffs.Count;
            return ans.ToString();
        }

        public static void Traverse(Dictionary<(int, int), char> map, (int, int) position,
            Dictionary<(int, int), int> path, int runningCount)
        {
            path.Add(position, runningCount);

            var up = (position.Item1, position.Item2 + 1);
            var down = (position.Item1, position.Item2 - 1);
            var left = (position.Item1 - 1, position.Item2);
            var right = (position.Item1 + 1, position.Item2);

            if (map.ContainsKey(up) && map[up] != '#' && !path.ContainsKey(up))
            {
                path[position] = runningCount;
                Traverse(map, up, path, runningCount+1);
            }
            else if (map.ContainsKey(down) && map[down] != '#' && !path.ContainsKey(down))
            {
                path[position] = runningCount;
                Traverse(map, down, path, runningCount + 1);
            }
            else if (map.ContainsKey(left) && map[left] != '#' && !path.ContainsKey(left))
            {
                path[position] = runningCount;
                Traverse(map, left, path, runningCount+1);
            }
            else if (map.ContainsKey(right) && map[right] != '#' && !path.ContainsKey(right))
            {
                path[position] = runningCount;
                Traverse(map, right, path, runningCount + 1);
            }
            
        }
    }
}
