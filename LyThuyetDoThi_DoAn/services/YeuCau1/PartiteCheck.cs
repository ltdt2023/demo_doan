using LyThuyetDoThi_DoAn.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyThuyetDoThi_DoAn.services.YeuCau1
{
    internal class PartiteCheck
    {
        public static void KiemTraDoThiK_Phan(Graph doThi)
        {
            int n = doThi.adjacencyMatrix.GetLength(0);

            List<HashSet<int>> partitions = new List<HashSet<int>>(); //Danh sách tập hơp con của đỉnh, mỗi tập hợp con sẽ chứa các đỉnh thuộc cùng 1 phân hoạch.
            HashSet<int> visited = new HashSet<int>(); // Tập hợp các đỉnh đã được duyệt qua

            int k = 0; // Số lượng k tập hợp trong phân hoạch

            for (int i = 0; i < n; i++)
            {
                if (!visited.Contains(i))
                {
                    HashSet<int> partition = new HashSet<int>();
                    visited.Add(i);
                    partition.Add(i);
                    bool foundPartition = false; // Dùng để đánh dấu có tìm thấy tập hợp cho đỉnh i hay không

                    for (int j = i + 1; j < n; j++)
                    {
                        bool check = doThi.IsAdjacentToAny(j, partition, doThi.adjacencyMatrix);
                        if (!visited.Contains(j) && !check)
                        {
                            partition.Add(j);
                            visited.Add(j);
                            foundPartition = true;
                        }
                    }

                    if (foundPartition || partition.Count == 1) // Kiểm tra cả trường hợp đỉnh đơn lẻ
                    {
                        partitions.Add(partition);
                        k++; // Tăng số lượng tập hợp lên một
                    }
                }
            }

            // In kết quả
            Console.Write($"Do thi k-phan: {k}-partite ");
            for (int i = 0; i < partitions.Count; i++)
            {
                Console.Write("{" + string.Join(", ", partitions[i]) + "} ");
            }
        }
    }
}
