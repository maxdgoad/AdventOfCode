using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024
{
    internal class Advent22x1
    {
        public static string Run()
        {
            var stringArr = FileReader.ReadFile("Advent22.txt").Select((str) => str[0]).ToList();

            BigInteger ans = 0;

            var secretNumbers = new List<BigInteger>();
            foreach (var str in stringArr)
            {
                if (BigInteger.TryParse(str, out var num))
                {
                    secretNumbers.Add(num);
                }
            }

            for (int x = 0; x < secretNumbers.Count; x++)
            {
                var secretNumber = secretNumbers[x];
                for (int i = 0; i < 2000;  i++)
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
                    secretNumber = mixed3;
                }
                secretNumbers[x] = secretNumber;
            }

            foreach (var secretNumber in secretNumbers)
            {
                ans += secretNumber;
                Console.WriteLine(secretNumber);
            }

            return ans.ToString();
        }
    }
}
