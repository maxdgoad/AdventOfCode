using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Euler
{
    internal static class Euler4
    {
        public static string Run()
        {
            var ans = 0;

            for (int i = 999; i >= 100; i--)
            {
                for (int j = i-1; j > 100; j--)
                {
                    var product = i * j;
                    var charArray = product.ToString().ToCharArray();
                    var isPalindrome = true;
                    for (int loc = 0; loc < charArray.Length/2; loc++)
                    {
                        if (charArray[loc] != charArray[charArray.Length-loc-1])
                        {
                            isPalindrome = false;
                            break;
                        }
                    }
                    if (isPalindrome)
                    {
                        Console.WriteLine(product);
                        if (product > ans)
                        {
                            ans = product;
                        }
                    }             
                }
            }

            return ans.ToString();
        }
    }
}
