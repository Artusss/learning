using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Graph
{
    interface ICounter
    {
        public int GetCount();
    }
    abstract class BaseGraph<T>
    {
        public abstract void InsertElement(T Element);

        public abstract void InsertNodes(T Element, List<T> RelationList);

        public abstract void FlushElement(T Element);

        public abstract List<T> SelectNodes(T Element);

        public abstract List<T> SelectAll();
    }
    class Graph<T> : BaseGraph<T>, ICounter
    {
        private Dictionary<T, List<T>> GraphStruct;

        public Graph()
        {
            this.GraphStruct = new Dictionary<T, List<T>>();
        }

        public override void InsertElement(T Element)
        {
            if (this.GraphStruct.ContainsKey(Element))
            {
                throw new Exception("This graph structure also have element with this value");
            }
            this.GraphStruct.Add(Element, new List<T>());
        }

        public override void InsertNodes(T Element, List<T> RelationList)
        {
            if (RelationList.Count == 0)
            {
                throw new Exception("This element don't have relations");
            }
            if (!this.GraphStruct.ContainsKey(Element))
            {
                throw new Exception($"Element `{Element}` not found");
            }
            foreach (T RelationElement in RelationList)
            {
                if (!this.GraphStruct.ContainsKey(RelationElement))
                {
                    throw new Exception($"RelationElement `{RelationElement}` not found");
                }
            }
            foreach (T RelationElement in new HashSet<T>(RelationList))
            {
                if (this.GraphStruct[Element].SingleOrDefault(i => i.Equals(RelationElement)) != null)
                {
                    throw new Exception($"Node {Element}->{RelationElement} also exists");
                }
                if (this.GraphStruct[RelationElement].SingleOrDefault(i => i.Equals(Element)) != null)
                {
                    throw new Exception($"Node {RelationElement}->{Element} also exists");
                }
                this.GraphStruct[Element].Add(RelationElement);
                this.GraphStruct[RelationElement].Add(Element);
            }
        }

        public override void FlushElement(T Element)
        {
            if (!this.GraphStruct.ContainsKey(Element))
            {
                throw new Exception($"Element `{Element}` not found");
            }
            foreach (T RelationElement in this.GraphStruct[Element])
            {
                this.GraphStruct[RelationElement].Remove(Element);
            }
            this.GraphStruct.Remove(Element);
        }

        public override List<T> SelectNodes(T Element)
        {
            if (!this.GraphStruct.ContainsKey(Element))
            {
                throw new Exception($"Element `{Element}` not found");
            }
            return this.GraphStruct[Element];
        }

        public override List<T> SelectAll()
        {
            if (this.GraphStruct.Count == 0)
            {
                throw new Exception("Graph structure is empty");
            }
            List<T> AllElements = new List<T>(this.GraphStruct.Keys);
            return AllElements;
        }

        public int GetCount()
        {
            return this.GraphStruct.Count;
        }
    }
}
