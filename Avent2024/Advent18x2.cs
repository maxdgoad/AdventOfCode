using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024
{
    internal class Advent18x2
    {
        public static List<(int, int)> bytes;
        public static List<(int, int)> bounds;
        public static List<(int, int)> bounds2;
        public static int xBounds = 71;
        public static int yBounds = 71;
        public static string Run()
        {
            var stringArr = FileReader.ReadFile("Advent18.txt").Select((str) => str[0]).ToList();

            BigInteger ans = 0;

            bytes = new List<(int, int)>();

            var totalBytes = new List<(int, int)>();

            bounds = new List<(int, int)>();
            bounds.Add((70, 70));

            int count = 0;
            for (int i = 0; i < stringArr.Count; i++)
            {
                var commaIndex = stringArr[i].IndexOf(",");
                var x = int.Parse(stringArr[i].Substring(0, commaIndex));
                var y = int.Parse(stringArr[i].Substring(commaIndex + 1));
                if (count < 1500)
                {
                    bytes.Add((x, y));
                    count++;

                }
                totalBytes.Add((x, y));
            }

            for (int i = count; i < 3450; i++)
            {
                var byteToAdd = totalBytes[i];
                bytes.Add(byteToAdd);

                var initPos1 = (0, 0);
                var seenLocs = new HashSet<(int, int)>();
                var seen = Traverse(initPos1, initPos1, seenLocs, bounds);

                //for (int y = 0; y < yBounds; y++)
                //{
                //    for (int x = 0; x < xBounds; x++)
                //    {
                //        if (bytes.Contains((x, y)))
                //        {
                //            if (byteToAdd == (x, y))
                //            {
                //                Console.ForegroundColor = ConsoleColor.Red;
                //                Console.Write("#");
                //                Console.ForegroundColor = ConsoleColor.White;
                //            }
                //            else
                //            {
                //                Console.Write("#");
                //            }
                //        }
                //        else if (seenLocs.Contains((x, y)))
                //        {
                //            Console.ForegroundColor = ConsoleColor.Green;
                //            Console.Write("O");
                //            Console.ForegroundColor = ConsoleColor.White;
                //        }
                //        else
                //        {
                //            Console.Write(".");
                //        }
                //    }
                //    Console.WriteLine();
                //}
                //Console.WriteLine();

                if (seen)
                {
                    Console.WriteLine($"Found: {totalBytes[i]}, {i}");
                }
                if (!seen)
                {
                    Console.WriteLine($"Failed: {totalBytes[i]}, {i}");
                    break;
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }

            return ans.ToString();
        }

        public static bool Traverse((int, int) currentPos, (int, int) previousPos, HashSet<(int, int)> seen, List<(int, int)> bounds)
        {
            if (bounds.Contains(currentPos))
            {
                return true;
            }

            var up = (currentPos.Item1, currentPos.Item2 - 1);
            var right = (currentPos.Item1 + 1, currentPos.Item2);
            var down = (currentPos.Item1, currentPos.Item2 + 1);
            var left = (currentPos.Item1 - 1, currentPos.Item2);

            var directions = new List<(int, int)>() { down, left, right, up };

            seen.Add(currentPos);
            var isFound = false;
            foreach (var direction in directions)
            {
                if (direction != previousPos && !bytes.Contains(direction) && !seen.Contains(direction))
                {
                    if (direction.Item1 >= 0 && direction.Item2 >= 0 && direction.Item1 < xBounds && direction.Item2 < yBounds)
                    {

                        isFound |= Traverse(direction, currentPos, seen, bounds);
                        
                    }
                }
            }

            return isFound;
        }
    }
}
