using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024
{
    internal class Advent22x2
    {
        public static string Run()
        {
            var stringArr = FileReader.ReadFile("Advent22.txt").Select((str) => str[0]).ToList();

            BigInteger ans = 0;

            var secretNumbers = new List<BigInteger>();

            var sequenceMaps = new Dictionary<BigInteger, Dictionary<(int, int, int, int), int>>();
            foreach (var str in stringArr)
            {
                if (BigInteger.TryParse(str, out var num))
                {
                    secretNumbers.Add(num);
                }
            }

            for (int x = 0; x < secretNumbers.Count; x++)
            {
                var last4 = new List<int> { 0, 0, 0, 0 };
                var secretNumber = secretNumbers[x];
                for (int i = 0; i < 2000; i++)
                {
                    BigInteger newSecret = secretNumber * 64;
                    BigInteger mixed = newSecret ^ secretNumber;
                    mixed %= 16777216;

                    BigInteger newSecret2 = mixed / 32;
                    BigInteger mixed2 = newSecret2 ^ mixed;
                    mixed2 %= 16777216;

                    BigInteger newSecret3 = mixed2 * 2048;
                    BigInteger mixed3 = newSecret3 ^ mixed2;
                    mixed3 %= 16777216;


                    if (i > -1)
                    {
                        var lastDigit = (int)(mixed3 % 10);
                        var previousLastDigit = (int)(secretNumber % 10);
                        var diff = lastDigit - previousLastDigit;
                        //Console.WriteLine($"{mixed3}: {lastDigit} ({diff})");

                        last4 = last4.Skip(1).ToList();
                        last4.Add(diff);
                        var last4Tuple = (last4[0], last4[1], last4[2], last4[3]);

                        if (i >= 3)
                        {
                            if (!sequenceMaps.ContainsKey(secretNumbers[x]))
                            {
                                sequenceMaps[secretNumbers[x]] = new Dictionary<(int, int, int, int), int>();
                            }
                            
                            if (!sequenceMaps[secretNumbers[x]].ContainsKey(last4Tuple))
                            {
                                sequenceMaps[secretNumbers[x]][last4Tuple] = lastDigit;
                            }
                        }
                        
                    }
                    secretNumber = mixed3;
                }
                secretNumbers[x] = secretNumber;
            }

            var highest = 0;
            var highestKey = (0, 0, 0, 0);

            var allSequences = sequenceMaps.SelectMany(x => x.Value.Keys).ToHashSet();

            Console.WriteLine(allSequences.Count);

            foreach (var sequence in allSequences)
            {
                var total = 0;
                foreach (var sequenceMap in sequenceMaps)
                {
                    if (sequenceMap.Value.ContainsKey(sequence))
                    {
                        total += sequenceMap.Value[sequence];
                    }
                }
                if (total > highest)
                {
                    highest = total;
                    highestKey = sequence;
                }
            }

            ans = highest;
            Console.WriteLine(highestKey);

            return ans.ToString();
        }
    }
}
