using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent10x2
{
    private static List<List<char>> charArr;
    private static bool?[,] mainLoop;

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

            if (row + rowAdd < mainLoop.GetLength(0) && !mainLoop[row + rowAdd, col].HasValue)
            {
                mainLoop[row + rowAdd, col] = true;
                row++;
            }
            else if (row - rowSub >= 0 && !mainLoop[row - rowSub, col].HasValue)
            {
                mainLoop[row - rowSub, col] = true;
                row--;
            }
            else if (col + colAdd < mainLoop.GetLength(0) && !mainLoop[row, col + colAdd].HasValue)
            {
                mainLoop[row, col + colAdd] = true;
                col++;
            }
            else if (col - colSub >= 0 && !mainLoop[row, col - colSub].HasValue)
            {
                mainLoop[row, col - colSub] = true;
                col--;
            }
            else
            { break; }
        }
    }

    public static string Run()
    {
        charArr = FileReader.ReadFileCharArray("Advent10.txt", " ");

        // find s
        (int, int) sLoc = (0, 0);
        mainLoop = new bool?[charArr.Count, charArr[0].Count];
        for (int i = 0; i < charArr.Count; i++)
        {
            for (int j = 0; j < charArr[i].Count; j++)
            {
                if (charArr[i][j] == 'S')
                {
                    sLoc = (i, j);
                    mainLoop[i, j] = true;
                }
                else if (charArr[i][j] == '.')
                {
                    mainLoop[i, j] = false;
                }
                else
                {
                    mainLoop[i, j] = null;
                }
            }
        }

        // traverse
        Simulate(sLoc.Item1, sLoc.Item2);

        char?[,] sixArr = new char?[charArr.Count * 3, charArr[0].Count * 3];

        for (int i = 0; i < mainLoop.GetLength(0); i++)
        {
            for (int j = 0; j < mainLoop.GetLength(1); j++)
            {
                char pipeType = charArr[i][j];

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

                sixArr[3 * i, 3 * j] = 'x';
                sixArr[3 * i, 3 * j + 1] = rowSub == 1 ? '|' : 'x';             
                sixArr[3 * i, 3 * j + 2] = 'x';

                sixArr[3 * i + 1, 3 * j] = colSub == 1 ? '-' : 'x';     
                sixArr[3 * i + 1, 3 * j + 1] = pipeType;    
                sixArr[3 * i + 1, 3 * j + 2] = colAdd == 1 ? '-' : 'x';

                sixArr[3 * i + 2, 3 * j] = 'x';     
                sixArr[3 * i + 2, 3 * j + 1] = rowAdd == 1 ? '|' : 'x';         
                sixArr[3 * i + 2, 3 * j + 2] = 'x';

            }
        }

        for (int i = 0; i < sixArr.GetLength(0); i++)
        {
            for (int j = 0; j < sixArr.GetLength(1); j++)
            {
                bool? mainLoopVal = mainLoop[i/3, j/3];
                if (mainLoopVal.HasValue)
                {
                    if (mainLoopVal ?? false)
                    {
                        Console.BackgroundColor = sixArr[i, j] == 'x' ? ConsoleColor.Black : ConsoleColor.Green;
                        Console.Write(sixArr[i,j]);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = sixArr[i, j] != 'x' ? ConsoleColor.Blue : ConsoleColor.Black;
                        Console.Write(sixArr[i,j]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        // Method to find out if it can reach the edge through X's
                    }
                }
                else
                {
                    Console.BackgroundColor = sixArr[i, j] != 'x' ? ConsoleColor.Blue : ConsoleColor.Black;
                    Console.Write(sixArr[i,j]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    // Method to find out if it can reach the edge through X's
                }


            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }

        long ans = 0;
        return ans.ToString();
    }
}
