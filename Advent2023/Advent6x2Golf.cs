using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent6x2Golf
{
    public static long Run()
    {
        var td = FileReader.ReadFile("Advent6.txt", " ").Select(sa => long.Parse(string.Concat(sa[1..]))).ToList();
        return Enumerable.Range(0, (int)td[0]).Aggregate(0, (a, i) => a + (i * (td[0] - i) > td[1] ? 1 : 0));
    }
}
