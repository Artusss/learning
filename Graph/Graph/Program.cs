using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Graph;


namespace Graph
{
    class Program
    {
        public static string widthSearch(string MainItem, Graph.Graph<string> graph)
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
            return SearchedItem[0].Equals("J".ToString());
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
            Console.ReadKey();            
        }
    }
}
