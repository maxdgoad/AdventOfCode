namespace AdventOfCode.Euler;
internal static class Euler2
{
    public static string Run()
    {
        var a = 1;
        var b = 2;
        var ans = 0;

        while (b < 4000000)
        {
            if (b % 2 == 0)
            {
                ans+=b;
            }
            var temp = a;
            a = b;
            b += temp;

        }
        return ans.ToString();
    }
}
