using AdventOfCode.Utils;
using System.Numerics;
using System.Text;

namespace AdventOfCode.Advent2024;
internal class Advent15x2
{
    public class Box
    {
        public (int, int) LeftBracket { get; set; }
        public (int, int) RightBracket { get; set; }
    }

    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent15.txt").Select((str) => str.Count == 0 ? "" : str[0]).ToList();

        BigInteger ans = 0;

        var robotPos = (0, 0);

        var map = new Dictionary<(int, int), char>();

        var inputSb = new StringBuilder();

        var isMap = true;

        var boxes = new List<Box>();    

        for (int y = 0; y < stringArr.Count; y++)
        {
            if (stringArr[y].Length == 0)
            {
                isMap = false;
            }
            if (!isMap)
            {
                inputSb.Append(stringArr[y]);
                continue;
            }

            int newX = 0;
            for (int x = 0; x < stringArr[y].Length; x++)
            {
                if (stringArr[y][x] == '#')
                {
                    map[(newX, y)] = '#';
                    map[(newX + 1, y)] = '#';
                }

                if (stringArr[y][x] == 'O')
                {
                    map[(newX, y)] = '[';
                    map[(newX + 1, y)] = ']';
                    boxes.Add(new Box { LeftBracket = (newX, y), RightBracket = (newX + 1, y) });
                }

                if (stringArr[y][x] == '@')
                {
                    robotPos = (newX, y);
                    map[(newX, y)] = '@';
                    map[(newX + 1, y)] = '.';
                }

                if (stringArr[y][x] == '.')
                {
                    map[(newX, y)] = '.';
                    map[(newX + 1, y)] = '.';
                }
                newX+=2;
            }
        }

        var input = inputSb.ToString();

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                if (map.ContainsKey((j, i)))
                {
                    Console.Write(map[(j, i)]);
                }
            }
            Console.WriteLine();
        }

        var length = input.Length;

        var index = 0;
        foreach (var direction in input)
        {
            int y = 0;
            int x = 0;
            if (direction == '^')
            {
                y = -1;
            }
            else if (direction == 'v')
            {
                y = 1;
            }
            else if (direction == '<')
            {
                x = -1;
            }
            else if (direction == '>')
            {
                x = 1;
            }

            if (map.TryGetValue((robotPos.Item1 + x, robotPos.Item2 + y), out var value))
            {
                if (value == '.')
                {
                    map[robotPos] = '.';
                    robotPos = (robotPos.Item1 + x, robotPos.Item2 + y);
                    map[robotPos] = '@';

                }
                else if ((value == '[' || value == ']'))
                {
                    var firstBox = boxes.First(b => b.LeftBracket == (robotPos.Item1 + x, robotPos.Item2 + y) || b.RightBracket == (robotPos.Item1 + x, robotPos.Item2 + y));
                    var boxesToMove = new List<Box>();

                    if (x == 0)
                    {
                        var pos = (robotPos.Item1 + x, robotPos.Item2 + y);
                        var firstPos = pos;
                        boxesToMove.Add(firstBox);
                        var blocked = false;
                        var prevBoxRow = new List<Box>();
                        prevBoxRow.Add(firstBox);
                        while (!blocked)
                        {
                            var boxRow = new List<Box>();
                            foreach (var box in prevBoxRow)
                            {
                                if (index == 77)
                                {

                                }
                                var leftItem = (box.LeftBracket.Item1, box.LeftBracket.Item2 + y);
                                var rightItem = (box.RightBracket.Item1, box.RightBracket.Item2 + y);
                                if (map[leftItem] == '[' && map[rightItem] == ']'
                                )
                                {
                                    var nextBox = boxes.First(b => b.LeftBracket == leftItem || b.RightBracket == rightItem);
                                    if (!boxRow.Contains(nextBox))
                                    {
                                        boxRow.Add(nextBox);
                                    }
                                }
                                if (map[rightItem] == '[')
                                {
                                    var nextBox = boxes.First(b => b.LeftBracket == rightItem || b.RightBracket == rightItem);
                                    if (!boxRow.Contains(nextBox))
                                    {
                                        boxRow.Add(nextBox);
                                    }
                                }
                                if (map[leftItem] == ']')
                                {
                                    var nextBox = boxes.First(b => b.LeftBracket == leftItem || b.RightBracket == leftItem);
                                    if (!boxRow.Contains(nextBox))
                                    {
                                        boxRow.Add(nextBox);
                                    }
                                }
                                if (map[leftItem] == '#' || map[rightItem] == '#')
                                {
                                    blocked = true;
                                }
                            }

                            if (!blocked && boxRow.Count == 0)
                            {
                                foreach (var box in boxesToMove)
                                {

                                    box.LeftBracket = (box.LeftBracket.Item1 + x, box.LeftBracket.Item2 + y);
                                    box.RightBracket = (box.RightBracket.Item1 + x, box.RightBracket.Item2 + y);
                                    map[box.LeftBracket] = '[';
                                    map[box.RightBracket] = ']';
                                    var leftItem = (box.LeftBracket.Item1 - 1, box.LeftBracket.Item2);
                                    if (map.ContainsKey(leftItem) && map[leftItem] == '[')
                                    {
                                        map[(box.LeftBracket.Item1 - 1, box.LeftBracket.Item2)] = '.';
                                    }
                                    var rightItem = (box.RightBracket.Item1 + 1, box.RightBracket.Item2);
                                    if (map.ContainsKey(rightItem) && map[rightItem] == ']')
                                    {
                                        map[(box.RightBracket.Item1 + 1, box.RightBracket.Item2)] = '.';
                                    }
                                }
                                map[firstPos] = '@';
                                if (value == '[')
                                {
                                    var nextToRobot = (robotPos.Item1+1, robotPos.Item2+y);
                                    map[nextToRobot] = '.';
                                }
                                else
                                {
                                    var nextToRobot = (robotPos.Item1 - 1, robotPos.Item2 + y);
                                    map[nextToRobot] = '.';
                                }
                                map[robotPos] = '.';
                                robotPos = firstPos;
                                blocked = true;
                            }
                            else
                            {
                                boxesToMove.AddRange(boxRow);
                                prevBoxRow = boxRow;
                            }
                        }
                    }
                    else
                    {
                        var pos = (robotPos.Item1 + x, robotPos.Item2 + y);
                        var firstPos = pos;
                        boxesToMove.Add(firstBox);
                        while (map[pos] == '[' || map[pos] == ']')
                        {          
                            if (!boxesToMove.Any(b => b.LeftBracket == pos || b.RightBracket == pos))
                            {
                                var box = boxes.First(b => b.LeftBracket == pos || b.RightBracket == pos);
                                boxesToMove.Add(box);
                            }
                            pos = (pos.Item1 + x, pos.Item2 + y);
                        }
                        if (map[pos] == '.')
                        {
                            foreach (var box in boxesToMove)
                            {
                                box.LeftBracket = (box.LeftBracket.Item1 + x, box.LeftBracket.Item2 + y);
                                box.RightBracket = (box.RightBracket.Item1 + x, box.RightBracket.Item2 + y);
                                map[box.LeftBracket] = '[';
                                map[box.RightBracket] = ']';
                            }
                            map[firstPos] = '@';
                            map[robotPos] = '.';
                            robotPos = firstPos;
                        }
                    }
                }
            }
            index++;
            if (index > 20000)
            {
                Console.WriteLine(direction + " " + index);
                for (int i = 0; i < 100; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        if (map.ContainsKey((j, i)))
                        {
                            Console.Write(map[(j, i)]);
                        }
                    }
                    Console.WriteLine();
                }
            }
            
        }

        foreach (var key in map.Keys)
        {
            if (map[key] == '[')
            {
                ans += 100 * key.Item2 + key.Item1;
            }
        }

        return ans.ToString();
    }
}
