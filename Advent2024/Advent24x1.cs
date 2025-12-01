using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024
{
    internal class Advent24x1
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

            foreach (var str in stringArr)
            {
                if (str.Contains(":"))
                {
                    var vals = str.Split(":");
                    var gate = vals[0];
                    var value = int.Parse(vals[1].Trim());
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

                    var left = gates.FirstOrDefault(g => g.GateName == lhs);
                    var right = gates.FirstOrDefault(g => g.GateName == rhs);

                    var gate = new Gate { GateName = gateName, Left = left, Right = right, Op = op, LeftName = vals[0], RightName = vals[2] };
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
            BigInteger val = 0;
            int count = 0;
            foreach (var gate in gates)
            {
                if (gate.GateName[0] == 'z')
                {
                    Console.WriteLine(gate.GateName + " " + gate.Value);
                    if (gate.Value == 1)
                    {
                        val += BigInteger.Pow(2, count);
                    }
                    count++;
                }
            }
            ans = val;
            return ans.ToString();
        }
    }
}
