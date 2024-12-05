using AdventOfCode.Utils;

namespace AdventOfCode.Advent2024;
internal class Advent5x1
{
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent5.txt");

        var rules = new List<(int, int)>();

        var updates = new List<List<int>>();

        var ans = 0;

        var isRules = true;
        foreach (var line in stringArr)
        {
            if (line.Count == 0)
            {
                isRules = false;
            }
            else
            {
                if (isRules)
                {
                    var lhs = line[0].Substring(0, line[0].IndexOf("|"));
                    var rhs = line[0].Substring(line[0].IndexOf("|") + 1);

                    rules.Add((int.Parse(lhs), int.Parse(rhs)));
                }
                else
                {
                    updates.Add(line[0].Split(",").Select((str) => int.Parse(str.Trim())).ToList());
                }
            }
        }

        foreach (var update in updates)
        {
            var isValid = true;
            for (var i = 0; i < update.Count; i++)
            {
                // check rules for nums after
                for (int j = i+1; j < update.Count; j++)
                {
                    // find matching bad rule where rhs is current num and lhs is next num
                    for (int k = 0; k < rules.Count; k++)
                    {
                        if (rules[k].Item2 == update[i] && rules[k].Item1 == update[j])
                        {
                            isValid = false;
                        }
                    }
                }

                // check rules for nums before
                for (int k = i-1; k > 0; k--)
                {
                    // find matching bad rule where lhs is current num and rhs is next num
                    for (int j = 0; j < rules.Count; j++)
                    {
                        if (rules[j].Item1 == update[i] && rules[j].Item2 == update[k])
                        {
                            isValid = false;
                        }
                    }
                }
            }

            if (isValid)
            {
                // add the middle number to the answer
                ans += update[update.Count / 2];
            }
        }

        return ans.ToString();
    }
}
