using LyThuyetDoThi_DoAn.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LyThuyetDoThi_DoAn.services.YeuCau4
{
    public class FindShortedPath
    { 
        private Graph graph;
        int[,] Path = new int [100, 100];
        
        public FindShortedPath(Graph graph_input)
        {
            graph = graph_input;
            // Gán trọng số giữa hai đỉnh phân biệt không có đường đi
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (graph.AdjacencyMatrix[i, j] == 0)
                    {
                        if (i == j)
                        {
                            continue;
                        }
                        graph.AdjacencyMatrix[i, j] = 9999;
                    }

                }
            }
        }

        // Kiểm tra đồ thị có chứa trọng số âm nào không
        public void Check()
        {
            bool flag = true;
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
            for (int j = 0; j < graph.NumberOfVertices; j++)
            {
                if (graph.AdjacencyMatrix[i, j] < 0)
                {
                    flag = false;
                    break;
                }
            }
            }

            if (flag == false)
            {
                Console.WriteLine("Do thi khong thoa man dieu kien de xet yeu cau 4.");
                Console.WriteLine("=> Do thi co chua trong so am.");
                return;
            }
            else
                Floyd();
        } 
        

        public void Floyd()
        {
            // Gán giá trị ban đầu cho ma trận đường đi
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (graph.AdjacencyMatrix[i, j] > 0 && graph.AdjacencyMatrix[i, j] < 9999)
                    {
                        Path[i, j] = j;
                    }
                    else
                    {
                        Path[i, j] = -1;
                    }
                }
            }

            // Thuật toán Floyd-Warshall tìm đường đi ngắn nhất  
            for (int k = 0; k < graph.NumberOfVertices; k++)
            {
                for (int i = 0; i < graph.NumberOfVertices; i++)
                {
                    for (int j = 0; j < graph.NumberOfVertices; j++)
                    {
                        if (graph.AdjacencyMatrix[i, j] > graph.AdjacencyMatrix[i, k] + graph.AdjacencyMatrix[k, j])
                        {
                            graph.AdjacencyMatrix[i, j] = graph.AdjacencyMatrix[i, k] + graph.AdjacencyMatrix[k, j];
                            Path[i, j] = Path[i, k];
                        }
                    }
                }
            }

            // In đường đi ngắn nhất giữa các cặp đỉnh
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                Console.WriteLine("Duong di xuat phat tu {0}: ", i);
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (graph.AdjacencyMatrix[i, j] > 0 && graph.AdjacencyMatrix[i, j] < 9999)
                    {
                        PrintPath(i, j);
                    }
                    else if (graph.AdjacencyMatrix[i, j] == 9999)
                    {
                        Console.WriteLine("{0} -> {1}: Khong co duong di nao", i, j);
                    }
                }
            }
        }

        // In đường đi giữa cặp đỉnh u, v
        public void PrintPath(int u, int v)
        {
            int a = u;
            int b = v;
            Console.Write(u);
            int k = u;
            while (k != v)
            {
                Console.Write(" -> {0}", Path[k, v]);
                k = Path[k, v];
            }
            Console.Write(": {0}", graph.AdjacencyMatrix[a, b]);
            Console.WriteLine();
        }
    }
}

