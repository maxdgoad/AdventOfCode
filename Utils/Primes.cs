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

//def primeFactors(n):
//    pf = []

//    while n % 2 == 0:
//        pf.append(2)
//        n = n / 2


//    for i in range(3, int(math.sqrt(n)) + 1, 2):
//        while n % i == 0:
//            pf.append(int(i))
//            n = n / i
//    if n > 2:
//        pf.append(int(n))
//    return pf

//def primes_up_to(n):
//    # http://code.activestate.com/recipes/576640/
//    nroot = int(math.sqrt(n))
//    sieve = [True] * (n + 1)# range(n + 1)
//    sieve[0] = False
//    sieve[1] = False

//    for i in range(2, nroot + 1):
//        if sieve[i]:
//            m = n / i - i
//            sieve[i * i: n + 1: i] = [False] * (int(m) + 1)

//    return [i for i in range(n + 1) if sieve[i]]

//def primes_up_to_with_start(start, n):
//    # http://code.activestate.com/recipes/576640/
//    nroot = int(math.sqrt(n))
//    sieve = [True] * (n + 1)# range(n + 1)
//    sieve[0] = False
//    sieve[1] = False

//    for i in range(2, nroot + 1):
//        if sieve[i]:
//            m = n / i - i
//            sieve[i * i: n + 1: i] = [False] * (int(m) + 1)

//    return [i for i in range(n + 1) if sieve[i] and len(set(str(i)))+5 < len(str(i))]
