using AdventOfCode.Utils;
using System.Numerics;

namespace AdventOfCode.Advent2024;
internal class Advent11x1
{
    public class Node
    {
        public BigInteger value;
        public Node? left;
        public Node? right;
        public bool isLeaf = true;

    }
    public static string Run()
    {
        var stringArr = FileReader.ReadFile("Advent11.txt", " ")[0].ToList();

        var ans = 0;

        var nodes = new List<Node>();

        foreach (var c in stringArr)
        {
            var node = new Node();
            node.value = int.Parse(c);
            
            nodes.Add(node);
        }

        foreach (var node in nodes)
        {
            BuildTree(node, 25);
        }

        // count the leaves
        foreach (var node in nodes)
        {
            ans += CountLeaves(node);  
            //PrintLeaves(node);
        }

        return ans.ToString();
    }

    public static void BuildTree(Node? node, int depth)
    {
        if (depth == 0)
        {
            return;
        }
        if (node == null)
        { 
            return;
        }
        if (node.value == 0)
        {
            node.value = 1;
            BuildTree(node, depth - 1);
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

            BuildTree(node.left, depth - 1);
            BuildTree(node.right, depth - 1);
        }
        else
        {
            node.value *= 2024;
            BuildTree(node, depth - 1);
        }
    }

    public static int CountLeaves(Node? node)
    {
        if (node == null)
        {
            return 0;
        }
        if (node.isLeaf)
        {
            return 1;
        }
        return CountLeaves(node.left) + CountLeaves(node.right);
    }

    public static void PrintLeaves(Node? node)
    {
        if (node == null)
        {
            return;
        }
        if (node.isLeaf)
        {
            Console.Write(node.value + " ");
        }
        PrintLeaves(node.left);
        PrintLeaves(node.right);
    }
}
