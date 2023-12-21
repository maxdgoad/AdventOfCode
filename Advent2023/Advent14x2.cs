using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent14x2
{

    public static void MoveNorth(List<List<string>> stringArr)
    {
        for (int i = 1; i < stringArr.Count; i++)
        {
            for (int j = 0; j < stringArr[i][0].Length; j++)
            {
                int gamer = i;
                while (gamer > 0 && stringArr[gamer - 1][0][j] == '.' && stringArr[gamer][0][j] == 'O')
                {
                    // move the rock down
                    stringArr[gamer - 1][0] = stringArr[gamer - 1][0].Substring(0, j) + "O" + stringArr[gamer - 1][0].Substring(j + 1);
                    // replace rock with dang-ol empty
                    stringArr[gamer][0] = stringArr[gamer][0].Substring(0, j) + "." + stringArr[gamer][0].Substring(j + 1);
                    gamer--;
                }
            }
        }
    }

    public static void MoveSouth(List<List<string>> stringArr)
    {
        for (int i = stringArr.Count - 2; i >= 0; i--)
        {
            for (int j = 0; j < stringArr[i][0].Length; j++)
            {
                int gamer = i;
                while (gamer < stringArr.Count - 1 && stringArr[gamer + 1][0][j] == '.' && stringArr[gamer][0][j] == 'O')
                {
                    // move the rock down
                    stringArr[gamer+1][0] = stringArr[gamer + 1][0].Substring(0, j) + "O" + stringArr[gamer + 1][0].Substring(j + 1);
                    // replace rock with dang-ol empty
                    stringArr[gamer][0] = stringArr[gamer][0].Substring(0, j) + "." + stringArr[gamer][0].Substring(j + 1);
                    gamer++;
                }
            }
        }
    }

    public static void MoveEast(List<List<string>> stringArr)
    {
        for (int i = 0; i < stringArr.Count; i++)
        {
            for (int j = stringArr[i][0].Length-2; j >= 0; j--)
            {
                int gamer = j;
                while (gamer + 1 < stringArr[i][0].Length && stringArr[i][0][gamer + 1] == '.' && stringArr[i][0][gamer] == 'O')
                {
                    var offset = gamer - 1 < 0 ? 0 : gamer;
                    // move the rock right
                    stringArr[i][0] = stringArr[i][0].Substring(0, offset) + ".O" + stringArr[i][0].Substring(offset + 2);
                    gamer++;
                }
            }
        }
    }

    public static void MoveWest(List<List<string>> stringArr)
    {
        for (int i = 0; i < stringArr.Count; i++)
        {
            for (int j = 1; j < stringArr[i][0].Length; j++)
            {
                int gamer = j;
                while (gamer > 0 && stringArr[i][0][gamer - 1] == '.' && stringArr[i][0][gamer] == 'O')
                {
                    var offset = gamer - 1 < 0 ? gamer: gamer - 1;
                    // move the rock right
                    stringArr[i][0] = stringArr[i][0].Substring(0, offset) + "O." + stringArr[i][0].Substring(offset+2);
                    gamer--;
                }
            }
        }
    }

    public static string Run()
    {
        long ans = 0;
        var stringArr = FileReader.ReadFile("Advent14Example.txt");

        var uniqueAns = new Dictionary<int, int>();

        for (int rep = 1; rep <= 10000; rep++)
        {
            var tempAns = 0;
            if (rep % 100000 == 0)
            {
                Console.WriteLine(rep);
            }
            MoveNorth(stringArr);
            MoveWest(stringArr);
            MoveSouth(stringArr);
            MoveEast(stringArr);

            // Calculate northern weight
            for (int i = stringArr.Count; i > 0; i--)
            {
                tempAns += i * stringArr[stringArr.Count - i][0].Where(s => s == 'O').Count();
            }
            if (!uniqueAns.ContainsKey(tempAns))
            {
                uniqueAns.Add(tempAns, 1);
            }
            else
            {
                uniqueAns[tempAns]++;
            }
        }

        foreach (int i in uniqueAns.Keys)
        {
            Console.WriteLine(i + " " + uniqueAns[i]);
        }

        foreach (var line in stringArr)
        {
            Console.WriteLine(line[0]);
        }
        

        

        return ans.ToString();
    }
}
