using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023
{
    internal class Advent3x2
    {
        // Crawl till find symbol
        // Then check numbers in 8 directions

        // check left and right
        // Check up and down. If up and down are not numbers then you know that you can check the diagonals
        public static string Run()
        {
            var stringArr = FileReader.ReadFile("Advent3.txt");

            long ans = 0;

            for (int x = 0; x < stringArr.Count; x++)
            {
                var line = stringArr[x];
                string lineStr = (line.FirstOrDefault() ?? "");

                for (int i = 0; i < lineStr.Length; i++)
                {
                    if (lineStr.Substring(i, 1) == "*")
                    {
                        List<int> gears = new List<int>();
                        if (x > 0)
                        {
                            // check above
                            var lineAbove = stringArr[x - 1];
                            string lineStrAbove = (lineAbove.FirstOrDefault() ?? "");
                            if (int.TryParse(lineStrAbove.Substring(i, 1), out int blah2))
                            {
                                int startIndex = 0;
                                int endIndex = lineStr.Length - 1;
                                if (i > 0)
                                {
                                    // find start index
                                    startIndex = lineStrAbove.IndexOf('.', i);
                                }
                                if (i < lineStr.Length - 1)
                                {
                                    // find end index
                                    endIndex = lineStrAbove.LastIndexOf(".", i);
                                }

                                if (int.TryParse(lineStrAbove.Substring(endIndex + 1, startIndex - endIndex - 1), out int aboveAns))
                                {
                                    gears.Add(aboveAns);
                                }
                            }
                            else
                            {
                                // if not number above, check left and right diagonals, if index is appropriate

                                // top left
                                if (i > 0)
                                {
                                    if (int.TryParse(lineStrAbove.Substring(i - 1, 1), out int blah3))
                                    {
                                        // we know i is a period
                                        var startIndex = lineStrAbove.LastIndexOf(".", i - 1);
                                        startIndex += 1;

                                        if (int.TryParse(lineStrAbove.Substring(startIndex, i - startIndex), out int topLeftDiag))
                                        {
                                            gears.Add(topLeftDiag);
                                        }
                                    }
                                }
                                // top right
                                if (i < lineStr.Length - 1)
                                {
                                    if (int.TryParse(lineStrAbove.Substring(i + 1, 1), out int blah3))
                                    {
                                        // we know i is a period
                                        var endIndex = lineStrAbove.IndexOf(".", i + 1);
                                        if (endIndex < 0) endIndex = lineStrAbove.Length;

                                        if (int.TryParse(lineStrAbove.Substring(i + 1, endIndex - i - 1), out int topRightDiag))
                                        {
                                            gears.Add(topRightDiag);
                                        }
                                    }
                                }
                            }

                        }
                        if (x < stringArr.Count - 1)
                        {
                            // check below
                            var lineAbove = stringArr[x + 1];
                            string lineStrBelow = (lineAbove.FirstOrDefault() ?? "");
                            if (int.TryParse(lineStrBelow.Substring(i, 1), out int blah2))
                            {
                                int startIndex = 0;
                                int endIndex = lineStr.Length - 1;
                                if (i > 0)
                                {
                                    // find start index
                                    startIndex = lineStrBelow.IndexOf('.', i);
                                }
                                if (i < lineStr.Length - 1)
                                {
                                    // find end index
                                    endIndex = lineStrBelow.LastIndexOf(".", i);
                                }

                                if (int.TryParse(lineStrBelow.Substring(endIndex + 1, startIndex - endIndex - 1), out int aboveAns))
                                {
                                    gears.Add(aboveAns);
                                }
                            }
                            else
                            {
                                // if not number below, check left and right diagonals, if index is appropriate

                                // bottom left
                                if (i > 0)
                                {
                                    if (int.TryParse(lineStrBelow.Substring(i - 1, 1), out int blah3))
                                    {
                                        // we know i is a period
                                        var startIndex = lineStrBelow.LastIndexOf(".", i - 1);
                                        startIndex += 1;

                                        if (int.TryParse(lineStrBelow.Substring(startIndex, i - startIndex), out int bottomLeftDiag))
                                        {
                                            gears.Add(bottomLeftDiag);
                                        }
                                    }
                                }
                                // bottom right
                                if (i < lineStr.Length - 1)
                                {
                                    if (int.TryParse(lineStrBelow.Substring(i + 1, 1), out int blah3))
                                    {
                                        // we know i is a period
                                        var endIndex = lineStrBelow.IndexOf(".", i + 1);
                                        if (endIndex < 0) endIndex = lineStrBelow.Length;

                                        if (int.TryParse(lineStrBelow.Substring(i + 1, endIndex - i - 1), out int bottomRightDiag))
                                        {
                                            gears.Add(bottomRightDiag);
                                        }
                                    }
                                }
                            }
                        }
                        if (i > 0)
                        {
                            // check left
                            var lastPeriodIndex = lineStr.LastIndexOf('.', i - 1);
                            if (lastPeriodIndex < i - 1)
                            {
                                if (int.TryParse(lineStr.Substring(lastPeriodIndex + 1, i - lastPeriodIndex - 1), out int leftAns))
                                {
                                    gears.Add(leftAns);
                                }
                            }
                        }
                        if (i < lineStr.Length - 1)
                        {
                            // check right
                            var nextPeriodIndex = lineStr.IndexOf('.', i + 1);
                            if (nextPeriodIndex < 0) nextPeriodIndex = lineStr.Length;
                            if (nextPeriodIndex > i + 1)
                            {
                                if (int.TryParse(lineStr.Substring(i + 1, nextPeriodIndex - i - 1), out int rightAns))
                                {
                                    gears.Add(rightAns);
                                }
                            }
                        }

                        if (gears.Count == 2)
                        {
                            ans += gears[0] * gears[1];
                        }
                    }
                }
            }
            return ans.ToString();
        }
    }
}
