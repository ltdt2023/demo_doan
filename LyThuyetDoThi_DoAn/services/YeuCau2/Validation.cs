using LyThuyetDoThi_DoAn.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LyThuyetDoThi_DoAn.Validation 
{
    public class Validation 
    {
        private Graph graph;

        private List<int>[] adjacency;

        public Validation() { }

        private Validation(Graph graph_input)
        {
            int vertices = graph_input.NumberOfVertices;
            adjacency = new List<int>[vertices];
            for (int i = 0; i < vertices; ++i)
                adjacency[i] = new List<int>();
        }

        private void dfs(int vertice, bool[] used)
        {
            used[vertice] = true;
            List<int> list = adjacency[vertice];
            foreach (var element in list)
            {
                if (!used[element])
                    dfs(element, used);
            }
        }

        private int[,] createMatrixThrough(Graph graph)
        {
            Validation instance = new Validation(graph);
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (graph.AdjacencyMatrix[i, j] == 1)
                        instance.adjacency[i].Add(j);
                }
            }
            int[,] newMatrix = new int[graph.NumberOfVertices, graph.NumberOfVertices];
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                bool[] used = new bool[graph.NumberOfVertices];
                instance.dfs(i, used);
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (used[j] == true)
                        newMatrix[i, j] = 1;
                }
            }
            return newMatrix;
        }

        private bool isStrongConnected(Graph graph) {
            int[,] newMatrix = createMatrixThrough(graph);
            bool isStrong = true;
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (newMatrix[i, j] == 0)
                    {
                        isStrong = false;
                        break;
                    }
                }
            }
            return isStrong;
        }

        private bool isPartConnected(Graph graph) {
            int[,] newMatrix = createMatrixThrough(graph);
            bool isPart = true;
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (newMatrix[i, j] == 0 & newMatrix[j, i] == 0)
                    {
                        isPart = false;
                        break;
                    }
                }
            }
            return isPart;
        }

        private bool isWeakConnected(Graph graph) {
            Validation matrix_1 = new Validation(graph);
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (graph.AdjacencyMatrix[i, j] == 1)
                        matrix_1.adjacency[i].Add(j);
                }
            }
            Validation matrix_2 = new Validation(graph);
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                for (int j = 0; j < matrix_1.adjacency[i].Count; j++)
                {
                    matrix_2.adjacency[i].Add(matrix_1.adjacency[i][j]);
                    matrix_2.adjacency[matrix_1.adjacency[i][j]].Add(i);
                }
            }
            int[,] newMatrix = new int[graph.NumberOfVertices, graph.NumberOfVertices];
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                bool[] used = new bool[graph.NumberOfVertices];
                matrix_2.dfs(i, used);
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (used[j] == true)
                        newMatrix[i, j] = 1;
                }
            }
            bool isWeak = true;
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (newMatrix[i, j] == 0)
                    {
                        isWeak = false;
                        break;
                    }
                }
            }
            return isWeak;
        }

        public void display(Graph graph)
        {
            if (isStrongConnected(graph))
                Console.WriteLine("Do thi lien thong manh");
            else if (isPartConnected(graph))
                Console.WriteLine("Do thi lien thong tung phan");
            else if (isWeakConnected(graph))
                Console.WriteLine("Do thi lien thong yeu");
            else
                Console.WriteLine("Do thi khong lien thong");
        }
    }
}