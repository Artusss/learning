using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Graph;


namespace Graph
{
    class Program
    {
        delegate Dictionary<int, List<T>> SearchGraphHandler<T>(T fromElement, T toElement, Graph<T> graph);
        delegate Dictionary<int, List<T>> SearchWGraphHandler<T>(T fromElement, T toElement, WeightedGraph<T> graph);

        public static void ShowShortestRoute<T>(T fromElement, T toElement, BaseGraph<T> graph)
        {
            Dictionary<int, List<T>> FullPath;
            int TotalCost;
            if (graph.GetType().Equals(typeof(Graph<T>)))
            {
                SearchGraphHandler<T> searchGraphHandler;
                searchGraphHandler = widthSearch<T>;
                FullPath = searchGraphHandler(fromElement, toElement, (Graph<T>)graph);
                TotalCost = FullPath.First().Key;
                ShowRouteInfo<T>(FullPath, TotalCost);
            }
            else if (graph.GetType().Equals(typeof(WeightedGraph<T>)))
            {
                SearchWGraphHandler<T> searchWGraphHandler;
                searchWGraphHandler = DijkstraAlg<T>;
                FullPath = searchWGraphHandler(fromElement, toElement, (WeightedGraph<T>)graph);
                TotalCost = FullPath.First().Key;
                ShowRouteInfo<T>(FullPath, TotalCost);
            }
        }

        private static void ShowRouteInfo<T>(Dictionary<int, List<T>> FullPath, int TotalCost)
        {
            Console.WriteLine($"TotalCost: {TotalCost}");
            foreach (var RouteNode in FullPath.First().Value)
            {
                Console.Write($"{RouteNode}");
                if (!RouteNode.Equals(FullPath.First().Value.Last())) Console.Write(" ---> ");
            }
            Console.WriteLine();
        }

        public static Dictionary<int, List<T>> DijkstraAlg<T>(T fromElement, T toElement, WeightedGraph<T> wGraph)
        {
            if (wGraph.SelectNodes(fromElement) == null) throw new GraphException("FromElement not found");
            if (wGraph.SelectNodes(toElement) == null)   throw new GraphException("ToElement not found");

            Dictionary<T, int> CostNodes      = new Dictionary<T, int>();
            Dictionary<T, T> ParentNodes      = new Dictionary<T, T>();
            List<T> VerifiedNodes             = new List<T>();
            Dictionary<int, List<T>> FullPath = new Dictionary<int, List<T>>();

            foreach (var Node in wGraph.SelectAll())
            {
                CostNodes  .Add(Node, int.MaxValue);
                ParentNodes.Add(Node, default);
            }
            CostNodes[fromElement] = 0;
            foreach (var Node in wGraph.SelectNodes(fromElement))
            {
                CostNodes[Node.Value]   = Node.Key;
                ParentNodes[Node.Value] = fromElement;
            }
            
            T CurNode = findLowCostNode<T>(CostNodes, VerifiedNodes);
            while (VerifiedNodes.Count < wGraph.GetCount())
            {
                if (CurNode != null)
                {
                    int CurCost      = CostNodes[CurNode];
                    var CurNeighbors = wGraph.SelectNodes(CurNode);
                    foreach (T CurNeighbor in CurNeighbors.Values)
                    {
                        int newCost = CurCost + CurNeighbors.SingleOrDefault(i => i.Value.Equals(CurNeighbor)).Key;
                        if (CostNodes[CurNeighbor] > newCost)
                        {
                            CostNodes[CurNeighbor]   = newCost;
                            ParentNodes[CurNeighbor] = CurNode;
                        }
                    }
                    VerifiedNodes.Add(CurNode);
                    CurNode = findLowCostNode<T>(CostNodes, VerifiedNodes);
                }
                else break;
            }

            FullPath.Add(CostNodes[toElement], new List<T>());
            FullPath[CostNodes[toElement]].Add(toElement);

            T CurParentNode = ParentNodes[toElement];
            while (!CurParentNode.Equals(fromElement))
            {
                FullPath[CostNodes[toElement]].Add(CurParentNode);
                CurParentNode = ParentNodes[CurParentNode];
            }
            FullPath[CostNodes[toElement]].Add(fromElement);
            FullPath[CostNodes[toElement]].Reverse();

            return FullPath;
        }
        private static T findLowCostNode<T>(Dictionary<T, int> CostNodes, List<T> VerifiedNodes)
        {
            int LowCost   = int.MaxValue;
            T LowCostNode = default;
            foreach (var CostNode in CostNodes)
            {
                if (CostNode.Value < LowCost && !VerifiedNodes.Contains(CostNode.Key))
                {
                    LowCost     = CostNode.Value;
                    LowCostNode = CostNode.Key;
                }
            }
            return LowCostNode;
        }


        public static Dictionary<int, List<T>> widthSearch<T>(T MainItem, T ToSearchItem, Graph<T> graph)
        {
            if (graph.SelectNodes(MainItem) == null)     throw new GraphException("Main element not found");
            if (graph.SelectNodes(ToSearchItem) == null) throw new GraphException("ToSearch element not found");
            if (MainItem.Equals(ToSearchItem))           return new Dictionary<int, List<T>>() { { 0, new List<T>() { MainItem } } };

            List<T> searchedList         = new List<T>();
            Queue<T> queue               = new Queue<T>();
            Dictionary<T, T> ParentNodes = new Dictionary<T, T>();
            List<T> FullPath = new List<T>();

            searchedList.Add(MainItem);
            foreach (var Node in graph.SelectAll())           ParentNodes.Add(Node, default);
            foreach (var Node in graph.SelectNodes(MainItem)) ParentNodes[Node] = MainItem;

            foreach (T graphElement in graph.SelectNodes(MainItem)) queue.Enqueue(graphElement);

            while (queue.Count != 0)
            {
                T queueItem = queue.Dequeue();
                if (!searchedList.Contains(queueItem))
                {
                    if (searchValidator(queueItem, ToSearchItem)) break;
                    else
                    {
                        searchedList.Add(queueItem);
                        foreach (T graphElement in graph.SelectNodes(queueItem))
                        {
                            if (!queue.Contains(graphElement) && !searchedList.Contains(graphElement))
                            {
                                queue.Enqueue(graphElement);
                                ParentNodes[graphElement] = queueItem;
                            }
                        }
                    }
                }
            }

            FullPath.Add(ToSearchItem);
            T CurParent = ParentNodes[ToSearchItem];
            while (!CurParent.Equals(MainItem))
            {
                FullPath.Add(CurParent);
                CurParent = ParentNodes[CurParent];
            }
            FullPath.Add(MainItem);
            FullPath.Reverse();

            return new Dictionary<int, List<T>>() { { FullPath.Count() - 1, FullPath } };
        }

        public static bool searchValidator<T>(T SearchedItem, T ToSearchItem)
        {
            return SearchedItem.Equals(ToSearchItem);
        }

        static void Main(string[] args)
        {
            try
            {
                Graph<String> graph = new Graph<string>();

                Dictionary<String, List<String>> graphElements = new Dictionary<String, List<String>>();
                graphElements["Me"]     = new List<String>() { "Bob2", "Klare", "Alice" };
                graphElements["Bob2"] = new List<String>() { "Bob" };
                graphElements["Bob"]    = new List<String>() { "Anudzh", "Paggy" };
                graphElements["Alice"]  = new List<String>() { "Paggy" };
                graphElements["Klare"]  = new List<String>() { "Tom", "John" };
                graphElements["Anudzh"] = new List<String>();
                graphElements["Paggy"]  = new List<String>();
                graphElements["Tom"]    = new List<String>();
                graphElements["John"]   = new List<String>();

                foreach (var graphElement in graphElements) graph.InsertElement(graphElement.Key);
                foreach (var graphElement in graphElements)
                {
                    if (graphElement.Value.Count > 0) graph.InsertNodes(graphElement.Key, graphElement.Value);
                }

                foreach (var graphElement in graphElements)
                {
                    List<String> Nodes = graph.SelectNodes(graphElement.Key);
                    Console.WriteLine($"For: {graphElement.Key}");
                    foreach (String RelationEelement in Nodes) Console.WriteLine($"{RelationEelement};");
                }
                ShowShortestRoute<string>("Bob", "John", graph);
            }
            catch (GraphException e)
            {
                Console.WriteLine(e.ToString());
            }

            try
            {
                WeightedGraph<string> wGraph = new WeightedGraph<string>();
                Dictionary<string, Dictionary<int, string>> wGraphElements = new Dictionary<string, Dictionary<int, string>>();
                wGraphElements["TwinPicks"]  = new Dictionary<int, string>(){ { 4, "TP_GG_0_0" }, { 10, "TP_GG_1_0" } };
                wGraphElements["TP_GG_0_0"]  = new Dictionary<int, string>(){ { 21, "TP_GG_0_1" } };
                wGraphElements["TP_GG_1_0"]  = new Dictionary<int, string>(){ { 5, "TP_GG_1_1" }, { 8, "TP_GG_1_2" } };
                wGraphElements["TP_GG_1_1"]  = new Dictionary<int, string>(){ { 5, "TP_GG_0_1" } };
                wGraphElements["TP_GG_1_2"]  = new Dictionary<int, string>(){ { 12, "TP_GG_0_1" } };
                wGraphElements["TP_GG_0_1"]  = new Dictionary<int, string>(){ { 4, "GoldenGate" } };
                wGraphElements["GoldenGate"] = new Dictionary<int, string>();

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
                    foreach (var RelationEelement in Nodes) Console.WriteLine($"{wGraphElement.Key}---{RelationEelement.Key}min-->{RelationEelement.Value};");
                }
                ShowShortestRoute<string>("TwinPicks", "GoldenGate", wGraph);
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
