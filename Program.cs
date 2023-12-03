using AdventOfCode.Advent2023;
using AdventOfCode.Euler;
using AdventOfCode.Utils;
using System.Diagnostics;

Console.WriteLine("Starting program:");

Stopwatch stopwatch = new Stopwatch();

stopwatch.Start();

var output = Advent2x2.Run();

Console.WriteLine($"Answer: {output}");

var endingTime = stopwatch.Elapsed;
Console.WriteLine($"Program ran in {endingTime.TotalSeconds} seconds");



