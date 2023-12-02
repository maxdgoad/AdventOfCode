using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Euler
{
    internal static class Euler1
    {
        public static string Run()
        {
            var ans = 0;

            for (int i = 1; i < 1000; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    ans += i;
                }
            }
            return ans.ToString();
        }
    }
}
