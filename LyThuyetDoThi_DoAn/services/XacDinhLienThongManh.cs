using LyThuyetDoThi_DoAn.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LyThuyetDoThi_DoAn.XacDinhLienThongManh
{
    public class XacDinhLienThongManh
    {
        private Graph graph;
        private bool[] used;
        //Stack dung cho thuat toan dsf
        Stack<int> st = new Stack<int>();
        //Danh sach gom cac phan tu la cac dinh trong 1 thanh phan lien thong manh    
        List<int> result;
        //Danh sach nay chua cac danh sach cua cac phan tu trong 1 thanh phan lien thong manh
        private List<List<int>> listThanhPhan;
        //Check do thi lien thong yeu
        bool weakChecked = true;

        public XacDinhLienThongManh(Graph graph_input)
        {
            graph = graph_input;
            result = new List<int>();   

            listThanhPhan = new List<List<int>>();

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

        //Kiem tra xem co phai do thi lien thong yeu 
        public void IsWeaklyConnected()
        {
            bool lowertri = true;
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (i < j && graph.AdjacencyMatrix[i, j] == 0)
                    {
                        lowertri = false;
                        break;
                    }
                }
                if (!lowertri)
                {
                    break;
                }
            }
            if (lowertri)
            {
                weakChecked = false;
            }
            else
            {
                weakChecked = true;
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
                    listThanhPhan.Add(cloneResult);
                    result.Clear();
                    i++;
                }
            }
            show();
        }

        public void show()
        {
            if (listThanhPhan.Count == 1)
            {
                Console.WriteLine("Do Thi Lien Thong Manh");
                Console.Write($"Thanh phan lien thong manh 1: ");
                foreach (int item in listThanhPhan[0])
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
                return;
            }
            for (int i = 0; i < listThanhPhan.Count - 1; i++)
            {
                for (int j = 0; j < listThanhPhan.Count - i - 1; j++)
                {
                    if (listThanhPhan[j][0] > listThanhPhan[j + 1][0])
                    {
                        List<int> temp = listThanhPhan[j];
                        listThanhPhan[j] = listThanhPhan[j + 1];
                        listThanhPhan[j + 1] = temp;
                    }
                }
            }
            if (listThanhPhan.Count > 1)
            {
                for (int l = 0; l < graph.NumberOfVertices; l++)
                {
                    for (int j = 0; j < graph.NumberOfVertices; j++)
                    {
                        if (graph.AdjacencyMatrix[l, j] == 0 && graph.AdjacencyMatrix[j, l] == 0)
                        {
                            for (int n = 0; n < graph.NumberOfVertices; n++)
                            {
                                if (graph.AdjacencyMatrix[j, n] != 0)
                                {
                                    Console.WriteLine("Do Thi Lien Thong yeu");
                                    break;
                                } else
                                {
                                    Console.WriteLine("Do Thi Khong Lien Thong");
                                    break;
                                }
                            }                                                        
                            int k = 1;
                            foreach (List<int> sublist in listThanhPhan)
                            {
                                Console.Write($"Thanh phan lien thong manh {k++}: ");
                                foreach (int item in sublist)
                                {
                                    Console.Write(item + " ");
                                }
                                Console.WriteLine();
                            }
                            return;
                        }
                    }
                }


                Console.WriteLine("Do Thi Lien Thong Tung Phan");
                int i = 1;
                foreach (List<int> sublist in listThanhPhan)
                {
                    Console.Write($"Thanh phan lien thong manh {i++}: ");
                    foreach (int item in sublist)
                    {
                        Console.Write(item + " ");
                    }
                    Console.WriteLine();
                }
                return;
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

    class LienThong
    {
        private int dinh;
        private List<int>[] a;
        public LienThong() { }
        private LienThong(int dinh_input)
        {
            dinh = dinh_input;
            a = new List<int>[dinh];
            for (int i = 0; i < dinh; ++i)
                a[i] = new List<int>();
        }
        private void ThemCanh(int dinh, int canh)
        {
            a[dinh].Add(canh);
        }
        private void dfs(int dinh, bool[] used)
        {
            used[dinh] = true;
            List<int> list = a[dinh];
            foreach (var element in list)
            {
                if (!used[element])
                    dfs(element, used);
            }
        }
        private int[,] TaoMaTranDuongDi(Graph graph)
        {
            LienThong instance = new LienThong(graph.NumberOfVertices);
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (graph.AdjacencyMatrix[i, j] == 1)
                        instance.ThemCanh(i, j);
                }
            }
            int[,] maTran = new int[graph.NumberOfVertices, graph.NumberOfVertices];
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                bool[] used = new bool[graph.NumberOfVertices];
                instance.dfs(i, used);
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (used[j] == true)
                        maTran[i, j] = 1;
                }
            }
            return maTran;
        }
        private bool LienThongManh(Graph graph)
        {
            int[,] maTran = TaoMaTranDuongDi(graph);
            bool LienThongManh = true;
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (maTran[i, j] == 0)
                    {
                        LienThongManh = false;
                        break;
                    }
                }
            }
            return LienThongManh;
        }
        private bool LienThongTungPhan(Graph graph)
        {
            int[,] maTran = TaoMaTranDuongDi(graph);
            bool LienThongTungPhan = true;
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (maTran[i, j] == 0 & maTran[j, i] == 0)
                    {
                        LienThongTungPhan = false;
                        break;
                    }
                }
            }
            return LienThongTungPhan;
        }
        private bool LienThongYeu(Graph graph)
        {
            LienThong f = new LienThong(graph.NumberOfVertices);
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (graph.AdjacencyMatrix[i, j] == 1)
                        f.ThemCanh(i, j);
                }
            }
            LienThong k = new LienThong(graph.NumberOfVertices);
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                for (int j = 0; j < f.a[i].Count; j++)
                {
                    k.ThemCanh(i, f.a[i][j]);
                    k.ThemCanh(f.a[i][j], i);
                }
            }
            int[,] maTran = new int[graph.NumberOfVertices, graph.NumberOfVertices];
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                bool[] used = new bool[graph.NumberOfVertices];
                k.dfs(i, used);
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (used[j] == true)
                        maTran[i, j] = 1;
                }
            }
            bool LienThongYeu = true;
            for (int i = 0; i < graph.NumberOfVertices; i++)
            {
                for (int j = 0; j < graph.NumberOfVertices; j++)
                {
                    if (maTran[i, j] == 0)
                    {
                        LienThongYeu = false;
                        break;
                    }
                }
            }
            return LienThongYeu;
        }
        public void Xuat(Graph graph)
        {
            if (LienThongManh(graph))
                Console.WriteLine("Do thi lien thong manh");
            else if (LienThongTungPhan(graph))
                Console.WriteLine("Do thi lien thong tung phan");
            else if (LienThongYeu(graph))
                Console.WriteLine("Do thi lien thong yeu");
            else
                Console.WriteLine("Do thi khong lien thong");
        }
    }
}
