using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Utils
{
    public class AdjacencyList
    {
        private int _numNodes;
        public List<int> nodes; 
        private List<List<int>> _adj;   // The actual adjacency list
        private List<List<int>> _rev;   // The adjacency list of the reverse graph
        private bool _directed;

        public AdjacencyList(int num_nodes, bool directed = false)
        {
            _numNodes = num_nodes;
            nodes = new int[num_nodes].ToList();
            _adj = Enumerable.Repeat<List<int>>(new List<int>(), num_nodes).ToList();
            _rev = Enumerable.Repeat<List<int>>(new List<int>(), num_nodes).ToList();
            _directed = directed;
        }

        public void AddEdge(int s, int t, bool try_directed = true)
        {
            try
            {
                if (!_adj[s].Contains(t))
                {
                    _adj[s].Append(t);
                    _rev[t].Append(s);
                }

                if (!_directed && try_directed)
                {
                    AddEdge(t, s, false);
                }
            }
            catch (Exception ex)
            {

            }
            
        }

        public void DeleteEdge(int s, int t, bool try_directed = true)
        {
            try
            {
                var tIndex = _adj[s].IndexOf(t);
                _adj[s].Remove(tIndex);
                var sIndex = _rev[t].IndexOf(s);
                _rev[t].Remove(sIndex);

            }
            catch (Exception e)
            {

            }

            if (!_directed && try_directed)
            {
                DeleteEdge(t, s, false);
            }
        }

        public bool HasEdge(int s, int t)
        {
            return _adj[s].Contains(t);
        }

        public bool HasEdgeReverse(int s, int t)
        {
            return _rev[s].Contains(t);
        }

        public bool IsPath(List<int> path)
        {
            for (int i = 1; i < path.Count; i++)
            {
                if (!HasEdge(path[i-1], path[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsCycle(List<int> path)
        {
            if (!_directed && path.Count <= 2)
            {
                return false;
            }

            path.Add(path[0]);
            return IsPath(path);
        }

        public int InDegree(int s)
        {
            return _rev[s].Count;
        }

        public int OutDegree(int s)
        {
            return _adj[s].Count;
        }

        public int Degree(int s)
        {
            if (!_directed)
            {
                return OutDegree(s);
            }
            return InDegree(s) + OutDegree(s);
        }

        public void Sort()
        {
            foreach (int n in nodes)
            {
                _adj[n].Sort();
                _rev[n].Sort();
            }
        }

        public AdjacencyList Reverse()
        {
            var rev_adjlist = new AdjacencyList(_numNodes, _directed);
            rev_adjlist._adj = _rev.ToArray().ToList(); // deepcopy?
            rev_adjlist._rev = _adj.ToArray().ToList(); // deepcopy?
            return rev_adjlist;
        }

        public List<int> GetItem(int node)
        {
            return _adj[node];
        }

        public int Length()
        {
            return nodes.Count;
        }

        public override string ToString()
        {
            var ret = "";
            foreach (int n in nodes)
            {
                var neighbors = string.Join(" ", _adj[n]);
                ret += n + ": " + neighbors + "\n";
            }
            return ret;
        }
    }
}

