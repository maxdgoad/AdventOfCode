using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent10x2
{
    private static List<List<char>> charArr;
    private static bool?[,] mainLoop;
    private static char?[,] sixArr;
    private static bool[,] escapeArr;
    private static bool[,] trappedArr;

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
            else if (col + colAdd < mainLoop.GetLength(1) && !mainLoop[row, col + colAdd].HasValue)
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
        long ans = 0;
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

        sixArr = new char?[charArr.Count * 3, charArr[0].Count * 3];

        escapeArr = new bool[charArr.Count * 3, charArr[0].Count * 3];
        trappedArr = new bool[charArr.Count * 3, charArr[0].Count * 3];

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
                if (mainLoopVal ?? false)
                {
                    Console.BackgroundColor = sixArr[i, j] == 'x' ? ConsoleColor.Black : ConsoleColor.Green;
                    Console.Write(sixArr[i,j]);
                    Console.BackgroundColor = ConsoleColor.Black;

                }
                else
                {
                    //Console.BackgroundColor = sixArr[i, j] != 'x' ? ConsoleColor.Blue : ConsoleColor.Black;

                    if ((i - 1) % 3 == 0 && (j - 1) % 3 == 0)
                    {
                        // Method to find out if it can reach the edge through X's
                        var seen = new bool[sixArr.GetLength(0), sixArr.GetLength(1)];
                        // 224, 210
                        var canEscape = CanEscape(i, j, seen, 0);

                        if (canEscape)
                        {
                            // update escapeArr with seen
                            for (int x = 0; x < seen.GetLength(0); x++)
                            {
                                for (int y = 0; y < seen.GetLength(1); y++)
                                {
                                    escapeArr[x, y] = seen[x, y];
                                }
                            }
                            Console.BackgroundColor = ConsoleColor.Blue;
                        }
                        else
                        {
                            // update trappedArr with seen
                            for (int x = 0; x < seen.GetLength(0); x++)
                            {
                                for (int y = 0; y < seen.GetLength(1); y++)
                                {
                                    trappedArr[x, y] = seen[x, y];
                                }
                            }
                            Console.BackgroundColor = ConsoleColor.Red;
                            ans += 1;
                        }
                    }

                    Console.Write(sixArr[i, j]);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }
        return ans.ToString();
    }
    

    private static bool CanEscape(int i, int j, bool[,] seen, int stack)
    {
        if (stack > 3000)
        {
            return false;
        }

        seen[i, j] = true;

        if (escapeArr[i, j])
        {
            return true;
        }
        if (trappedArr[i, j])
        {
            return false;
        }

        bool canEscape = false;

        for (int x = -1; x <= 1; x++)
        {
            if (i + x < 0 || i + x >= sixArr.GetLength(0))
            {
                return true;
            }
            for (int y = -1; y <= 1; y++)
            {       
                if (j + y < 0 || j + y >= sixArr.GetLength(1))
                {
                    return true;
                }
                else if (sixArr[i + x, j + y] == 'x' && !seen[i + x, j + y])
                {
                    if (escapeArr[i + x, j + y])
                    {
                        return true;
                    }
                    else if (trappedArr[i + x, j + y])
                    {
                        return false;
                    }
                    if (canEscape)
                    {
                        return true;
                    }
                    canEscape = CanEscape(i + x, j + y, seen, stack + 1);
                }          
            }
        }
        return canEscape;
    }
}
