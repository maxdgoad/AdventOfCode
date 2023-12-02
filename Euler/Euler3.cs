using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Euler
{
    internal static class Euler3
    {
        public static string Run()
        {
            string ans = "";

            var primeFactors = new List<long>();

            long startingVal = 232792560;
            var largestFactor = startingVal;
            for (long i = 2; i <= largestFactor;)
            {
                if (primeFactors.Any(p => i % p == 0))
                {
                    
                }
                else if (startingVal % i == 0)
                {
                    primeFactors.Add(i);
                    largestFactor = (long)Math.Ceiling((decimal)largestFactor / (decimal)(i));
                }
                i++;
            }

            ans = string.Join(",", primeFactors);

            return ans;
        }
    }
}
