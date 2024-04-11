using LyThuyetDoThi_DoAn.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyThuyetDoThi_DoAn.ReadFile
{
    public class ReadInputFile
    {
        public static dynamic DocDoThiTuFile(string tenFile)
        {
            try
            {
                StreamReader sr = new StreamReader(tenFile);
                int soDinh = int.Parse(sr.ReadLine()); // Đọc số đỉnh từ dòng đầu tiên
                Graph doThi = new Graph(soDinh);

                for (int i = 0; i < soDinh; i++)
                {
                    string[] tokens = sr.ReadLine().Split(' ');
                    int[] result = new int[tokens.Length];
                    for (int k = 0; k < tokens.Length; k++)
                    {
                        result[k] = int.Parse(tokens[k]);
                    }

                    int soDinhKe = result[0]; // Đọc số lượng đỉnh kề của đỉnh i
                    HashSet<int> visited = new HashSet<int>();
                    for (int j = 1; j < result.Length; j += 2)
                    {
                        if (visited.Contains(result[j])) // Kiem tra do thi vo huong co canh boi
                        {
                            throw new Exception("Day la do thi vo huong co canh boi");
                        } // Kiem tra do thi vo huong co canh khuyen
                        else
                        {
                            visited.Add(result[j]);
                            int dinhKe = result[j]; // Đọc đỉnh kề
                            int trongSo = result[j +1]; // Đọc trọng số
                            doThi.AddEdge(i, dinhKe, trongSo); // Thêm cạnh vào ma trận kề của đồ thị
                        }
                    }
                }
                sr.Close();
                return doThi;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

    }
}
