using AdventOfCode.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml;
using System.Collections.Specialized;

namespace AdventOfCode.Euler
{
    internal class Euler81
    {

        public static void Djikstra()
        {
            //            def Dijkstra(G, w, s):  # {{{
            //    """ Dijkstra's shortest path algorithm. Return the shortest paths tree M and an
            //    array dist such that
            //    dist[u] = the distance from s to u along the unique path in M from s to u
            //            = the shortest distance from s to u in G
            //    Disregarding operations on the priority queue, your algorithm should run in
            //    O(m), where m is the number of edges in G.  """

            //    Q = PriorityDict()
            //    for g in G.nodes:
            //        Q.push(g, float("inf"))

            //    pred = [None for _ in G.nodes]
            //    dist = [float("inf") for _ in G.nodes]

            //    dist[s] = 0

            //    Q.update_key(s, 0)

            //    while len(Q) != 0:
            //        u = Q.pop()
            //        for v in G[u]:
            //            new_dist = dist[u] + w[(u, v)]
            //            if v in Q.keys() and new_dist < Q[v]:
            //                Q.update_key(v, new_dist)
            //                dist[v] = new_dist
            //                pred[v] = u
            //    return pred, dist
        }

        private static int Dequeue(OrderedDictionary dict)
        {
            var lastKey = -1;
            var lastValue = -1;
            foreach(DictionaryEntry entry in dict)
            {
                lastKey = (int)entry.Key;
                lastValue = (int)entry.Value!;
            }
            dict.Remove(lastKey);
            return lastValue;
        }

        private static int GetValue(OrderedDictionary dict, int key)
        {
            foreach (DictionaryEntry entry in dict)
            {
                if ((int)entry.Key == key)
                    return (int)entry.Value!;
            }
            return -1;
        }

        public static (List<int?> pred, List<int> dist) Dijkstra(AdjacencyList G, Dictionary<(int, int), int> w, int s)
        {
            var Q = new OrderedDictionary();
            foreach (var g in G.nodes)
            {
                Q.Add(g, int.MaxValue);
            }

            var pred = Enumerable.Repeat<int?>(null, G.Length()).ToList();
            var dist = Enumerable.Repeat(int.MaxValue, G.Length()).ToList() ?? new List<int>();

            dist[s] = 0;

            while (Q.Count != 0)
            {
                var u = Dequeue(Q);
                foreach (var v in G.GetItem(u))
                {
                    var newDist = dist[u] + w[(u, v)];
                    if (Q.Contains(v) && newDist < GetValue(Q, v))
                    {
                        Q.Remove(v);
                        Q.Add(v, newDist);
                        dist[v] = newDist;
                        pred[v] = u;
                    }
                }
            }
            return (pred, dist);
        }

        public static bool IsConnected(AdjacencyList G)
        {
            var seen = new bool[G.Length()];
            seen[0] = true;
            var working_nodes = new Queue<int>();
            working_nodes.Enqueue(0);
            while (working_nodes.Count != 0)
            {
                var u = working_nodes.Dequeue();
                foreach (var v in G.GetItem(u))
                {
                    if (!seen[v])
                    {
                        seen[v] = true;
                        working_nodes.Enqueue(v);
                    }
                }
            }
            return !seen.Contains(false);
        }

        public static (AdjacencyList, Dictionary<(int,int), int>) ConvertArrayToGraph(List<List<string>> arr)
        {
            var G = new AdjacencyList(arr.Count * arr.Count, true);
            var w = new Dictionary<(int, int), int>();

            for (int x = 0; x < arr.Count * arr.Count; x++)
            {
                if ((x+1) % arr.Count != 0 || x == 0)
                {
                    G.AddEdge(x, x + 1);
                    w[(x, x + 1)] = int.Parse(arr[(x / arr.Count)][(x + 1) % (arr.Count)]);
                }
                if (x < arr.Count*arr.Count-arr.Count)
                {
                    G.AddEdge(x, x + arr.Count);
                    w[(x, x + arr.Count)] = int.Parse(arr[x/arr.Count][x%arr.Count]);
                }
            }
            return (G, w);
        }

        public static string Run()
        {
            var ans = 1;
            var rows = FileReader.ReadFile("Euler81.txt");

            var (G, w) = ConvertArrayToGraph(rows);

            var (T, dist_D) = Dijkstra(G, w, 0);

            return ans.ToString();
        }
    }
}

