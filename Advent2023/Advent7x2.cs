using AdventOfCode.Utils;

namespace AdventOfCode.Advent2023;
internal class Advent7x2
{
    private static List<(string, string)> fiveOfKind = new List<(string, string)>();
    private static List<(string, string)> fourOfKind = new List<(string, string)>();
    private static List<(string, string)> fullHouse = new List<(string, string)>();
    private static List<(string, string)> threeOfKind = new List<(string, string)>();
    private static List<(string, string)> twoPair = new List<(string, string)>();
    private static List<(string, string)> onePair = new List<(string, string)>();
    private static List<(string, string)> highCard = new List<(string, string)>();

    private static Dictionary<char, int> cardRank = new Dictionary<char, int>
    {
        { 'J', 1 },
        { '2', 2 },
        { '3', 3 },
        { '4', 4 },
        { '5', 5 },
        { '6', 6 },
        { '7', 7 },
        { '8', 8 },
        { '9', 9 },
        { 'T', 10 },
        { 'Q', 12 },
        { 'K', 13 },
        { 'A', 14 },
    };

    public static void insertHand(List<string> hand, List<(string, string)> typeList)
    {
        int insertIndex = 0;
        for (int rep = 0; rep < typeList.Count; rep++)
        {
            var existingHand = typeList[rep].Item1;
            for (int j = 0; j < existingHand.Length; j++)
            {
                if (cardRank[hand[0].ElementAt(j)] > cardRank[existingHand.ElementAt(j)])
                {
                    insertIndex += 1;
                    break;
                }
                else if (hand[0].ElementAt(j) == existingHand.ElementAt(j))
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        typeList.Insert(insertIndex, (hand[0], hand[1]));
    }

    private static Dictionary<char, int> getCardCount(string hand)
    {
        var response = new Dictionary<char, int>();

        int jokerCount = 0;

        for (int rep = 0; rep < hand.Length; rep++)
        {
            if (hand.ElementAt(rep) != 'J')
            {
                if (!response.ContainsKey(hand.ElementAt(rep)))
                {
                    response.Add(hand.ElementAt(rep), 1);
                }
                else
                {
                    response[hand.ElementAt(rep)]++;
                }
            }
            else
            {
                jokerCount++;
            }
        }
        var highestKey = response.Keys.First();
        foreach (var key in response.Keys)
        {
            if (response[key] > response[highestKey])
            {
                highestKey = key;
            }
        }
        response[highestKey] += jokerCount;
        return response;
    }
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent7.txt", " ");

        foreach (var hand in stringArr)
        {
            var charCount = hand[0] != "JJJJJ" ? getCardCount(hand[0]) : new Dictionary<char, int>() { { 'j', 5 } };

            if (charCount.Any(cnt => cnt.Value == 5))
            {
                insertHand(hand, fiveOfKind);
            }
            else if (charCount.Any(cnt => cnt.Value >= 4))
            {
                insertHand(hand, fourOfKind);
            }
            else if (charCount.Any(cnt => cnt.Value == 3) && charCount.Any(cnt => cnt.Value == 2))
            {
                insertHand(hand, fullHouse);
            }
            else if (charCount.Any(cnt => cnt.Value >= 3))
            {
                insertHand(hand, threeOfKind);
            }
            else if (charCount.Where(cnt => cnt.Value >= 2).ToList().Count == 2)
            {
                insertHand(hand, twoPair);
            }
            else if (charCount.Any(cnt => cnt.Value >= 2))
            {
                insertHand(hand, onePair);
            }
            else
            {
                insertHand(hand, highCard);
            }
        }

        var combinedList = highCard
        .Concat(onePair)
        .Concat(twoPair)
        .Concat(threeOfKind)
        .Concat(fullHouse)
        .Concat(fourOfKind)
        .Concat(fiveOfKind).ToArray();

        long ans = 0;

        for (int rep = 0; rep < combinedList.Length; rep++)
        {
            ans += (rep + 1) * int.Parse(combinedList[rep].Item2);
        }

        return ans.ToString();
    }
}
