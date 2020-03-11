using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Graph;


namespace Graph
{
    class Program
    {
        public static bool widthSearch<T>(T MainItem, T SearchedItem, Graph.Graph<T> graph)
        {
            if (graph.SelectNodes(MainItem) == null)
            {
                throw new Exception("Main element not found");
            }

            List<T> searchedList = new List<T>();

            Queue<T> queue = new Queue<T>();
            foreach (T graphElement in graph.SelectNodes(MainItem))
            {
                queue.Enqueue(graphElement);
            }

            while (queue.Count != 0)
            {
                T queueItem = queue.Dequeue();

                if (!searchedList.Contains(queueItem))
                {
                    if (searchValidator<T>(queueItem, SearchedItem)) return true;
                    else
                    {
                        searchedList.Add(queueItem);
                        foreach (T graphElement in graph.SelectNodes(queueItem))
                        {
                            queue.Enqueue(graphElement);
                        }
                    }
                }
            }

            return false;
        }

        public static bool searchValidator<T>(T SearchedItem, T ValidItem)
        {
            return SearchedItem.Equals(ValidItem);
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
                Console.WriteLine(widthSearch<string>("Me", "Bob", graph));
                Console.WriteLine(widthSearch<string>("Me", "aedaeae", graph));
                Console.WriteLine(widthSearch<string>("Me", "Paggy", graph));
                Console.WriteLine(widthSearch<string>("Me", "Tom", graph));
                Console.WriteLine(widthSearch<string>("Me", "qweert", graph));
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
