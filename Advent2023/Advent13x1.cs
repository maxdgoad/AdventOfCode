using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent13x1
{

    public static int CheckVerticalSymmetry(List<string> set)
    {
        for (int cursor = 0; cursor < set[0].Length - 1; cursor++)
        {
            // find if next 1..x where x <= cursor+1 if symmetric
            var distanceFromCursor = 1;
            var isSymmetric = false;
            while (distanceFromCursor <= cursor+1 && distanceFromCursor + cursor < set[0].Length)
            {
                // get column that is distanceFromCursor to the left
                var leftColumn = new string(set.Select(s => s[cursor - distanceFromCursor + 1]).ToArray());
                // get columns that is distanceFromCursor to the right
                var rightColumn = new string(set.Select(s => s[cursor + distanceFromCursor]).ToArray());

                if (leftColumn == rightColumn)
                {
                    isSymmetric = true;
                }
                else
                {
                    isSymmetric = false;
                    break;
                }
                distanceFromCursor++;
            }
            var leftEdge = cursor + 1 - distanceFromCursor;
            var rightEdge = distanceFromCursor + cursor;
            if (isSymmetric && (distanceFromCursor + cursor == set[0].Length || cursor+1-distanceFromCursor <= 0))
            {
                return cursor + 1;
            }
        }

        return 0;
    }

    public static int CheckHorizontalSymmetry(List<string> set)
    {
        for (int cursor = 0; cursor < set.Count - 1; cursor++)
        {
            // find if next 1..x where x <= cursor+1 if symmetric
            var distanceFromCursor = 1;
            var isSymmetric = false;
            while (distanceFromCursor <= cursor + 1 && distanceFromCursor + cursor < set.Count)
            {
                // get column that is distanceFromCursor to the left
                var topRow = set[cursor - distanceFromCursor + 1];
                // get columns that is distanceFromCurose to the right
                var bottomRow = set[cursor + distanceFromCursor];

                if (topRow == bottomRow)
                {
                    isSymmetric = true;
                }
                else
                {
                    isSymmetric = false;
                    break;
                }
                distanceFromCursor++;
            }
            var topEdge = cursor + 1 - distanceFromCursor;
            var bottomEdge = distanceFromCursor + cursor;
            if (isSymmetric && (distanceFromCursor + cursor == set.Count || cursor + 1 - distanceFromCursor <= 0))
            {
                return cursor + 1;
            }
        }

        return 0;
    }

    public static string Run()
    {
        long ans = 0;
        var stringArr = FileReader.ReadFile("Advent13.txt");

       
        List<List<string>> sets = new List<List<string>>();

        #region "Get list of sets"
        List<string> currentSet = new List<string>();
        foreach ( var s in stringArr )
        {
            if (s.Count != 0)
            {
                currentSet.Add(s[0]);
            }
            else
            {
                sets.Add(currentSet);
                currentSet = new List<string>();
            }
        }
        sets.Add(currentSet);
        #endregion "Get list of sets"

        var count = 0;
        foreach ( var s in sets )
        {
            count++;
            var verticalSymmetry = CheckVerticalSymmetry(s);
            var horizontalSymmetry = CheckHorizontalSymmetry(s);

            Console.WriteLine("#" + count + ": " + verticalSymmetry + " " +  horizontalSymmetry);

            ans += (verticalSymmetry + 100 * horizontalSymmetry);
        }

        

        return ans.ToString();
    }
}
