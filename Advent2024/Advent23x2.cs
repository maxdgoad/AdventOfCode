using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024
{
    internal class Advent23x2
    {
        public class Computer
        {
            public string Name { get; set; }
            public List<Computer> Connections { get; set; } = new List<Computer>();
        }

        public static string Run()
        {
            var stringArr = FileReader.ReadFile("Advent23.txt").Select((str) => str[0]).ToList();

            BigInteger ans = 0;

            var computers = new List<Computer>();
            foreach (var connection in stringArr)
            {
                var split = connection.Split("-");

                var computer1 = computers.FirstOrDefault(c => c.Name == split[0]);
                if (computer1 == null)
                {
                    computer1 = new Computer { Name = split[0] };
                    computers.Add(computer1);
                }

                var computer2 = computers.FirstOrDefault(c => c.Name == split[1]);
                if (computer2 == null)
                {
                    computer2 = new Computer { Name = split[1] };
                    computers.Add(computer2);
                }

                computer1.Connections.Add(computer2);
                computer2.Connections.Add(computer1);
            }

            var nodeCount = 0;
            var largestNodes = new HashSet<Computer>();
            foreach (var computer1 in computers)
            {
                var nodes = new HashSet<Computer> { computer1 };
                foreach (var computer2 in computer1.Connections)
                {
                    foreach (var computer3 in computer2.Connections)
                    {
                        if (computer3.Connections.Contains(computer1))
                        {
                            nodes.Add(computer3);
                        }
                    }
                }

                var isValid = true;
                foreach (var node1 in nodes)
                {
                    foreach (var node2 in nodes)
                    {
                        if (node1 != node2 && !node1.Connections.Contains(node2))
                        {
                            isValid = false;
                            break;
                        }
                    }
                }

                if (isValid)
                {
                    if (nodes.Count > nodeCount)
                    {
                        nodeCount = nodes.Count;
                        largestNodes = nodes;
                    }
                }
            }

            foreach (var computer in computers)
            {
                Console.WriteLine($"{computer.Name}: {string.Join(", ", computer.Connections.Select(c => c.Name))}");
            }
            var nodeList = largestNodes.ToList();
            nodeList.Sort((a, b) => a.Name.CompareTo(b.Name));
            Console.WriteLine($"{string.Join(",", nodeList.Select(c => c.Name))}");
            ans = largestNodes.Count;

            return ans.ToString();
        }
    }
}
