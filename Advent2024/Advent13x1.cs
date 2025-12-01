using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024;
internal class Advent13x1
{
    public class ClawMachine
    {
        public (int, int) ButtonA { get; set; }
        public (int, int) ButtonB { get; set; }
        public (BigInteger, BigInteger) Prize { get; set; }
    }

    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent13.txt").Select((str) => str.Count == 0 ? "" : str[0]).ToList();

        BigInteger ans = 0;

        var clawMachines = new List<ClawMachine>();

        for (int i = 0; i < stringArr.Count; i+=4)
        {
            var xAIndex = stringArr[i].IndexOf("X");
            var yAIndex = stringArr[i].IndexOf("Y");
            var commaIndex = stringArr[i].IndexOf(",");

            var xA = int.Parse(stringArr[i].Substring(xAIndex + 2, commaIndex - xAIndex - 2));
            var yA = int.Parse(stringArr[i].Substring(yAIndex + 2));

            var xBIndex = stringArr[i + 1].IndexOf("X");
            var yBIndex = stringArr[i + 1].IndexOf("Y");
            commaIndex = stringArr[i + 1].IndexOf(",");

            var xB = int.Parse(stringArr[i + 1].Substring(xBIndex + 2, commaIndex - xBIndex - 2));
            var yB = int.Parse(stringArr[i + 1].Substring(yBIndex + 2));

            var xPrizeIndex = stringArr[i + 2].IndexOf("X");
            var yPrizeIndex = stringArr[i + 2].IndexOf("Y");
            commaIndex = stringArr[i + 2].IndexOf(",");

            var xPrize = int.Parse(stringArr[i + 2].Substring(xPrizeIndex + 2, commaIndex - xPrizeIndex - 2));
            var yPrize = int.Parse(stringArr[i + 2].Substring(yPrizeIndex + 2));

            clawMachines.Add(new ClawMachine()
            {
                ButtonA = (xA, yA),
                ButtonB = (xB, yB),
                Prize = (10000000000000 + xPrize, 10000000000000 + yPrize)
                //Prize = (xPrize, yPrize)
            });
        }

        int machineIndex = 1;
        foreach (var clawMachine in clawMachines)
        {

            BigInteger denominator = clawMachine.ButtonA.Item1 * clawMachine.ButtonB.Item2 - clawMachine.ButtonB.Item1 * clawMachine.ButtonA.Item2;

            if (machineIndex == 250)
            {

            }

            bool worksHere = false;
            BigInteger min = 0;
            if (denominator != 0)
            {
                BigInteger numerator = clawMachine.ButtonA.Item1 * clawMachine.Prize.Item2 - clawMachine.ButtonA.Item2 * clawMachine.Prize.Item1
                                - 3 * clawMachine.ButtonB.Item1 * clawMachine.Prize.Item2 + 3 * clawMachine.ButtonB.Item2 * clawMachine.Prize.Item1;

                if (numerator % denominator == 0)
                {
                    min = numerator / denominator;
                    worksHere = true;
                }
            }

            bool worksHere2 = false;
            BigInteger min2 = 0;
            BigInteger nANumerator = (clawMachine.ButtonB.Item1 * clawMachine.Prize.Item2 - clawMachine.ButtonB.Item2 * clawMachine.Prize.Item1);
            BigInteger nADenominator = (clawMachine.ButtonB.Item1 * clawMachine.ButtonA.Item2 - clawMachine.ButtonA.Item1 * clawMachine.ButtonB.Item2);

            BigInteger nBNumerator = (clawMachine.ButtonA.Item1 * clawMachine.Prize.Item2 - clawMachine.ButtonA.Item2 * clawMachine.Prize.Item1);
            BigInteger nBDenominator = (clawMachine.ButtonA.Item1 * clawMachine.ButtonB.Item2 - clawMachine.ButtonB.Item1 * clawMachine.ButtonA.Item2);

            if (nANumerator % nADenominator == 0 && nBNumerator % nBDenominator == 0)
            {
                min2 = 3 * (nANumerator / nADenominator) + (nBNumerator / nBDenominator);
                worksHere2 = true;
            }

            if (worksHere != worksHere2)
            {
                Console.WriteLine("Mismatch: " + min + " " + min2 + " " + (min - min2) + " " + machineIndex);
            }
            machineIndex++;

            //Console.WriteLine(min + " " + (nANumerator / nADenominator) + " " + (nBNumerator / nBDenominator));
            ans += min2;
        }

        return ans.ToString();
    }
}

// 75800131617315
// 75200131617108
