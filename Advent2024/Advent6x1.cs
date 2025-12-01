using AdventOfCode.Utils;

namespace AdventOfCode.Advent2024;
internal class Advent6x1
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent6.txt").Select((str) => str[0]).ToList();

        var ans = 1;

        // find up carat in the stringArr
        var caratY = stringArr.FindIndex((str) => str.Contains("^"));
        var caratX = stringArr[caratY].IndexOf("^");

        var up = (0, -1);
        var right = (1, 0);
        var down = (0, 1);
        var left = (-1, 0);
        var direction = up; 

        var leaves = false;

        var visitedLocations = new List<(int, int)>();
        while (!leaves)
        {
            var nextY = caratY + direction.Item2;
            var nextX = caratX + direction.Item1;

            if (nextY < 0 || nextY >= stringArr.Count || nextX < 0 || nextX > stringArr[0].Length)
            {
                leaves = true;
            }
            else if (stringArr[nextY][nextX] == '#')
            {
                // turn
                if (direction == up)
                {
                    direction = right;
                }
                else if (direction == right)
                {
                    direction = down;
                }
                else if (direction == down)
                {
                    direction = left;
                }
                else if (direction == left)
                {
                    direction = up;
                }
            }
            else
            {
                if (!visitedLocations.Contains((caratX, caratY)))
                {
                    visitedLocations.Add((caratX, caratY));
                    ans++;
                }
                caratY = nextY;
                caratX = nextX;
            }         
        }

        return ans.ToString();
    }
}
