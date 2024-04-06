using LyThuyetDoThi_DoAn.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyThuyetDoThi_DoAn.XacDinhLienThongManh
{
    public class XacDinhLienThongManh
    {
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
