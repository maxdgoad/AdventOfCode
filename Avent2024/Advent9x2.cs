using AdventOfCode.Utils;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode.Advent2024;
internal class Advent9x2
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent9.txt").ToList();

        BigInteger ans = 0;

        var str = stringArr[0][0];

        int id = 0;
        var disk = new Dictionary<BigInteger, (string, int)>();
        var index = 0;
        var dotCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            if (i % 2 == 0)
            {
                var length = int.Parse(str[i].ToString());
                for (int j = 0; j < length; j++)
                {
                    disk.Add(index, (id.ToString(), length));
                    index++;
                }
                id++;
            }
            else
            {
                var gap = int.Parse(str[i].ToString());
                for (int j = 0; j < gap; j++)
                {
                    disk.Add(index, (".", gap));
                    index++;
                    dotCount++;
                }
            }
        }
        id--;
        var ids = disk.Values.Where(t => t.Item1 != ".").Select(t => KeyValuePair.Create(BigInteger.Parse(t.Item1), t.Item2)).Distinct().ToDictionary();
        BigInteger endIndex = disk.Keys.Count - 1;
        for (BigInteger i = id; i > 0; i--)
        {
            var isMoved = false;

            for (int j = 0; j < disk.Keys.Count; j++)
            {
                if (!isMoved && disk[j].Item1 == "." && ids[i] <= disk[j].Item2)
                {
                    var space = disk[j].Item2;
                    for (int k = 0; k < space; k++)
                    {
                        if (k < ids[i])
                        {
                            disk[j+k] = (i.ToString(), ids[i]);
                        }
                        else
                        {  
                            disk[j+k] = (".", space - ids[i]);                   
                        }
                    }
                    isMoved = true;
                    j += ids[i]-1;
                }
                else if (disk[j].Item1 == i.ToString())
                {
                    if (isMoved)
                    {
                        var numDots = ids[i];
                        // check for dots before and after
                        var before = 1;
                        while (j-before > 0 && disk[j-before].Item1 == ".")
                        {
                            before++;
                        }
                        before--;

                        var after = 0;
                        while (j + ids[i] + after < disk.Count && disk[j + ids[i] + after].Item1 == ".")
                        {
                            after++;
                        }

                        for (int k = j-before; k < j + after + ids[i]; k++)
                        {
                            disk[k] = (".", ids[i] + before + after);
                        }
                    }               
                    break;
                }
            }
        }

        foreach (var key in disk.Keys)
        {
            if (disk[key].Item1 == ".")
            {
                continue;
            }
            else
            {
                ans += key * BigInteger.Parse(disk[key].Item1);
            }
        }

        return ans.ToString();
    }
}
