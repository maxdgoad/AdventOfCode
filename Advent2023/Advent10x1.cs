using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent10x1
{

    private static List<List<char>> charArr;
    private static int?[,] distanceArr;

    public static void Simulate(int row, int col)
    {
        char pipeType = 'M';
        while (true)
        {
            pipeType = charArr[row][col];
            int rowAdd = 0;
            int rowSub = 0;
            int colAdd = 0;
            int colSub = 0;
            switch (pipeType)
            {
                case '|':
                    rowAdd = 1; rowSub = 1; break;
                case '-':
                    colAdd = 1; colSub = 1; break;
                case '7':
                    rowAdd = 1; colSub = 1; break;
                case 'F':
                    rowAdd = 1; colAdd = 1; break;
                case 'J':
                    rowSub = 1; colSub = 1; break;
                case 'L':
                    rowSub = 1; colAdd = 1; break;
                case 'S':
                    rowAdd = 1; colAdd = 1; break;
            }

            if (row + rowAdd < distanceArr.GetLength(0) && !distanceArr[row + rowAdd, col].HasValue)
            {
                distanceArr[row + rowAdd, col] = distanceArr[row, col] + 1;
                row++;
            }
            else if (row - rowSub >= 0 && !distanceArr[row - rowSub, col].HasValue)
            {
                distanceArr[row - rowSub, col] = distanceArr[row, col] + 1;
                row--;
            }
            else if (col + colAdd < distanceArr.GetLength(0) && !distanceArr[row, col + colAdd].HasValue)
            {
                distanceArr[row, col + colAdd] = distanceArr[row, col] + 1;
                col++;
            }
            else if (col - colSub >= 0 && !distanceArr[row, col - colSub].HasValue)
            {
                distanceArr[row, col - colSub] = distanceArr[row, col] + 1;
                col--;
            }
            else
            { break; }
        }

        Console.WriteLine((distanceArr[row, col]+1)/2);
    }

    public static string Run()
    {
        charArr = FileReader.ReadFileCharArray("Advent10Example2.txt", " ");
        
        // find s
        (int, int) sLoc = (0, 0);
        distanceArr = new int?[charArr.Count, charArr.Count];
        for (int i = 0; i < charArr.Count; i++)
        {
            for (int j = 0; j < charArr[i].Count; j++)
            {
                if (charArr[i][j] == 'S')
                {
                    sLoc = (i, j);
                    distanceArr[i, j] = 0;
                }
                else
                {
                    distanceArr[i, j] = null;
                }
            }
        }

        // traverse
        Simulate(sLoc.Item1, sLoc.Item2);

        for (int i = 0; i < distanceArr.GetLength(0); i++)
        {
            for (int j = 0; j < distanceArr.GetLength(1); j++)
            {
                if (!distanceArr[i, j].HasValue)
                {
                    Console.Write("." + " ");
                }
                else
                {
                    Console.Write(distanceArr[i, j] + " ");
                }               
            }
            Console.WriteLine();
        }

        long ans = 0;
        return ans.ToString();
    }
}
