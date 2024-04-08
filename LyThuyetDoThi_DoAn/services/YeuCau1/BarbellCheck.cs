using LyThuyetDoThi_DoAn.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyThuyetDoThi_DoAn.services.YeuCau1
{
    internal class BarbellCheck
    {
        //Hàm kiểm tra các cặp có đối xứng nhau qua đường chéo phụ hay không
        public static bool KiemTraDoiXungQuaDuongCheoPhu(int[,] matrix)
        {
            int n = matrix.GetLength(0); // Lấy số hàng của ma trận
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    // Kiểm tra xem cặp số [i, j] và [n - j - 1, n - i - 1] có giống nhau không
                    if (matrix[i, j] != matrix[n - j - 1, n - i - 1])
                    {
                        return false; // Nếu không giống nhau, không đối xứng qua đường chéo phụ
                    }
                }
            }
            return true; // Nếu tất cả các cặp số đối xứng qua đường chéo phụ
        }

        //Hàm kiểm tra barbell
        public static bool KiemTraBarbell(Graph doThi)
        {
            int soDinh = doThi.numberOfVertices;

            if (soDinh % 2 != 0) // Số đỉnh của Barbell phải là chẵn
            {
                return false;
            }

            if (!KiemTraDoiXungQuaDuongCheoPhu(doThi.adjacencyMatrix))
            {
                return false;
            }

            return true;
        }
    }
}
