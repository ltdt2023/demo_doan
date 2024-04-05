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
        public static Graph DocDoThiTuFile(string tenFile)
        {
            StreamReader sr = new StreamReader(tenFile);
            int soDinh = int.Parse(sr.ReadLine()); // Đọc số đỉnh từ dòng đầu tiên
            Graph doThi = new Graph(soDinh);

            for (int i = 0; i < soDinh; i++)
            {
                string[] tokens = sr.ReadLine().Split(' ');
                int soDinhKe = int.Parse(tokens[0]); // Đọc số lượng đỉnh kề của đỉnh i
                for (int j = 1; j < tokens.Length; j += 2)
                {
                    int dinhKe = int.Parse(tokens[j]); // Đọc đỉnh kề
                    int trongSo = int.Parse(tokens[j + 1]); // Đọc trọng số
                    doThi.AddEdge(i, dinhKe, trongSo); // Thêm cạnh vào ma trận kề của đồ thị
                }
            }

            sr.Close();
            return doThi;
        }

    }
}
