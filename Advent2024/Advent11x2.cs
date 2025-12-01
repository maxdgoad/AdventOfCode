using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024;
internal class Advent11x2
{
    public class Node
    {
        public BigInteger value;
        public Node? left;
        public Node? right;
        public bool isLeaf = true;
    }

    public static int MAX_DEPTH = 75;

    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent11.txt", " ")[0].ToList();

        BigInteger ans = 0;

        var nodes = new List<Node>();

        foreach (var c in stringArr)
        {
            var node = new Node();
            node.value = int.Parse(c);
            nodes.Add(node);
        }

        var memo = new Dictionary<(string, int), BigInteger>();
        foreach (var node in nodes)
        {
            ans += BuildTree(node, MAX_DEPTH, memo);
        }

        return ans.ToString();
    }

    public static BigInteger BuildTree(Node node, int depth, Dictionary<(string, int), BigInteger> memo)
    {
        if (memo.TryGetValue((node.value.ToString(), depth), out var me))
        {
            return me;
        }
        if (depth == 0)
        {
            return 1;
        }
        if (node.value == 0)
        {
            var val = node.value.ToString();
            node.value = 1;
            var m = BuildTree(node, depth - 1, memo);
            memo[(val, depth)] = m;
            return m;
        }
        else if (node.value.ToString().Length % 2 == 0)
        {
            var str = node.value.ToString();
            var left = int.Parse(str[..(str.Length / 2)]);

            var right = int.Parse(str[(str.Length / 2)..]);

            var leftNode = new Node();
            leftNode.value = left;
            leftNode.isLeaf = true;

            var rightNode = new Node();
            rightNode.value = right;
            rightNode.isLeaf = true;

            node.left = leftNode;
            node.right = rightNode;
            node.isLeaf = false;

            var m = BuildTree(node.left, depth - 1, memo) + BuildTree(node.right, depth - 1, memo); 
            memo[(str, depth)] = m;
            return m;
        }
        else
        {
            var val = node.value.ToString();
            node.value *= 2024;
            var m = BuildTree(node, depth - 1, memo);
            memo[(val, depth)] = m;
            return m;
        }
    }
}
