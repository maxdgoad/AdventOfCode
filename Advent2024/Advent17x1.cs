using AdventOfCode.Utils;
using System.Numerics;
using System.Text;

namespace AdventOfCode.Advent2024;
internal class Advent17x1
{
    public static int rA = 0;
    public static int rB = 0;
    public static int rC = 0;

    public static int ComboOperand(int operand)
    {
        if (operand <= 3)
        {
            return operand;
        }
        if (operand == 4)
        {
            return rA;
        }
        if (operand == 5)
        {
            return rB;
        }
        if (operand == 6)
        {
            return rC;
        }
        return 0;
    }
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent17.txt");

        BigInteger ans = 0;

        rA = int.Parse(stringArr[0][0].Substring(12));
        rB = int.Parse(stringArr[1][0].Substring(12));
        rC = int.Parse(stringArr[2][0].Substring(12));

        var instructions = stringArr[4][0].Substring(8).Split(",").Select(str => int.Parse(str)).ToArray();

        var output = new StringBuilder();
        try
        {
            var instrPtr = 0;

            while (instrPtr < instructions.Length-1)
            {
                var instr = instructions[instrPtr];

                var operand = instructions[instrPtr + 1];

                var doesJump = false;
                if (instr == 0)
                {
                    var numerator = rA;
                    var denominator = (int)Math.Pow(2, ComboOperand(operand));
                    rA = numerator / denominator;
                }
                else if (instr == 1)
                {
                    rB = rB ^ operand;
                }
                else if (instr == 2)
                {
                    rB = ComboOperand(operand) % 8;
                }
                else if (instr == 3)
                {
                    if (rA != 0)
                    {
                        instrPtr = operand;
                        doesJump = true;
                    }
                }
                else if (instr == 4)
                {
                    rB = rB ^ rC;
                }
                else if (instr == 5)
                {
                    output.Append((ComboOperand(operand) % 8) + ",");
                    Console.WriteLine("Register A: " + rA);

                }
                else if (instr == 6)
                {
                    var numerator = rA;
                    var denominator = (int)Math.Pow(2, ComboOperand(operand));
                    rB = numerator / denominator;
                }
                else if (instr == 7)
                {
                    var numerator = rA;
                    var denominator = (int)Math.Pow(2, ComboOperand(operand));
                    rC = numerator / denominator;
                }
                if (!doesJump)
                {
                    instrPtr += 2;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }

        return output.ToString();
    }

}
