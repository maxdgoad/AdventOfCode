using AdventOfCode.Advent2024;
using System.Diagnostics;

Console.WriteLine("Starting program:");
Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();
var output = Advent24x2.Run();
Console.WriteLine($"Answer: {output}");
Console.WriteLine($"Ran in {stopwatch.Elapsed.TotalSeconds} seconds");
