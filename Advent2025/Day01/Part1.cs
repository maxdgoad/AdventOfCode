using AdventOfCode.Utils;

namespace AdventOfCode.Advent2025.Day01;

internal class Part1
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Day01\\advent.txt", " ");
        var instructions = stringArr.Select(x => x[0]).ToList();

        var ans = 0;
        var arrow = 50;
        foreach (var instruction in instructions)
        {
            var distance = int.Parse(instruction[1..]);
            arrow = arrow + (instruction[0] == 'L' ? -1 : 1) * distance;
            ans = ans + (arrow % 100 == 0 ? 1 : 0);
            Console.WriteLine($"{instruction} : {arrow}");
        }

        return ans.ToString();
    }
}
