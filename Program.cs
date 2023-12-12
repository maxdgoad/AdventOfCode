using AdventOfCode.Advent2023;
using System.Diagnostics;

Console.WriteLine("Starting program:");
Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();
var output = Advent12x1.Run();
Console.WriteLine($"Answer: {output}");
Console.WriteLine($"Program ran in {stopwatch.Elapsed.TotalSeconds} seconds");
