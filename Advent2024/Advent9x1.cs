using AdventOfCode.Utils;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode.Advent2024;
internal class Advent9x1
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent9.txt").ToList();

        BigInteger ans = 0;

        var str = stringArr[0][0];

        int id = 0;
        var disk = new Dictionary<BigInteger, string>();
        var index = 0;
        for (int i = 0; i < str.Length; i++)
        {
            if (i % 2 == 0)
            {
                var length = int.Parse(str[i].ToString());
                for (int j = 0; j < length; j++)
                {
                    disk.Add(index, id.ToString());
                    index++;
                }
                id++;
            }
            else
            {
                var gap = int.Parse(str[i].ToString());
                for (int j = 0; j < gap; j++)
                {
                    disk.Add(index, ".");
                    index++;
                }
            }
        }

        BigInteger endIndex = disk.Keys.Count - 1;
        var compactDisk = new List<(BigInteger,  BigInteger)>();
        for (int i = 0; i <= endIndex; i++)
        {
            if (disk[i] == ".")
            {
                while (disk[endIndex] == ".")
                {
                    endIndex--;
                }
                compactDisk.Add((i, BigInteger.Parse(disk[endIndex])));
                endIndex--;
            }
            else
            {
                compactDisk.Add((i, BigInteger.Parse(disk[i])));
            }
        }

        for (int i = 0; i < compactDisk.Count; i ++)
        {
            ans += compactDisk[i].Item1 * compactDisk[i].Item2;
        }

        return ans.ToString();
    }
}
