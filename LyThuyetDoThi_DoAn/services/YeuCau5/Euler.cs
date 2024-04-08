using LyThuyetDoThi_DoAn.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyThuyetDoThi_DoAn.services.YeuCau5
{
    internal class Euler
    {
        //Hàm kiểm tra đồ thị là đồ thị Euler hay không
        //Bằng cách đếm số bậc lẻ của đồ thị, nếu ko tồn tại đỉnh bậc lẻ => Đồ thị Euler
        public static bool IsEulerian(Graph graph)
        {
            int oddDegreeCount = 0; //Đếm số đỉnh bậc lẻ của đồ thị

            bool hasEulerCycle;

            if (graph.numberOfVertices == 1) //đồ thị không có cạnh được coi là đồ thị Euler vì không có cạnh nào để đi qua.
            {
                return true;
            }

            for (int i = 0; i < graph.numberOfVertices; i++)
            {
                int degree = 0;
                for (int j = 0; j < graph.numberOfVertices; j++)
                {
                    degree += graph.adjacencyMatrix[i, j]; // Tính toán bậc của đỉnh
                }
                if (degree % 2 != 0) //Kiểm tra bậc đỉnh có phải bậc lẻ hay bậc chẵn
                {
                    oddDegreeCount++; // bậc lẻ thì count lên + 1
                }
            }

            if (oddDegreeCount == 0)
            {
                hasEulerCycle = true;
                return hasEulerCycle;
            }

            hasEulerCycle = false;
            return hasEulerCycle;
        }

        //Hàm kiểm tra đồ thị Nửa Euler
        //Bằng cách kiểm tra bậc của đồ thị, nếu đồ thị không có quá 2 đỉnh bậc lẻ
        public static bool IsSemiEulerian(Graph graph)
        {
            int oddDegreeCount = 0;
            bool hasEulerPath;

            for (int i = 0; i < graph.numberOfVertices; i++)
            {
                int degree = 0;
                for (int j = 0; j < graph.numberOfVertices; j++)
                {
                    degree += graph.adjacencyMatrix[i, j];
                }
                if (degree % 2 != 0)
                {
                    oddDegreeCount++;
                }
            }

            if (oddDegreeCount <= 2)
            {
                hasEulerPath = true;
                return hasEulerPath;
            }

            hasEulerPath = false;
            return hasEulerPath;
        }




        public static void FleuryAlgorithm(Graph graph)
        {
            // Step 1: Chọn một đỉnh u tùy ý để bắt đầu
            int startVertex = FindStartVertex(graph);

            // Kiểm tra nếu không tìm thấy đỉnh để bắt đầu
            if (startVertex == -1)
            {
                Console.WriteLine("Khong co duong di Euler.");
                return;
            }

            // Tạo một bản sao của ma trận kề để không thay đổi ma trận gốc
            int[,] tempMatrix = (int[,])graph.adjacencyMatrix.Clone();

            // Step 2-5: Áp dụng thuật toán Fleury
            FleuryRecursive(startVertex, tempMatrix);
        }

        // Bước 1: Chọn một đỉnh u tùy ý để bắt đầu
        private static int FindStartVertex(Graph graph)
        {
            for (int i = 0; i < graph.numberOfVertices; i++)
            {
                int deg = 0;
                for (int j = 0; j < graph.numberOfVertices; j++)
                {
                    if (graph.adjacencyMatrix[i, j] > 0)
                        deg++; // Tăng bậc của đỉnh khi tìm thấy cạnh kết nối
                }
                if (deg % 2 != 0) // Khi bậc của đỉnh là lẻ
                    return i; // Trả về đỉnh có bậc lẻ
            }
            return -1; // Không tìm thấy đỉnh để bắt đầu
        }

        // Kiểm tra xem cạnh uv có phải là cầu không
        private static bool IsBridge(int u, int v, int[,] tempMatrix)
        {
            int deg = 0;
            for (int i = 0; i < tempMatrix.GetLength(0); i++)
            {
                if (tempMatrix[v, i] > 0)
                    deg++;
            }
            return deg == 1; // Trả về true nếu cạnh uv là cầu, ngược lại trả về false
        }

        // Kiểm tra xem đồ thị có còn cạnh nào không
        private static bool HasEdges(int[,] tempMatrix)
        {
            for (int i = 0; i < tempMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < tempMatrix.GetLength(1); j++)
                {
                    if (tempMatrix[i, j] > 0)
                        return true; // Đồ thị còn cạnh
                }
            }
            return false; // Đồ thị không còn cạnh
        }

        // Thuật toán Fleury đệ quy
        private static void FleuryRecursive(int u, int[,] tempMatrix)
        {
            for (int v = 0; v < tempMatrix.GetLength(0); v++)
            {
                if (tempMatrix[u, v] > 0 && (!IsBridge(u, v, tempMatrix) || !HasEdges(RemoveEdge(u, v, tempMatrix))))
                {
                    Console.Write(u + " -- " + v + " "); // In cạnh uv
                    tempMatrix[u, v]--; // Đánh dấu cạnh đã đi qua
                    tempMatrix[v, u]--; // Đánh dấu cạnh ngược lại
                    FleuryRecursive(v, tempMatrix); // Đi tiếp từ đỉnh v
                    break;
                }
            }
        }

        // Loại bỏ cạnh uv từ ma trận kề
        private static int[,] RemoveEdge(int u, int v, int[,] tempMatrix)
        {
            int[,] newMatrix = (int[,])tempMatrix.Clone();
            newMatrix[u, v] = 0;
            newMatrix[v, u] = 0;
            return newMatrix;
        }




        public static void FindEulerCycle(Graph graph)
        {
            // Yêu cầu người dùng nhập đỉnh bắt đầu của chu trình Euler
            Console.Write("Nhap dinh bat dau (tu 0 den " + (graph.numberOfVertices - 1) + "): ");
            int startVertex;
            if (!int.TryParse(Console.ReadLine(), out startVertex) || startVertex < 0 || startVertex >= graph.numberOfVertices)
            {
                Console.WriteLine("Nhap khong hop le.");
                return;
            }

            //Tìm chu trình Euler
            graph.FindFleuryCycle(startVertex);
        }



        public static void FindEulerPath(Graph graph)
        {
            // Yêu cầu người dùng nhập đỉnh bắt đầu của đường đi Euler
            Console.Write("Nhap dinh bat dau (tu 0 den " + (graph.numberOfVertices - 1) + "): ");
            int startVertex;
            if (!int.TryParse(Console.ReadLine(), out startVertex) || startVertex < 0 || startVertex >= graph.numberOfVertices)
            {
                Console.WriteLine("Nhap khong hop le.");
                return;
            }



            // Tìm đường đi Euler
            graph.FindEulerPath(startVertex);
        }
    }
}
