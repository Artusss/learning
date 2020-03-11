using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Graph
{
    class GraphException : Exception 
    {
        public GraphException() : base()
        { }
        public GraphException(string message) : base(message)
        { }
        public GraphException(string message, Exception inner) : base(message, inner) 
        { }
    }
    class WeightedGraph<T> : ICounter
    {
        private Dictionary<T, Dictionary<int, T>> GraphStruct;

        public WeightedGraph()
        {
            this.GraphStruct = new Dictionary<T, Dictionary<int, T>>();
        }

        public void InsertElement(T Element)
        {
            if (this.GraphStruct.ContainsKey(Element))
            {
                throw new GraphException("This graph structure also have element with this value");
            }
            this.GraphStruct.Add(Element, new Dictionary<int, T>());
        }

        public void InsertNodes(T Element, Dictionary<int, T>  RelationDict)
        {
            if (RelationDict.Count == 0)
            {
                throw new GraphException("This element don't have relations");
            }
            if (!this.GraphStruct.ContainsKey(Element))
            {
                throw new GraphException($"Element `{Element}` not found");
            }
            foreach (var RelationElement in RelationDict)
            {
                if (this.GraphStruct[Element].Values.SingleOrDefault(i => i.Equals(RelationElement.Value)) != null)
                {
                    throw new GraphException($"Node {Element}->{RelationElement} also exists");
                }
                this.GraphStruct[Element].Add(RelationElement.Key, RelationElement.Value);
            }
        }

        public void FlushElement(T Element)
        {
            if (!this.GraphStruct.ContainsKey(Element))
            {
                throw new GraphException($"Element `{Element}` not found");
            }
            foreach (var RelationElement in this.GraphStruct[Element])
            {
                this.GraphStruct[RelationElement.Value]
                    .Remove(
                        this.GraphStruct[RelationElement.Value]
                            .SingleOrDefault(
                                i => i.Value.Equals(Element)
                            )
                        .Key
                    );
            }
            this.GraphStruct.Remove(Element);
        }

        public Dictionary<int, T> SelectNodes(T Element)
        {
            if (!this.GraphStruct.ContainsKey(Element))
            {
                throw new GraphException($"Element `{Element}` not found");
            }
            return this.GraphStruct[Element];
        }

        public List<T> SelectAll()
        {
            if (this.GraphStruct.Count == 0)
            {
                throw new GraphException("Graph structure is empty");
            }
            return new List<T>(this.GraphStruct.Keys);
        }

        public int GetCount()
        {
            return this.GraphStruct.Count;
        }
    }
}
