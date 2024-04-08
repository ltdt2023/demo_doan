using LyThuyetDoThi_DoAn.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyThuyetDoThi_DoAn.services.YeuCau1
{
    internal class WindMillCheck
    {
        public static bool KiemTraCoixayGio(Graph doThi)
        {
            int k = 3; // k = 3 là cố định, đề cho

            int soDinh = doThi.numberOfVertices;

            //Console.WriteLine("So dinh la: "+ soDinh);

            int n = (soDinh - 1) / (k - 1); // Số đỉnh trong đồ thị xay gió bằng n*(k-1) + 1, và k =3 

            //Console.WriteLine("n la: "+ n);

            if (n < 2) // Đồ thị cối xay gió có điều kiện n >= 2
            {
                return false;
            }

            // 2*n +1 = số đỉnh => đỉnh chắc chắn phải là số lẻ

            if (soDinh % 2 == 0) // nếu số đỉnh là số chẵn => sai
            {
                return false;
            }

            int Edge = doThi.DemSoCanhCuaDoThiVoHuong(doThi.adjacencyMatrix);

            //Console.WriteLine("So canh la: " + Edge);
            if (Edge != 3 * n) // cạnh = n*k*(k-1)/2
            {
                return false;
            }

            return true;
        }
    }
}
