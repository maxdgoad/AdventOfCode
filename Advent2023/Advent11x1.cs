using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent11x1
{
    public static int ManhattanDistance((int, int) galaxy1, (int, int) galaxy2)
    {
        return Math.Abs(galaxy2.Item1 - galaxy1.Item1) + Math.Abs(galaxy2.Item2 - galaxy1.Item2);
    }

    public static string Run()
    {
        long ans = 0;
        var stringArr = FileReader.ReadFile("Advent11.txt");

        List<int> rowsToExpand = new List<int>();
        List<int> colsToExpand = new List<int>();

        for (int row = 0; row < stringArr.Count; row++)
        {
            if (!stringArr[row][0].Contains("#"))
            {
                rowsToExpand.Add(row);
            }
        }

        for (int col = 0; col < stringArr.Count; col++)
        {
            bool isEmpty = true;
            for (int row = 0; row < stringArr.Count; row++)
            {
                if (stringArr[row][0][col] == '#')
                {
                    isEmpty = false;
                    break;
                }
            }
            if (isEmpty)
            {
                colsToExpand.Add(col);
            }
        }

        int rowsAdded = 0;
        foreach (var row in rowsToExpand)
        {
            stringArr.Insert(row + rowsAdded, new List<string> { string.Join("", stringArr[0][0].Select(v => "X")) });
            rowsAdded++;
        }

        int colsAdded = 0;
        foreach (var col in colsToExpand)
        {
            for (int row = 0; row < stringArr.Count; row++)
            {
                stringArr[row][0] = stringArr[row][0].Insert(col + colsAdded, "X");
            }
            colsAdded++;
        }

        List<(int, int)> galaxies = new List<(int, int)>();

        int[] colXs = new int[stringArr[0][0].Length];
        for (int row = 0; row < stringArr.Count; row++)
        {
            int rowX = 0;
            for (int col = 0; col < stringArr[row][0].Length; col++)
            {
                if (stringArr[row][0][col] == '#')
                {
                    galaxies.Add(((row - colXs[col] + (colXs[col] > 0 ? 1 : 1)) + colXs[col] * 1, (col - rowX + (rowX > 0 ? 1 : 1)) + rowX * 1));
                }
                else if (stringArr[row][0][col] == 'X')
                {
                    rowX++;
                    colXs[col]++;
                }
            }
        }

        //for (int i = 0; i < stringArr.Count; i++)
        //{
        //    for (int j = 0; j < stringArr[i][0].Length; j++)
        //    {
        //        Console.Write(stringArr[i][0][j] + " ");
        //    }
        //    Console.WriteLine();
        //}

        for (int x = 0; x < galaxies.Count - 1; x++)
        {
            var galaxy1 = galaxies[x];
            for (int y = x + 1; y < galaxies.Count; y++)
            {
                var galaxy2 = galaxies[y];

                var dist = ManhattanDistance(galaxy1, galaxy2);
                //Console.WriteLine("(" + galaxy1.Item1 + ", " + galaxy1.Item2 + ")" + "<>" + "(" + galaxy2.Item1 + ", " + galaxy2.Item2 + ") " + dist);
                ans += dist;
            }
        }
        return ans.ToString();
    }
}
