using System;
using System.Collections.Generic;
using System.Text;
using Orleans;

namespace Server
{
    public class HashRing<T>
    {
        private readonly T[] _nodes;

        public HashRing(int nodeCount, Func<int, T> nodeExpression)
        {
            _nodes = new T[nodeCount];
            for (var i = 0; i < nodeCount; i++)
            {
                _nodes[i] = nodeExpression(i);
            }
        }

        public IEnumerable<T> Nodes
        {
            get { return _nodes; }
        }

        public T GetNode(string key)
        {
            var hash = JenkinsHash.ComputeHash(Encoding.UTF8.GetBytes(key));
            int index = (int)(hash % _nodes.Length);
            return _nodes[index];
        }
    }
}