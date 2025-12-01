using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024
{
    internal class Advent23x1
    {
        public class Computer
        {
            public string Name { get; set; }
            public bool StartsWithT => Name.StartsWith("t");
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

            var totalConnections = new HashSet<(Computer, Computer, Computer)>();
            var totalTConnections = new HashSet<(Computer, Computer, Computer)>();
            foreach (var computer1 in computers)
            {
                foreach (var computer2 in computer1.Connections)
                {
                    foreach (var computer3 in computer2.Connections)
                    {
                        if (computer3.Connections.Contains(computer1))
                        {
                            if (computer1 != computer2 && computer2 != computer3 && computer1 != computer3)
                            {
                                var sortedComputers = new List<Computer> { computer1, computer2, computer3 };
                                sortedComputers.Sort((a, b) => a.Name.CompareTo(b.Name));
                                totalConnections.Add((sortedComputers[0], sortedComputers[1], sortedComputers[2]));

                                if (computer1.StartsWithT || computer2.StartsWithT || computer3.StartsWithT)
                                {
                                    totalTConnections.Add((sortedComputers[0], sortedComputers[1], sortedComputers[2]));
                                }
                            }
                        }       
                    }
                }
            }

            ans = totalTConnections.Count;

            return ans.ToString();
        }
    }
}
