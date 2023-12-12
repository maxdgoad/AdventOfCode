using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent12x1
{
    public static int Combinations(string row, List<int> sequences, int index = 0)
    {
        if (index >= sequences.Count) { return !row.Any(s => s != '.') ? 1 : 0; }
        var count = 0;
		for (int i = 0; i < row.Length; i++)
		{
			if (row[i] == '.')
			{
				break;
			}
			count++;		
		}

        if (count == 0 && row.Length > 1)
        {
            return Combinations(row.Substring(1), sequences, index);
        }
		else if (count == sequences[index] && index == sequences.Count-1)
		{ 
            return 1; 
        }
		else if (count >= sequences[index])
        {
			var combs = 0;
			for (int head = 0; head <= count - sequences[index]; head++)
            {
				if (row[head] == '?')
				{
                    if (row.Length > (head + sequences[index] + 1) && row[sequences[index] + 1 + head] == '#')
                    {
                        combs += Combinations(row.Substring(1), sequences, index);
                    }
					else if (row.Length > head + sequences[index] + 1)
                    {
                        combs += Combinations(row.Substring(head + sequences[index] + 1), sequences, index + 1);
					}
				}
				else
				{
                    if (row.Length > (sequences[index] + head + 1) && 
                        (row[sequences[index] + head + 1] == '?' || row[sequences[index] + head + 1] == '.'))
                    {
						combs += Combinations(row.Substring(head + sequences[index] + 1), sequences, index + 1);
					}
                    break;
				}
			}
            
            return combs;
        }
        return 0;
    }
    public static string Run()
    {
        long ans = 0;
        var stringArr = FileReader.ReadFile("Advent12Example.txt", " ");

        List<string> rows = stringArr.Select(a => a[0]).ToList();

        List<List<int>> sequences = stringArr.Select(a => a[1].Split(",").Select( s => int.Parse(s)).ToList()).ToList();

        for (int rep = 0; rep < rows.Count;  rep++)
        {
            var comb =  Combinations(rows[rep], sequences[rep]);
            Console.WriteLine(rows[rep] +  " " + comb);
        }

        return ans.ToString();
    }
}
