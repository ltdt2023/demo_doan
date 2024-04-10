using LyThuyetDoThi_DoAn.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyThuyetDoThi_DoAn.services.YeuCau3
{
    internal class Prim
    {
        Graph graph;
        public Prim(Graph input)
        {
            graph = input;
        }

        private int maxKey(int[] key, bool[] mstSet)
        {
            int max = int.MinValue, maxIndex = -1;

            for (int v = 0; v < graph.numberOfVertices; v++)
            {
                if (!mstSet[v] && key[v] > max)
                {
                    max = key[v];
                    maxIndex = v;
                }
            }

            return maxIndex;
        }

        // In ra cây khung lớn nhất đã tìm được
        public void MaxSpanningTree(int startVertex)
        {
            int[] parent = new int[graph.numberOfVertices];
            int[] key = new int[graph.numberOfVertices];
            bool[] mstSet = new bool[graph.numberOfVertices];

            for (int i = 0; i < graph.numberOfVertices; i++)
            {
                key[i] = int.MinValue;
                mstSet[i] = false;
            }

            key[startVertex] = 0;
            parent[startVertex] = -1;

            for (int count = 0; count < graph.numberOfVertices - 1; count++)
            {
                int u = maxKey(key, mstSet);

                mstSet[u] = true;

                for (int v = 0; v < graph.numberOfVertices; v++)
                {
                    if (graph.adjacencyMatrix[u, v] != 0 && !mstSet[v] && graph.adjacencyMatrix[u, v] > key[v])
                    {
                        parent[v] = u;
                        key[v] = graph.adjacencyMatrix[u, v];
                    }
                }
            }

            // In ra thông tin của cây khung lớn nhất bắt đầu từ đỉnh startVertex
            Console.WriteLine("Giai thuat Prim");
            Console.WriteLine("Tap canh cua cay khung:");

            int totalWeight = 0;
            for (int i = 0; i < graph.numberOfVertices; i++)
            {
                if (i != startVertex)
                {
                    Console.WriteLine(parent[i] + " - " + i + ": " + graph.adjacencyMatrix[i, parent[i]]);
                    totalWeight += graph.adjacencyMatrix[i, parent[i]];
                }
            }

            Console.WriteLine("\nTrong so cua cay khung:");
            Console.WriteLine(totalWeight);
        }

    }
}
