using AdventOfCode.Utils;

namespace AdventOfCode.Advent2024;
internal class Advent5x2
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
            int badCount = 0;
            var rulesBroken = RulesBroken(update, rules);
            while (rulesBroken.Count > 0)
            {
                for (int i = 0; i < update.Count; i++)
                {
                    foreach (var rule in rulesBroken)
                    {
                        if (rule.Item2 == update[i])
                        {
                            // find where item1 is in the update
                            for (int j = i + 1; j < update.Count; j++)
                            {
                                if (update[j] == rule.Item1)
                                {
                                    // swap the two numbers
                                    var temp = update[i];
                                    update[i] = update[j];
                                    update[j] = temp;
                                }
                            }
                        }
                        if (rule.Item1 == update[i])
                        {
                            // find where item2 is in the update
                            for (int j = i - 1; j > 0; j--)
                            {
                                if (update[j] == rule.Item2)
                                {
                                    // swap the two numbers
                                    var temp = update[i];
                                    update[i] = update[j];
                                    update[j] = temp;
                                }
                            }
                        }
                    }
                }
                rulesBroken = RulesBroken(update, rules);

                badCount++;
            }

            if (badCount > 0)
            {
                // add the middle number to the answer
                ans += update[update.Count / 2];
                Console.WriteLine(badCount);
                //Console.WriteLine(update.Aggregate("", (acc, num) => acc + num + ", ") + " " +
                //    rulesBroken.Aggregate("", (acc, rule) => acc + rule.Item1 + "|" + rule.Item2 + ", "));
            }
        }

        return ans.ToString();
    }

    public static List<(int,int)> RulesBroken(List<int> update, List<(int, int)> rules)
    {

        var rulesBroken = new List<(int, int)>();
        for (var i = 0; i < update.Count; i++)
        {
            // check rules for nums after
            for (int j = i + 1; j < update.Count; j++)
            {
                // find matching bad rule where rhs is current num and lhs is next num
                for (int k = 0; k < rules.Count; k++)
                {
                    if (rules[k].Item2 == update[i] && rules[k].Item1 == update[j])
                    {
                        // add the rule to the list of broken rules, if it's not already there
                        if (!rulesBroken.Contains(rules[k]))
                        {
                            rulesBroken.Add(rules[k]);
                        }
                    }
                }
            }

            // check rules for nums before
            for (int k = i - 1; k > 0; k--)
            {
                // find matching bad rule where lhs is current num and rhs is next num
                for (int j = 0; j < rules.Count; j++)
                {
                    if (rules[j].Item1 == update[i] && rules[j].Item2 == update[k])
                    {
                        // add the rule to the list of broken rules, if it's not already there
                        if (!rulesBroken.Contains(rules[j]))
                        {
                            rulesBroken.Add(rules[j]);
                        }
                    }
                }
            }
        }

        return rulesBroken;
    }
}
