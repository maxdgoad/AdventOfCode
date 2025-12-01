using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024;
internal class Advent14x2
{
    public class Robot
    {
        public (int, int) InitialPosition { get; set; }
        public int Recurrence { get; set; }
        public (int, int) Position { get; set; }
        public (int, int) Velocity { get; set; }
        public int TreeStep { get; set; }
    }

    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent14.txt").Select((str) => str.Count == 0 ? "" : str[0]).ToList();

        BigInteger ans = 0;

        var robots = new List<Robot>();

        foreach (var str in stringArr)
        {
            var firstComma = str.IndexOf(',');
            var x = int.Parse(str.Substring(2, firstComma - 2));

            var velocityIndex = str.IndexOf('v');
            var y = int.Parse(str.Substring(firstComma + 1, velocityIndex - firstComma - 1));

            var secondComma = str.IndexOf(',', velocityIndex);

            var xVel = int.Parse(str.Substring(velocityIndex + 2, secondComma - velocityIndex - 2));
            var yVel = int.Parse(str.Substring(secondComma + 1, str.Length - secondComma - 1));

            var robot = new Robot();
            robot.InitialPosition = (x, y);
            robot.Recurrence = 0;
            robot.TreeStep = 0;
            robot.Position = (x, y);
            robot.Velocity = (xVel, yVel);
            robots.Add(robot);
        }

        var dimX = 101;
        var dimY = 103;
        var halfX = (dimX - 1) / 2;

        var treePositions = new HashSet<(int, int)>();

        treePositions.Add((halfX, 0));
        for (int y = 1; y < (dimX-1)/2; y++)
        {
            treePositions.Add((halfX - y, y));
            treePositions.Add((halfX + y, y));
        }

        double lowXVar = 1000;
        double lowYVar = 1000;

        for (int t = 1; t <= 10404; t++)
        {
            var positions = new Dictionary<(int, int), int>();
            foreach (var robot in robots)
            {
                var newX = robot.Position.Item1 + robot.Velocity.Item1;
                var newY = robot.Position.Item2 + robot.Velocity.Item2;

                if (newX < 0)
                {
                    newX = dimX + newX;
                }

                if (newY < 0)
                {
                    newY = dimY + newY;
                }
                robot.Position = (newX % dimX, newY % dimY);

                if (robot.Position == robot.InitialPosition && robot.Recurrence == 0)
                {
                    robot.Recurrence = t;
                }

                if (robot.Recurrence == 0 && treePositions.Contains(robot.Position))
                {
                    robot.TreeStep = t;
                }

                if (positions.ContainsKey(robot.Position))
                {
                    positions[robot.Position]++;
                }
                else
                {
                    positions[robot.Position] = 1;
                }
            }

            var meanX = positions.Keys.Select(k => k.Item1).Average();
            var meanY = positions.Keys.Select(k => k.Item2).Average();

            var varianceX = positions.Keys.Select(k => k.Item1).Select(x => (x - meanX) * (x - meanX)).Sum() / positions.Keys.Count;
            var varianceY = positions.Keys.Select(k => k.Item2).Select(y => (y - meanY) * (y - meanY)).Sum() / positions.Keys.Count;

            var shouldPrint = varianceX < lowXVar && varianceY < lowYVar;
            if (shouldPrint)
            {
                lowXVar = varianceX;
                lowYVar = varianceY;

                Console.WriteLine($"t={t}, varX={varianceX}, varY={varianceY}");
                for (int y = 0; y < dimY; y++)
                {
                    for (int x = 0; x < dimX; x++)
                    {
                        if (positions.ContainsKey((x, y)))
                        {
                            Console.Write(positions[(x, y)]);
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
            }   
        }

        //foreach (var robot in robots)
        //{
        //    Console.WriteLine($"Robot at {robot.InitialPosition} tree={robot.TreeStep} and recurrence={robot.Recurrence}");   
        //}

        return ans.ToString();
    }
}
