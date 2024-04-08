using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyThuyetDoThi_DoAn.Entity
{
    public class Graph
    {
        public int[,] adjacencyMatrix; // Mảng hai chiều để lưu trữ ma trận kề
        public int numberOfVertices; // Số đỉnh của đồ thị
        
        //Constructor -- 
        public Graph(int numberOfVertices)
        {
            this.numberOfVertices = numberOfVertices;
            adjacencyMatrix = new int[numberOfVertices, numberOfVertices];
        }

        //getter for numberOfVertices
        public int NumberOfVertices
        {
            get { return numberOfVertices; }
        }

        //getter for AdjacencyMatrix
        public int[,] AdjacencyMatrix
        {
            get { return adjacencyMatrix; }
        }

        //Hàm in danh sách kề
        public void InDanhSachKe()
        {
            for (int i = 0; i < numberOfVertices; i++)
            {
                Console.Write(i + ": ");
                for (int j = 0; j < numberOfVertices; j++)
                {
                    if (adjacencyMatrix[i, j] != 0)
                    {
                        Console.Write("(" + j + ", " + adjacencyMatrix[i, j] + ") ");
                    }
                }
                Console.WriteLine();
            }
        }

        //Hàm in ma trận kề
        public void InMaTranKe(int[,] adjacencyMatrix)
        {
            for (int i = 0; i < numberOfVertices; i++)
            {

                for (int j = 0; j < numberOfVertices; j++)
                {

                    Console.Write("(" + adjacencyMatrix[i, j] + ") ");

                }
                Console.WriteLine();
            }
        }

        //Hàm thêm cạnh vào ma trận kề
        public void AddEdge(int source, int destination, int weight)
        {
            adjacencyMatrix[source, destination] = weight;
        }


        //Hàm chuyển đổi trọng số Ma trận kề 
        public int[,] TransformWeights(int[,] adjacencyMatrix)
        {
            int[,] newAdjacencyMatrix = new int[adjacencyMatrix.GetLength(0), adjacencyMatrix.GetLength(1)];

            for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < adjacencyMatrix.GetLength(1); j++)
                {
                    if (adjacencyMatrix[i, j] != 0)
                    {
                        newAdjacencyMatrix[i, j] = 1; // Đặt trọng số thành 1 nếu khác không
                    }
                }
            }

            return newAdjacencyMatrix;
        }

        //Hàm kiểm tra đồ thị vô hướng hay có hướng (checked)
        public bool IsUnDirected(int[,] adjacencyMatrix)
        {
            for (int i = 0; i < numberOfVertices; i++)
            {
                for (int j = 0; j < numberOfVertices; j++)
                {
                    if (adjacencyMatrix[i, j] != adjacencyMatrix[j, i])
                    {
                        return false; // Nếu có ít nhất một cặp cạnh không đối xứng, đồ thị có hướng
                    }
                }
            }
            return true; // Nếu tất cả các cặp cạnh đều đối xứng, đồ thị vô hướng
        }



        //Hàm kiểm tra đồ thị có chứa cạnh bội trong đồ thị có hướng hay không
        public bool ChuaCanhBoiTrongCoHuong(int[,] adjacencyMatrix)
        {
            int[,] newAdjacencyMatrix = TransformWeights(adjacencyMatrix);

            for (int i = 0; i < numberOfVertices; i++)
            {
                // Kiểm tra cạnh bội
                for (int j = 0; j < numberOfVertices; j++)
                {
                    if (newAdjacencyMatrix[i, j] == newAdjacencyMatrix[j, i])
                    {
                        return true; // Đồ thị chứa cạnh bội
                    }
                }
            }
            return false; // Đồ thị không chứa cạnh khuyên hoặc cạnh bội
        }

        //Hàm đếm số cạnh của đồ thị vô hướng 
        public int DemSoCanhCuaDoThiVoHuong(int[,] adjacencyMatrix)
        {
            int count = 0;
            for (int i = 0; i < numberOfVertices; i++)
            {

                for (int j = 0; j < numberOfVertices; j++)
                {
                    if (adjacencyMatrix[i, j] == adjacencyMatrix[j, i] && adjacencyMatrix[i, j] != 0 && adjacencyMatrix[j, i] != 0)
                    {
                        count += 1;
                    }
                }
            }

            return count / 2;
        }

        // Hàm kiểm tra xem một đỉnh có kề với bất kỳ đỉnh nào trong một tập hợp đỉnh không
        public bool IsAdjacentToAny(int vertex, HashSet<int> vertices, int[,] adjacencyMatrix)
        {
            foreach (int v in vertices)
            {
                if (adjacencyMatrix[vertex, v] != 0)
                {
                    return true;
                }
            }
            return false;
        }


        //Hàm kiểm tra đồ thị có liên thông

        public bool IsConnected(Graph graph)
        {
            int n = graph.adjacencyMatrix.GetLength(0); // Lấy số đỉnh của đồ thị
            bool[] visited = new bool[n]; // Mảng để đánh dấu các đỉnh đã được duyệt

            // Duyệt đồ thị từ đỉnh đầu tiên (đỉnh 0)
            DFS(graph, 0, visited);

            // Kiểm tra xem tất cả các đỉnh đã được duyệt hay không
            for (int i = 0; i < n; i++)
            {
                if (!visited[i])
                {
                    return false; // Nếu có đỉnh nào không được duyệt, đồ thị không liên thông
                }
            }

            return true; // Nếu tất cả các đỉnh đều được duyệt, đồ thị là liên thông
        }


        //Thuật toán DFS - Depth First Search
        public void DFS(Graph graph, int vertex, bool[] visited)
        {
            visited[vertex] = true; // Đánh dấu đỉnh hiện tại là đã được duyệt

            // Duyệt qua tất cả các đỉnh kề của đỉnh hiện tại
            for (int i = 0; i < graph.adjacencyMatrix.GetLength(1); i++)
            {
                if (graph.adjacencyMatrix[vertex, i] != 0 && !visited[i])
                {
                    DFS(graph, i, visited); // Duyệt đỉnh kề chưa được duyệt
                }
            }
        }



        // Hàm xác định chu trình Euler xuất phát từ đỉnh source với quy ước chọn hướng đi bằng thuật toán Fleury
        public void FindFleuryCycle(int source)
        {
            // Dictionary để lưu các cạnh đã được thăm
            Dictionary<string, bool> visitedEdges = new Dictionary<string, bool>();

            // List để lưu chu trình Euler
            List<int> eulerCycleList = new List<int>();

            // Thêm đỉnh bắt đầu vào List
            eulerCycleList.Add(source);

            while (eulerCycleList.Count > 0)
            {
                int currentVertex = eulerCycleList.Last();
                bool edgeFound = false;

                // Tạo danh sách các đỉnh kề của currentVertex theo thứ tự chỉ mục từ nhỏ đến lớn
                List<int> neighbors = new List<int>();
                for (int nextVertex = 0; nextVertex < numberOfVertices; nextVertex++)
                {
                    if (adjacencyMatrix[currentVertex, nextVertex] > 0 && !visitedEdges.ContainsKey(currentVertex + "-" + nextVertex))
                    {
                        neighbors.Add(nextVertex);
                    }
                }

                neighbors.Sort((a, b) => b.CompareTo(a));

                foreach (int nextVertex in neighbors)
                {
                    // Nếu có cạnh từ currentVertex đến nextVertex và cạnh chưa được thăm
                    if (adjacencyMatrix[currentVertex, nextVertex] > 0 && !visitedEdges.ContainsKey(currentVertex + "-" + nextVertex))
                    {
                        // Đánh dấu cạnh đã được thăm
                        visitedEdges[currentVertex + "-" + nextVertex] = true;

                        // Thêm nextVertex vào chu trình
                        eulerCycleList.Add(nextVertex);

                        // Loại bỏ cạnh đã đi qua
                        adjacencyMatrix[currentVertex, nextVertex]--;
                        adjacencyMatrix[nextVertex, currentVertex]--;

                        // Đánh dấu đã tìm thấy cạnh
                        edgeFound = true;

                        // Thoát vòng lặp
                        break;
                    }
                }

                // Nếu không tìm thấy cạnh nào đi từ currentVertex

                if (!edgeFound)
                {
                    // Lấy đỉnh cuối cùng từ List và in ra
                    int vertex = eulerCycleList.Last();
                    Console.Write(vertex + " ");
                    eulerCycleList.RemoveAt(eulerCycleList.Count - 1);
                }
            }
        }


        public void FindEulerPath(int source)
        {
            if (!IsOddDegree(source))
            {
                Console.WriteLine("Khong co loi giai!!!");
                return;
            }

            // Khởi tạo stack để lưu trữ đường đi
            Stack<int> stack = new Stack<int>();

            // Khởi tạo danh sách kết quả
            List<int> result = new List<int>();

            // Tạo một bản sao của ma trận kề để không làm thay đổi ma trận gốc
            int[,] tempAdjacencyMatrix = (int[,])adjacencyMatrix.Clone();

            // Thêm đỉnh nguồn vào stack
            stack.Push(source);

            while (stack.Count > 0)
            {
                int currentVertex = stack.Peek();
                bool found = false;

                // Duyệt qua tất cả các đỉnh kề của đỉnh hiện tại
                for (int i = 0; i < numberOfVertices; i++)
                {
                    if (tempAdjacencyMatrix[currentVertex, i] > 0)
                    {
                        // Nếu tìm thấy đỉnh kề, thêm vào stack và loại bỏ cạnh
                        stack.Push(i);
                        tempAdjacencyMatrix[currentVertex, i] = 0;
                        tempAdjacencyMatrix[i, currentVertex] = 0;
                        found = true;
                        break;
                    }
                }

                // Nếu không tìm thấy đỉnh kề, pop đỉnh hiện tại ra khỏi stack và thêm vào kết quả
                if (!found)
                {
                    result.Add(stack.Pop());
                }
            }

            // In ra đường đi Euler
            Console.Write("Duong di: ");
            for (int i = result.Count - 1; i >= 0; i--)
            {
                Console.Write(result[i]);
                if (i > 0)
                {
                    Console.Write(" - ");
                }
            }
        }

        //Kiểm tra xem 1 đỉnh có phải đỉnh bậc lẻ hay không
        public bool IsOddDegree(int vertex)
        {
            int degree = 0;
            for (int i = 0; i < numberOfVertices; i++)
            {
                degree += adjacencyMatrix[vertex, i];
            }
            return degree % 2 != 0;
        }
    
    }
}
