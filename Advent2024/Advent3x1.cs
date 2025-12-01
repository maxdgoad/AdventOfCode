using AdventOfCode.Utils;

namespace AdventOfCode.Advent2024;
internal class Advent3x1
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent3.txt");

        var str = stringArr[0][0];

        var ans = 0;

        for (int i = 0; i < 19256;)
        {
            var lhs = 0;
            var rhs = 0;
            if (str.Substring(i, 4) == "mul(")
            {
                // begin sub lookup
                int index = 1;
                while (int.TryParse(str.Substring(i+4, index), out int a))
                {
                    index++;
                    lhs = a;
                }

                // if index is still 1, not valid
                if (index == 1)
                {
                    i++;
                    continue;
                }

                // if the next character is not a comma then break
                if (str.Substring(i + 4 + index - 1, 1) != ",")
                {
                    i++;
                    continue;
                }

                int index2 = 1;
                // then find the rhs
                while (int.TryParse(str.Substring(i + 4 + index, index2), out int b))
                {
                    index2++;
                    rhs = b;
                }

                if (index2 == 1)
                {
                    i++;
                    continue;
                }

                // if next character is not a closing bracket then break
                if (str.Substring(i + 4 + index + index2 - 1, 1) != ")")
                {
                    i++;
                    continue;
                }
                var s = str.Substring(i, 4 + index + index2);
                Console.WriteLine(s + " " +  lhs * rhs + " " +  ans);

            }
            i++;
            ans += lhs * rhs;
           
        }

        return ans.ToString();
    }
}
