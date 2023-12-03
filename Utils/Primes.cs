using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Utils
{
    internal static class Primes
    {
        public static bool IsPrime(long n)
        {
            if (n == 1) return false;

            long i = 2;
            while (i * i <= n)
            {
                if (n % i == 0) return false;
                i++;
            }
            return true;
        }

        public static long NthPrime(long n)
        {
            if (n == 1) return 2;

            long count = 1;
            long num = 1;
            while (count < n)
            {
                n += 2; // optimization
                if (IsPrime(n))
                {
                    count++;
                }
            }
            return num;
        }

        public static List<long> PrimeFactors(long n)
        {
            var pf = new List<long>();

            while (n  %2 == 0)
            {
                pf.Add(2);
                n /= 2;
            }

            for(int i = 3; i < (int)Math.Sqrt(n) + 1; i++)
            {
                while (n % i == 0)
                {
                    pf.Add(i);
                    n /= i;
                }
            }

            if (n > 2)
            {
                pf.Add(n);
            }
            return pf;
        }
    }
}
