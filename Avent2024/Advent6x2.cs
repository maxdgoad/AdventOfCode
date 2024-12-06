using AdventOfCode.Utils;

namespace AdventOfCode.Advent2024;
internal class Advent6x2
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent6.txt").Select((str) => str[0]).ToList();

        var ans = 0;

        // find up carat in the stringArr
        var caratY = stringArr.FindIndex((str) => str.Contains("^"));
        var caratX = stringArr[caratY].IndexOf("^");

        // traverse until the carat can't turn right
        var up = (0, -1);
        var right = (1, 0);
        var down = (0, 1);
        var left = (-1, 0);
        var direction = up; // (1,0) = up, (0,1) = right, (-1,0) = down, (0,-1) = left


        var startingY = caratY;
        var startingX = caratX;

        for (var i = 0; i < stringArr.Count; i++)
        {
            Console.WriteLine(i + "/" + stringArr.Count + " " + ans);
            for (var j = 0; j < stringArr[i].Length; j++)
            {
                caratY = startingY;
                caratX = startingX;
                direction = up;
                if (!(i == startingY && j == startingX) && stringArr[i][j] == '.')
                {
                    // place a wall
                    stringArr[i] = stringArr[i].Remove(j, 1).Insert(j, "#");

                    var leaves = false;
                    var visitedLocations = new List<(int, int, (int, int))>();
                    var stuck = false;

                    while (!leaves)
                    {
                        if (!visitedLocations.Contains((caratX, caratY, direction)))
                        {
                            visitedLocations.Add((caratX, caratY, direction));
                        }
                        else
                        {
                            stuck = true;
                            break;
                        }

                        var nextY = caratY + direction.Item2;
                        var nextX = caratX + direction.Item1;

                        if (nextY < 0 || nextY >= stringArr.Count || nextX < 0 || nextX >= stringArr[0].Length)
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
                            caratY = nextY;
                            caratX = nextX;
                        }
                    }

                    if (stuck)
                    {
                        ans++;
                    }

                    // remove the wall
                    stringArr[i] = stringArr[i].Remove(j, 1).Insert(j, ".");
                }
            }
        }

        

        return ans.ToString();
    }
}
