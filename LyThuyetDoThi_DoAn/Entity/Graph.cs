using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyThuyetDoThi_DoAn.Entity
{
    public class Graph
    {
        private int[,] adjacencyMatrix; // Mảng hai chiều để lưu trữ ma trận kề
        private int numberOfVertices; // Số đỉnh của đồ thị
        
        //Constructor
        public Graph(int numberOfVertices)
        {
            this.numberOfVertices = numberOfVertices;
            adjacencyMatrix = new int[numberOfVertices, numberOfVertices];
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
        public void InMaTranKe()
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
    }
}
