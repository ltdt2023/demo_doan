using LyThuyetDoThi_DoAn.entity;
using LyThuyetDoThi_DoAn.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyThuyetDoThi_DoAn.services.YeuCau3
{
    internal class Kruskal
    {
        Graph graph;


        private int V; // Số lượng đỉnh trong đồ thị
        private List<Edge> edges; // Danh sách các cạnh của đồ thị

        public Kruskal(Graph doThi)
        {
            graph = doThi;
            V = doThi.numberOfVertices;
            edges = new List<Edge>();
        }

        // Thêm một cạnh vào danh sách cạnh của đồ thị
        public void AddEdge(int[,] adjacencyMatrix)
        {
            for (int i = 0; i < V; i++)
            {
                for (int j = i + 1; j < V; j++)
                {
                    int weight = adjacencyMatrix[i, j];
                    if (weight != 0)
                    {
                        edges.Add(new Edge(i, j, weight));
                    }
                }
            }
        }

        // Tìm đỉnh cha của một đỉnh trong cây
        private int FindParent(int[] parent, int vertex)
        {
            if (parent[vertex] != vertex)
            {
                // Nếu đỉnh không phải là đỉnh cha của chính nó, tìm đỉnh cha của nó
                return FindParent(parent, parent[vertex]);
            }
            return vertex;
        }

        // Hợp nhất hai cây thành một cây duy nhất
        private void Union(int[] parent, int x, int y)
        {
            int xSet = FindParent(parent, x);
            int ySet = FindParent(parent, y);
            parent[xSet] = ySet;
        }

        // Thực hiện thuật toán Kruskal để tìm cây khung lớn nhất
        public void MaxSpanningTree()
        {
            // Sắp xếp danh sách các cạnh theo thứ tự giảm dần của trọng số
            edges.Sort((x, y) => y.Weight.CompareTo(x.Weight));

            int[] parent = new int[V];
            for (int i = 0; i < V; i++)
            {
                parent[i] = i; // Khởi tạo mỗi đỉnh là một cây con độc lập
            }

            Console.WriteLine("Giai thuat Kruskal");
            Console.WriteLine("Tap canh cua cay khung:");

            int totalWeight = 0;
            bool treeFound = false; // Biến kiểm tra xem cây khung đã được tìm thấy chưa
            foreach (Edge edge in edges)
            {
                int x = FindParent(parent, edge.Source);
                int y = FindParent(parent, edge.Destination);

                if (x != y)
                {
                    // Nếu cạnh không tạo thành chu trình, thêm cạnh vào cây khung và hợp nhất hai cây
                    Console.WriteLine(edge.Source + " - " + edge.Destination + ": " + edge.Weight);
                    totalWeight += edge.Weight;
                    Union(parent, x, y);
                    treeFound = true; // Đánh dấu rằng đã tìm thấy cây khung
                }
            }

            if (!treeFound)
            {
                Console.WriteLine("Khong tim thay cay khung.");
                return;
            }

            Console.WriteLine("\nTrong so cua cay khung:");
            Console.WriteLine(totalWeight);
        }

    }
}
