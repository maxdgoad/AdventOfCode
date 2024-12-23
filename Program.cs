﻿using AdventOfCode.Advent2024;
using System.Diagnostics;

Console.WriteLine("Starting program:");
Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();
var output = Advent21x1.Run();
Console.WriteLine($"Answer: {output}");
Console.WriteLine($"Ran in {stopwatch.Elapsed.TotalSeconds} seconds");
