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

            int[,] tempAdjacencyMatrix = graph.TransformWeights(graph.adjacencyMatrix);

            if (graph.numberOfVertices == 1) //đồ thị không có cạnh được coi là đồ thị Euler vì không có cạnh nào để đi qua.
            {
                return true;
            }

            for (int i = 0; i < graph.numberOfVertices; i++)
            {
                int degree = 0;

                for (int j = 0; j < graph.numberOfVertices; j++)
                {
                    degree += tempAdjacencyMatrix[i, j]; // Tính toán bậc của đỉnh
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
            int[,] tempAdjacencyMatrix = graph.TransformWeights(graph.adjacencyMatrix);

            for (int i = 0; i < graph.numberOfVertices; i++)
            {
                int degree = 0;
                for (int j = 0; j < graph.numberOfVertices; j++)
                {
                    degree += tempAdjacencyMatrix[i, j];
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
            Console.WriteLine();
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
            Console.WriteLine();
        }
    }
}
