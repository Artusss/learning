using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Graph
{
    class Graph<T>
    {
        private Dictionary<T, List<T>> GraphStruct;

        public Graph()
        {
            this.GraphStruct = new Dictionary<T, List<T>>();
        }

        public void InsertElement(T Element)
        {
            if (this.GraphStruct.ContainsKey(Element))
            {
                throw new Exception("This graph structure also have element with this value");
            }
            this.GraphStruct.Add(Element, new List<T>());
        }

        public void InsertNodes(T Element, List<T> RelationList)
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

        public void FlushElement(T Element)
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

        public List<T> SelectNodes(T Element)
        {
            if (!this.GraphStruct.ContainsKey(Element))
            {
                throw new Exception($"Element `{Element}` not found");
            }
            return this.GraphStruct[Element];
        }

        public List<T> SelectAll()
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

    class Program
    {        
        static void Main(string[] args)
        {
            try
            {

                Graph<String> graph = new Graph<string>();

                Dictionary<String, List<String>> graphElements = new Dictionary<String, List<String>>();
                graphElements["Me"] = new List<String>() { "Bob", "Klare", "Alice" };
                graphElements["Bob"] = new List<String>() { "Anudzh", "Paggy" };
                graphElements["Alice"] = new List<String>() { "Paggy" };
                graphElements["Klare"] = new List<String>() { "Tom", "John" };
                graphElements["Anudzh"] = new List<String>();
                graphElements["Paggy"] = new List<String>();
                graphElements["Tom"] = new List<String>();
                graphElements["John"] = new List<String>();

                foreach (var graphElement in graphElements)
                {
                    graph.InsertElement(graphElement.Key);
                }
                foreach (var graphElement in graphElements)
                {
                    if (graphElement.Value.Count > 0)
                    {
                        graph.InsertNodes(graphElement.Key, graphElement.Value);
                    }                    
                }

                foreach (var graphElement in graphElements)
                {
                    List<String> Nodes = graph.SelectNodes(graphElement.Key);
                    Console.WriteLine($"For: {graphElement.Key}");
                    foreach (String RelationEelement in Nodes)
                    {
                        Console.WriteLine($"{RelationEelement};");
                    }
                }

                Console.WriteLine(graph.GetCount());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadKey();            
        }
    }
}
