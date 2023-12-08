using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent8x2
{
    // https://en.wikipedia.org/wiki/Least_common_multiple#Using_prime_factorization
    private static long LCM(List<long> values)
    {
        var uniquePrimeFactors = new HashSet<long>();
        foreach (long v in values)
        {
            var factors = Primes.PrimeFactors(8);
            factors.ForEach(factor => uniquePrimeFactors.Add(factor));
        }
        return uniquePrimeFactors.Aggregate((a, f) => a * f);
    }
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent8.txt", " ");

        long ans = 0;

        var path = new Dictionary<string, (string, string)>();

        var instructions = stringArr[0][0];

        for (int rep = 2; rep < stringArr.Count; rep++)
        {
            var key = stringArr[rep][0];
            var left = stringArr[rep][2].Substring(1, 3);
            var right = stringArr[rep][3].Substring(0, 3);

            path.Add(key, (left, right));
        }

        int count = 0;

        var currentNodes = path.Keys.Where(key => key[2] == 'A').ToList();

        //var currentNodes = new List<string>() { "AAA" };

        var frequencies = new long[currentNodes.Count];

        while (frequencies.Any(val => val == 0))
        {
            var nextNodes = new List<string>();
            char instr = instructions[count % instructions.Length];
            count++;
            for (int rep = 0; rep < currentNodes.Count; rep++)
            {
                var node = currentNodes[rep];
                var nextNode = "";
                if (instr == 'L')
                {
                    nextNode = path[node].Item1;
                }
                else
                {
                    nextNode = path[node].Item2;                
                }
                nextNodes.Add(nextNode);
                if (nextNode[2] == 'Z')
                {
                    frequencies[rep] = (count);
                }
            }
            currentNodes = nextNodes;
        }
        ans = LCM(frequencies.ToList());
        return ans.ToString();
    }
}
