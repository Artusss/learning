using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Graph;


namespace Graph
{
    class Program
    {
        public static string widthSearch(string MainItem, Graph<string> graph)
        {
            if (graph.SelectNodes(MainItem) == null)
            {
                throw new Exception("Main element not found");
            }

            List<string> searchedList = new List<string>();

            Queue<string> queue = new Queue<string>();
            foreach (string graphElement in graph.SelectNodes(MainItem))
            {
                queue.Enqueue(graphElement);
            }

            while (queue.Count != 0)
            {
                string queueItem = queue.Dequeue();

                if (!searchedList.Contains(queueItem))
                {
                    if (searchValidator(queueItem)) return queueItem;
                    else
                    {
                        searchedList.Add(queueItem);
                        foreach (string graphElement in graph.SelectNodes(queueItem))
                        {
                            queue.Enqueue(graphElement);
                        }
                    }
                }
            }

            return "Not Found";
        }

        public static bool searchValidator(string SearchedItem)
        {
            return SearchedItem[0].Equals('J');
        }

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
                Console.WriteLine(widthSearch("Me", graph));
                Console.WriteLine(graph.GetCount());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            try
            {
                WeightedGraph<string> wGraph = new WeightedGraph<string>();
                Dictionary<string, Dictionary<int, string>> wGraphElements = new Dictionary<string, Dictionary<int, string>>();
                wGraphElements["TwinPicks"] = new Dictionary<int, string>(){ { 4, "TP_GG_0_0" }, { 10, "TP_GG_1_0" } };
                wGraphElements["TP_GG_0_0"] = new Dictionary<int, string>(){ { 21, "TP_GG_0_1" } };
                wGraphElements["TP_GG_1_0"] = new Dictionary<int, string>(){ { 5, "TP_GG_1_1" }, { 8, "TP_GG_1_2" } };
                wGraphElements["TP_GG_1_1"] = new Dictionary<int, string>(){ { 5, "TP_GG_0_1" } };
                wGraphElements["TP_GG_1_2"] = new Dictionary<int, string>(){ { 12, "TP_GG_0_1" } };
                wGraphElements["TP_GG_0_1"] = new Dictionary<int, string>(){ { 4, "GoldenGate" } };

                foreach (var wGraphElement in wGraphElements)
                {
                    wGraph.InsertElement(wGraphElement.Key);
                }
                foreach (var wGraphElement in wGraphElements)
                {
                    if (wGraphElement.Value.Count > 0)
                    {
                        wGraph.InsertNodes(wGraphElement.Key, wGraphElement.Value);
                    }
                }

                foreach (var wGraphElement in wGraphElements)
                {
                    Dictionary<int, string> Nodes = wGraph.SelectNodes(wGraphElement.Key);
                    Console.WriteLine($"For: {wGraphElement.Key}");
                    foreach (var RelationEelement in Nodes)
                    {
                        Console.WriteLine($"{wGraphElement.Key}---{RelationEelement.Key}min-->{RelationEelement.Value};");
                    }
                }

            }
            catch(GraphException ge)
            {
                Console.WriteLine(ge.ToString());
            }
            finally
            {
                Console.WriteLine("OK");
            }
            Console.ReadKey();            
        }
    }
}
