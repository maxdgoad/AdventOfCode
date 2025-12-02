using System.Diagnostics;

Console.WriteLine("Starting program:");
Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();
var output = AdventOfCode.Advent2025.Day01.Part1.Run();
Console.WriteLine($"Answer: {output}");
Console.WriteLine($"Ran in {stopwatch.Elapsed.TotalSeconds} seconds");
