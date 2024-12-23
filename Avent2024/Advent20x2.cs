using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024
{
    internal class Advent20x2
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

            Dictionary<((int, int), (int, int)), int> cheats = new Dictionary<((int, int), (int, int)), int>();
            foreach (var kvp in path)
            {
                var loc = kvp.Key;
                var locX = loc.Item1;
                var locY = loc.Item2;

                for (int xDir = 0; xDir <= 20; xDir++)
                {
                    for (int yDir = 0; yDir <= 20-xDir; yDir++)
                    {
                        var loc1 = (locX + xDir, locY + yDir);
                        if (path.TryGetValue(loc1, out var val))
                        {
                            var diff = val - kvp.Value - (xDir + yDir);
                            if (diff >= 100)
                            {
                                cheats[((locX, locY), loc1)] = diff;
                            }
                        }

                        var loc2 = (locX + xDir, locY - yDir);
                        if (path.TryGetValue(loc2, out var val2))
                        {
                            var diff = val2 - kvp.Value - (xDir + yDir);
                            if (diff >= 100)
                            {
                                cheats[((locX, locY), loc2)] = diff;
                            }
                        }

                        var loc3 = (locX - xDir, locY + yDir);
                        if (path.TryGetValue(loc3, out var val3))
                        {
                            var diff = val3 - kvp.Value - (xDir + yDir);
                            if (diff >= 100)
                            {
                                cheats[((locX, locY), loc3)] = diff;
                            }
                        }

                        var loc4 = (locX - xDir, locY - yDir);
                        if (path.TryGetValue(loc4, out var val4))
                        {
                            var diff = val4 - kvp.Value - (xDir + yDir);
                            if (diff >= 100)
                            {
                                cheats[((locX, locY), loc4)] = diff;
                            }
                        }
                    }
                }

            }
            foreach (var diff in cheats)
            {
                Console.WriteLine(diff);
            }
            ans = cheats.Count;
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
                Traverse(map, up, path, runningCount + 1);
            }
            else if (map.ContainsKey(down) && map[down] != '#' && !path.ContainsKey(down))
            {
                path[position] = runningCount;
                Traverse(map, down, path, runningCount + 1);
            }
            else if (map.ContainsKey(left) && map[left] != '#' && !path.ContainsKey(left))
            {
                path[position] = runningCount;
                Traverse(map, left, path, runningCount + 1);
            }
            else if (map.ContainsKey(right) && map[right] != '#' && !path.ContainsKey(right))
            {
                path[position] = runningCount;
                Traverse(map, right, path, runningCount + 1);
            }

        }
    }
}
