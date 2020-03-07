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

                Console.WriteLine("elem1_234");
                graph.InsertElement("elem1_234");              

                Console.WriteLine("elem2_156");
                graph.InsertElement("elem2_156");                

                Console.WriteLine("elem3_156");
                graph.InsertElement("elem3_156");
                
                Console.WriteLine("elem4_1");
                graph.InsertElement("elem4_1");
                
                Console.WriteLine("elem5_236");
                graph.InsertElement("elem5_236");
                
                Console.WriteLine("elem6_235");
                graph.InsertElement("elem6_235");

                graph.InsertNodes("elem1_234", new List<String>() {
                    "elem2_156", "elem3_156", "elem4_1"
                });
                graph.InsertNodes("elem2_156", new List<String>() {
                    "elem5_236", "elem6_235"
                });
                graph.InsertNodes("elem3_156", new List<String>() {
                    "elem5_236", "elem6_235"
                });      
                graph.InsertNodes("elem5_236", new List<String>() {
                    "elem6_235"
                });

                List<String> elem2_156Nodes = graph.SelectNodes("elem2_156");
                Console.WriteLine("For: elem2_156");
                foreach (String RelationEelement in elem2_156Nodes)
                {
                    Console.WriteLine($"{RelationEelement} ;");
                }

                List<String> elem4_1Nodes = graph.SelectNodes("elem4_1");
                Console.WriteLine("For: elem4_1");
                foreach (String RelationEelement in elem4_1Nodes)
                {
                    Console.WriteLine($"{RelationEelement} ;");
                }

                List<String> elem5_236Nodes = graph.SelectNodes("elem5_236");
                Console.WriteLine("For: elem5_236");
                foreach (String RelationEelement in elem5_236Nodes)
                {
                    Console.WriteLine($"{RelationEelement} ;");
                }

                Console.WriteLine(graph.GetCount());
                graph.FlushElement("elem6_235");                

                elem2_156Nodes = graph.SelectNodes("elem2_156");
                Console.WriteLine("For: elem2_156");
                foreach (String RelationEelement in elem2_156Nodes)
                {
                    Console.WriteLine($"{RelationEelement} ;");
                }

                elem4_1Nodes = graph.SelectNodes("elem4_1");
                Console.WriteLine("For: elem4_1");
                foreach (String RelationEelement in elem4_1Nodes)
                {
                    Console.WriteLine($"{RelationEelement} ;");
                }

                elem5_236Nodes = graph.SelectNodes("elem5_236");
                Console.WriteLine("For: elem5_236");
                foreach (String RelationEelement in elem5_236Nodes)
                {
                    Console.WriteLine($"{RelationEelement} ;");
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
