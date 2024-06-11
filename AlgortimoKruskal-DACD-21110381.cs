
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Crear un grafo de ejemplo (representado como una matriz de adyacencia)
        int[,] graph = {
            { 0, 4, 2, 0, 0 },
            { 4, 0, 1, 5, 0 },
            { 2, 1, 0, 8, 10 },
            { 0, 5, 8, 0, 2 },
            { 0, 0, 10, 2, 0 }
        };

        Kruskal(graph);
    }

    static void Kruskal(int[,] graph)
    {
        int n = graph.GetLength(0);
        List<Edge> edges = new List<Edge>();

        // Crear una lista de aristas con sus pesos
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (graph[i, j] != 0)
                {
                    edges.Add(new Edge(i, j, graph[i, j]));
                }
            }
        }

        // Ordenar las aristas por peso
        edges.Sort((a, b) => a.Weight.CompareTo(b.Weight));

        int[] parent = new int[n];
        for (int i = 0; i < n; i++)
        {
            parent[i] = i;
        }

        List<Edge> minimumSpanningTree = new List<Edge>();

        foreach (var edge in edges)
        {
            int root1 = Find(parent, edge.Source);
            int root2 = Find(parent, edge.Destination);

            if (root1 != root2)
            {
                minimumSpanningTree.Add(edge);
                Union(parent, root1, root2);
            }
        }

        Console.WriteLine("Árbol de Mínimo Coste (Kruskal):");
        foreach (var edge in minimumSpanningTree)
        {
            Console.WriteLine($"Arista ({edge.Source}, {edge.Destination}) con peso {edge.Weight}");
        }
    }

    static int Find(int[] parent, int vertex)
    {
        if (parent[vertex] != vertex)
        {
            parent[vertex] = Find(parent, parent[vertex]);
        }
        return parent[vertex];
    }

    static void Union(int[] parent, int root1, int root2)
    {
        parent[root2] = root1;
    }
}

class Edge
{
    public int Source { get; }
    public int Destination { get; }
    public int Weight { get; }

    public Edge(int source, int destination, int weight)
    {
        Source = source;
        Destination = destination;
        Weight = weight;
    }
}
