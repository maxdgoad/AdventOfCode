using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024;
internal class Advent14x1
{
    public class Robot
    {
        public (int, int) Position { get; set; }
        public (int, int) Velocity { get; set; }
    }

    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent14.txt").Select((str) => str.Count == 0 ? "" : str[0]).ToList();

        BigInteger ans = 0;

        var robots = new List<Robot>();

        foreach (var str in stringArr)
        {
            var firstComma = str.IndexOf(',');
            var x = int.Parse(str.Substring(2, firstComma-2));

            var velocityIndex = str.IndexOf('v');
            var y = int.Parse(str.Substring(firstComma + 1, velocityIndex - firstComma - 1));

            var secondComma = str.IndexOf(',', velocityIndex);

            var xVel = int.Parse(str.Substring(velocityIndex + 2, secondComma - velocityIndex - 2));
            var yVel = int.Parse(str.Substring(secondComma + 1, str.Length - secondComma - 1));

            var robot = new Robot();
            robot.Position = (x, y);
            robot.Velocity = (xVel, yVel);
            robots.Add(robot);
        }

        var dimX = 101;
        var dimY = 103;
        for (int t = 0; t < 100; t++)
        {
            foreach (var robot in robots)
            {
                //if (robot.Velocity.Item1 == 2 && robot.Velocity.Item2 == -3)
                //{
                //    Console.WriteLine($"({robot.Position.Item1}, {robot.Position.Item2}) t={t}, ({robot.Position.Item1 % dimX}, {robot.Position.Item2 % dimY})");
                //}
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

                robot.Position = (newX, newY);
            }
        }
        var quadrant1 = 0;
        var quadrant2 = 0;
        var quadrant3 = 0;
        var quadrant4 = 0;
        foreach (var robot in robots)
        {
            var posX = robot.Position.Item1 % dimX;
            var posY = robot.Position.Item2 % dimY;

            if (posX != (dimX - 1) / 2 && posY != (dimY - 1) / 2)
            {
                if (posX < (dimX - 1) / 2)
                {
                    if (posY < (dimY - 1) / 2)
                    {
                        quadrant1++;
                    }
                    else
                    {
                        quadrant3++;
                    }
                }
                else
                {
                    if (posY < (dimY - 1) / 2)
                    {
                        quadrant2++;
                    }
                    else
                    {
                        quadrant4++;
                    }
                }
            }
        }

        ans = quadrant1 * quadrant2 * quadrant3 * quadrant4;

        return ans.ToString();
    }
}
