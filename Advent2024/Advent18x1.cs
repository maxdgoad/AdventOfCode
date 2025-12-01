using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024
{
    internal class Advent18x1
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

            int count = 0;
            for (int i = 0; i < stringArr.Count; i++)
            {
                if (count < 1024)
                {
                    var commaIndex = stringArr[i].IndexOf(",");
                    var x = int.Parse(stringArr[i].Substring(0, commaIndex));
                    var y = int.Parse(stringArr[i].Substring(commaIndex + 1));

                    bytes.Add((x, y));
                    count++;

                }  
            }
            bytes.Add((21, 2));

            // set boundary
            bounds = new List<(int, int)>();
            bounds2 = new List<(int, int)>();

            for (int i = 0; i < yBounds; i++)
            {
                for (int k = 0; k < xBounds; k++)
                {
                    if (bytes.Contains((k, i)))
                    {
                        Console.Write("#");
                    }
                    //else if (k == 21 && i == 17)
                    //{
                    //    Console.ForegroundColor = ConsoleColor.Red;
                    //    Console.Write("S");
                    //    Console.ForegroundColor = ConsoleColor.White;
                    //}
                    //else if (k == 42 && i == 49)
                    //{
                    //    Console.ForegroundColor = ConsoleColor.Red;
                    //    Console.Write("S");
                    //    Console.ForegroundColor = ConsoleColor.White;
                    //}
                    else if ((i == 30 && k < 9)
                        || (k == 9 && i < 30 && i > 20)
                        || (i == 22 && k > 9 && k < 20)
                        || (k == 21 && i == 20))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("E");
                        Console.ForegroundColor = ConsoleColor.White;
                        bounds.Add((k, i));
                    }
                    else if ((k > 30 && i == 40)
                        || (k == 10 && i > 43)
                        || (k == 21 && i > 0 && i < 21)
                        || (i == 49 && k > 34 && k < 45))
                    {
                        bytes.Add((k, i));
                        Console.Write("#");
                    }
                    else if (
                         (i == 49 && k > 10 && k < 35)
                        || (k == 46 && i > 29 && i < 44)
                        || (i == 30 && k > 46 && k < 55)
                        || (k == 54 && i > 29 && i < 45)
                        || (i == 45 && k > 55))
                        
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("E");
                        Console.ForegroundColor = ConsoleColor.White;
                        bounds2.Add((k, i));
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();



            var initPos1 = (0, 0);
            var totalSeen1 = new Dictionary<(int, int), BigInteger>();
            var (min1, minLoc1, minPath1) = Traverse(initPos1, initPos1, 0, new HashSet<(int, int)>(), totalSeen1, bounds);

            var initPos2 = (70, 70);
            var totalSeen2 = new Dictionary<(int, int), BigInteger>();
            var (min2, minLoc2, minPath2) = Traverse(initPos2, initPos2, 0, new HashSet<(int, int)>(), totalSeen2, bounds2);


            for (int i = 0; i < yBounds; i++)
            {
                for (int k = 0; k < xBounds; k++)
                {
                    if (bytes.Contains((k, i)))
                    {
                        Console.Write("#");
                    }
                    else if (minLoc1 == (k, i) || minLoc2 == (k, i))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("S");
                        Console.ForegroundColor = ConsoleColor.White;
                        ans++;
                    }
                    else if (minPath1.Contains((k, i)) || minPath2.Contains((k, i)))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("O");
                        Console.ForegroundColor = ConsoleColor.White;
                        ans++;
                    }
                    else if (k < 31 && k >= 21 && i == 44)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("O");
                        Console.ForegroundColor = ConsoleColor.White;
                        ans++;
                    }
                    else if (k == 21 && i > 17 && i < 44)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("O");
                        Console.ForegroundColor = ConsoleColor.White;
                        ans++;
                    }
                    else if (k == 31 && i >= 44 && i < 49)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("O");
                        Console.ForegroundColor = ConsoleColor.White;
                        ans++;
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            ans--;
            return ans.ToString();
        }

        public static (BigInteger, (int,int), HashSet<(int, int)>) Traverse((int, int) currentPos, (int, int) previousPos,
        BigInteger runningTotal, HashSet<(int, int)> seen, Dictionary<(int, int), BigInteger> totalSeen, List<(int, int)> bounds)
        {
            //for (int i = 0; i < yBounds; i++)
            //{
            //    for (int k = 0; k < xBounds; k++)
            //    {
            //        if (bytes.Contains((k, i)))
            //        {
            //            Console.Write("#");
            //        }
            //        else if (seen.Contains((k, i)))
            //        {
            //            Console.Write("O");
            //        }
            //        else
            //        {
            //            Console.Write(".");
            //        }
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();


            if (totalSeen.ContainsKey(currentPos) && totalSeen[currentPos] < runningTotal)
            {
                return (BigInteger.Pow(2, 32), currentPos, seen);
            }
            if (bounds.Contains(currentPos))
            {
                return (runningTotal, currentPos, seen);
            }

            var up = (currentPos.Item1, currentPos.Item2 - 1);
            var right = (currentPos.Item1 + 1, currentPos.Item2);
            var down = (currentPos.Item1, currentPos.Item2 + 1);
            var left = (currentPos.Item1 - 1, currentPos.Item2);

            var directions = new List<(int, int)>() { down, left, right, up };

            seen.Add(currentPos);
            totalSeen[currentPos] = runningTotal;
            var min = BigInteger.Pow(2, 32);
            var minLoc = (0, 0);
            var minSeen = new HashSet<(int, int)>();

            foreach (var direction in directions)
            {
                if (direction != previousPos && !bytes.Contains(direction) && !seen.Contains(direction))
                {
                    if (direction.Item1 >= 0 && direction.Item2 >= 0 && direction.Item1 < xBounds && direction.Item2 < yBounds)
                    {
                        var newVal = BigInteger.Pow(2, 32);

                        var newSeen = new HashSet<(int, int)>(seen);
                        (newVal, var newLoc, newSeen) = Traverse(direction, currentPos, runningTotal + 1, newSeen, totalSeen, bounds);
                        if (newVal < min)
                        {
                            min = newVal;
                            minLoc = newLoc;
                            minSeen = newSeen;
                        }
                    }    
                }
            }

            return (min, minLoc, minSeen);
        }
    }
}
