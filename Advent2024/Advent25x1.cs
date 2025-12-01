using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024
{
    internal class Advent25x1
    {
        public static string Run()
        {
            var stringArr = FileReader.ReadFile("Advent25.txt").ToList();

            BigInteger ans = 0;

            var keyBlocks = new List<List<List<char>>>();
            var lockBlocks = new List<List<List<char>>>();
            var currentKeyBlock = new List<List<char>>();
            var currentLockBlock = new List<List<char>>();

            bool? lockBlock = null;
            for (int i = 0; i < stringArr.Count; i++)
            {
                var fullRow = stringArr[i];
                if (fullRow.Count == 0)
                {
                    if (currentKeyBlock.Count > 0)
                    {
                        keyBlocks.Add(currentKeyBlock.ToArray().ToList());
                    }
                    if (currentLockBlock.Count > 0)
                    {
                        lockBlocks.Add(currentLockBlock.ToArray().ToList());
                    }
                    currentLockBlock.Clear();
                    currentKeyBlock.Clear();
                    lockBlock = null;
                    continue;
                }
                else
                {
                    var row = fullRow[0];
                    if (lockBlock == null)
                    {
                        if (row == ".....")
                        {
                            lockBlock = false;
                        }
                        else if (row == "#####")
                        {
                            lockBlock = true;
                        }
                    }

                    if (lockBlock ?? false)
                    {
                        currentLockBlock.Add(row.ToCharArray().ToList());
                    }
                    else
                    {
                        currentKeyBlock.Add(row.ToCharArray().ToList());
                    }
                }
            }
            if (currentKeyBlock.Count > 0)
            {
                keyBlocks.Add(currentKeyBlock.ToArray().ToList());
            }
            if (currentLockBlock.Count > 0)
            {
                lockBlocks.Add(currentLockBlock.ToArray().ToList());
            }

            var keys = new HashSet<(int, int, int, int, int)>();
            var locks = new HashSet<(int, int, int, int, int)>();

            foreach (var key in keyBlocks)
            {
                var heights = new List<int>();
                for (int i = 0; i < 5; i++)
                {
                    var height = 6;
                    for (int j = 0; j < 6; j++)
                    {
                        if (key[j][i] != '#')
                        {
                            height--;
                        }
                    }
                    heights.Add(height);
                }
                keys.Add((heights[0], heights[1], heights[2], heights[3], heights[4]));
            }

            foreach (var lok in lockBlocks)
            {
                var heights = new List<int>();
                for (int i = 0; i < 5; i++)
                {
                    var height = 0;
                    for (int j = 1; j < 7; j++)
                    {
                        if (lok[j][i] == '#')
                        {
                            height++;
                        }
                    }
                    heights.Add(height);
                }
                locks.Add((heights[0], heights[1], heights[2], heights[3], heights[4]));
            }

            var fit = 0;
            foreach (var lok in locks)
            {
                foreach (var key in keys)
                {
                    if (lok.Item1 + key.Item1 < 6 
                        && lok.Item2 + key.Item2 < 6 
                        && lok.Item3 + key.Item3 < 6 
                        && lok.Item4 + key.Item4 < 6 
                        && lok.Item5 + key.Item5 < 6)
                    {
                        fit++;
                    }
                }
            }
            ans = fit;
            return ans.ToString();
        }
    }
}
