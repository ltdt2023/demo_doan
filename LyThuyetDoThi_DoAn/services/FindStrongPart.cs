using LyThuyetDoThi_DoAn.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LyThuyetDoThi_DoAn.FindStrongPart
{
    public class FindStrongPart
    {
        private Graph graph;
        private bool[] used;
        //Stack dung cho thuat toan dsf
        Stack<int> st = new Stack<int>();
        //Danh sach gom cac phan tu la cac dinh trong 1 thanh phan lien thong manh    
        List<int> result;
        //Danh sach nay chua cac danh sach cua cac phan tu trong 1 thanh phan lien thong manh
        private List<List<int>> listPart;

        public FindStrongPart(Graph graph_input)
        {
            graph = graph_input;
            result = new List<int>();

            listPart = new List<List<int>>();

            //Phep gan cac trong so ve 1
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (graph.AdjacencyMatrix[i, j] > 0)
                    {
                        graph.AdjacencyMatrix[i, j] = 1;
                    }
                }
            }
        }

        //getter for numberOfVertices
        public Graph Graph
        {
            get { return graph; }
        }

        //thuat toan dfs cho ma tran ke
        public void dfs_1(int dinh)
        {
            used[dinh] = true;
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                if (graph.AdjacencyMatrix[dinh, i] > 0 && !used[i])
                    dfs_1(i);
            }
            st.Push(dinh);
        }

        //dao nguoc ma tran ke
        public void reverse()
        {
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (graph.AdjacencyMatrix[i, j] == 1)
                    {
                        graph.AdjacencyMatrix[i, j] = 0;
                        //Gan bang 2 de hien thi danh danh khac ma tran 1
                        graph.AdjacencyMatrix[j, i] = 2;
                    }
                }
            }
        }

        //thuat toan cho ma tran ke dao nguoc
        private void dfs_2(int dinh)
        {
            used[dinh] = true;
            result.Add(dinh);
            for (int v = 0; v < graph.NumberOfVertices; v++)
            {
                if (graph.AdjacencyMatrix[dinh, v] == 2 && !used[v])
                    dfs_2(v);
            }
        }


        public void display()
        {
            used = new bool[graph.NumberOfVertices];
            for (int dinh = 0; dinh < graph.NumberOfVertices; dinh++)
            {
                if (!used[dinh])
                    dfs_1(dinh);

            }
            reverse();
            used = new bool[graph.NumberOfVertices];
            int i = 1;
            while (st.Count > 0)
            {
                int dinh = st.Pop();
                if (!used[dinh])
                {
                    //Console.Write($"Thanh phan lien thong manh {i}: ");
                    dfs_2(dinh);
                    result.Sort();
                    List<int> cloneResult = new List<int>(result);
                    listPart.Add(cloneResult);
                    result.Clear();
                    i++;
                }
            }
            show();
        }

        public void show()
        {
            //Bubble for sorting the part in list
            List<int> temp;
            for (int a = 0; a < listPart.Count - 1; a++) {
                for (int b = 0; b < listPart.Count - a - 1; b++) {
                    if (listPart[b][0] > listPart[b+1][0]) {
                        temp = listPart[b];
                        listPart[b] = listPart[b+1];
                        listPart[b+1] = temp;
                    }
                }
            }


            int i = 1;
            foreach (List<int> sublist in listPart)
            {
                Console.Write($"Thanh phan lien thong manh {i++}: ");
                foreach (int item in sublist)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
            }
        }

        public static bool isValidated(Graph graph)
        {
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (graph.AdjacencyMatrix[i, j] > 0 && graph.AdjacencyMatrix[j, i] > 0)
                    {
                        if (graph.AdjacencyMatrix[i, j] != graph.AdjacencyMatrix[j, i])
                        {
                            Console.WriteLine("Day la do thi co huong co canh boi");
                            return false;
                        }
                        Console.WriteLine("Day la do thi vo huong hoac do thi co huong co canh boi");
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
