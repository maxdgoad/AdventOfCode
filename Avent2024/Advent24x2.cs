﻿using AdventOfCode.Utils;
using System.Numerics;
using System.Runtime.InteropServices;

namespace AdventOfCode.Advent2024
{
    internal class Advent24x2
    {
        public class Gate
        {
            public Gate Left { get; set; }
            public Gate Right { get; set; }

            public string LeftName { get; set; }
            public string RightName { get; set; }
            public string Op { get; set; }
            public string GateName { get; set; }

            public int Value => Op switch
            {
                "AND" => Left.Value & Right.Value,
                "OR" => Left.Value | Right.Value,
                "XOR" => Left.Value ^ Right.Value,
                "ONE" => 1,
                "ZERO" => 0,
            };
        }

        public static string Run()
        {
            var stringArr = FileReader.ReadFile("Advent24.txt").Select((str) => str[0]).ToList();

            BigInteger ans = 0;

            var gates = new List<Gate>();
            var rand = new Random(1993);
            foreach (var str in stringArr)
            {
                if (str.Contains(":"))
                {
                    var vals = str.Split(":");
                    var gate = vals[0];
                    var value = rand.Next(0, 2); //int.Parse(vals[1].Trim());
                    var baseGate = new Gate { GateName = gate, Op = value == 1 ? "ONE" : "ZERO" };
                    gates.Add(baseGate);
                }
                else
                {
                    var vals = str.Split(" ");

                    var lhs = vals[0];
                    var op = vals[1];
                    var rhs = vals[2];
                    var gateName = vals[4];

                    var gate = new Gate { GateName = gateName, Op = op, LeftName = vals[0], RightName = vals[2] };
                    gates.Add(gate);
                }
            }

            foreach (var gate in gates)
            {
                if (gate.RightName?.Length > 0)
                {
                    gate.Right = gates.FirstOrDefault(g => g.GateName == gate.RightName);
                }
                if (gate.LeftName?.Length > 0)
                {
                    gate.Left = gates.FirstOrDefault(g => g.GateName == gate.LeftName);
                }
            }

            gates.Sort((a, b) => a.GateName.CompareTo(b.GateName));

            BigInteger xVal = 0;
            int xCount = 0;
            BigInteger yVal = 0;
            int yCount = 0;

            var xBinary = new List<int>();
            var yBinary = new List<int>();

            foreach (var gate in gates)
            {
                if (gate.GateName[0] == 'x')
                {
                    xBinary.Add(gate.Value);
                    if (gate.Value == 1)
                    {
                        xVal += BigInteger.Pow(2, xCount);
                    }
                    xCount++;
                }
            }
            xBinary.Reverse();

            foreach (var gate in gates)
            {
                if (gate.GateName[0] == 'y')
                {
                    yBinary.Add(gate.Value);
                    if (gate.Value == 1)
                    {
                        yVal += BigInteger.Pow(2, yCount);
                    }
                    yCount++;
                }
            }
            yBinary.Reverse();

            ans = yVal + xVal;
            var ansBinary = new List<int>();
            for (int i = 45; i >= 0; i--)
            {
                if (ans - BigInteger.Pow(2, i) > 0)
                {
                    ansBinary.Add(1);
                    ans -= BigInteger.Pow(2, i);
                }
                else
                {
                    ansBinary.Add(0);
                }
            }
            ansBinary.Reverse();
            var zAns = ansBinary;

            BigInteger zVal = 0;
            int zCount = 0;
           
            foreach (var gate in gates)
            {
                if (gate.GateName[0] == 'z')
                {
                    if (gate.Value.ToString() != zAns[zCount].ToString())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine();

                    Console.Write(gate.Value + " - " + zAns[zCount] + "  ");

                    PrintGates(gate, 1);

                    Console.ForegroundColor = ConsoleColor.White;

                    if (gate.Value == 1)
                    {
                        zVal += BigInteger.Pow(2, zCount);
                    }
                    zCount++;
                }
            }

            Console.WriteLine(xVal);
            Console.WriteLine(yVal);
            Console.WriteLine(zVal);

            ans = yVal + xVal;

            return ans.ToString();
        }

        public static void PrintGates(Gate gate, int depth)
        {
            if (gate.Left != null && gate.Right != null)
            {
                for (int i = 0; i < depth; i++)
                {
                    Console.Write("\t");
                }
                Console.WriteLine(gate.GateName + ": " + gate.Left.GateName + " " + gate.Op + " " + gate.Right.GateName);
                PrintGates(gate.Left, depth + 1);
                PrintGates(gate.Right, depth + 1);
            }
        }
    }
}
