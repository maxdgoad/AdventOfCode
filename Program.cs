
using AdventOfCode;
using System.Diagnostics;

Console.WriteLine("Starting program:");

Stopwatch stopwatch = new Stopwatch();

stopwatch.Start();

var output = Euler67.Run();

Console.WriteLine(output);

var endingTime = stopwatch.Elapsed;
Console.WriteLine($"Program ran in {endingTime.TotalSeconds} seconds");



