﻿using System;
using LyThuyetDoThi_DoAn.Entity;
using LyThuyetDoThi_DoAn.ReadFile;
using LyThuyetDoThi_DoAn.Validation;
using LyThuyetDoThi_DoAn.FindStrongPart;
using LyThuyetDoThi_DoAn.services.YeuCau1;
using LyThuyetDoThi_DoAn.services.YeuCau5;


namespace MyApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
                string filePath = Path.Combine(projectRoot, "resource", "text.txt");
                Graph doThi = ReadInputFile.DocDoThiTuFile(filePath);
                Graph doThi_2 = ReadInputFile.DocDoThiTuFile(filePath);
                if (doThi != null)
                {
                    Console.WriteLine("In danh sach ke: ");
                    doThi.InDanhSachKe();
                    Console.WriteLine("In ma tran ke: ");
                    doThi.InMaTranKe(doThi.TransformWeights(doThi.adjacencyMatrix));
                    Console.WriteLine("In ma tran ke duoi dang trong so: ");
                    doThi.InMaTranKe(doThi.adjacencyMatrix);
                }
                Console.WriteLine();
                requestNumberOne(doThi);
                Console.WriteLine();
                requestNumberTwo(doThi);
                Console.WriteLine();
                requestNumberFive(doThi);

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Error: Khong tim thay file.");
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(); // This waits for a key press before exiting
        }

        public static void requestNumberTwo(Graph graph)
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("\nYeu cau so 2: Xac dinh thanh phan lien thong manh");

            
                Validation validation = new Validation();
                FindStrongPart findStrongPart = new FindStrongPart(graph);
                if (FindStrongPart.isValidated(graph))
                {
                    validation.display(graph);
                    findStrongPart.display();
                }
            
            


        }

        public static void requestNumberOne(Graph graph)
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("\nYeu cau 1: Nhan dien mot so dang do thi dac biet");

            if (graph.IsUnDirected(graph.adjacencyMatrix))
            {
                Console.WriteLine("Do thi la do thi vo huong khong canh boi va khong canh khuyen => Thoa dieu kien cua yeu cau 1!");

                //Kiem tra do thi coi xay gio ?
                bool laCoiXayGio = WindMillCheck.KiemTraCoixayGio(graph);
                if (laCoiXayGio)
                {
                    int k = 3;
                    int soDinh = graph.numberOfVertices;
                    int n = (soDinh - 1) / (k - 1);

                    Console.WriteLine($"Do thi coi xay gio: Wd(3,{n})");
                }
                else
                {
                    Console.WriteLine("Do thi coi xay gio: Khong");
                }


                //Kiem tra do thi Barbell
                bool barbellCheck = BarbellCheck.KiemTraBarbell(graph);
                if (barbellCheck)
                {
                    int n = graph.numberOfVertices / 2; // n sẽ bằng số đỉnh đồ thị chia 2

                    Console.WriteLine($"Do thi Barbell: Bac {n}");
                }
                else
                {
                    Console.WriteLine("Do thi Barbell: Khong");
                }

                //Kiem tra do thi K- Phân
                PartiteCheck.KiemTraDoThiK_Phan(graph);
            }
            else
            {
                Console.WriteLine("Do thi khong thoa man dieu kien de xet yeu cau 1.");
                Console.WriteLine("=> Do thi la do thi co huong.");

                if (graph.ChuaCanhBoiTrongCoHuong(graph.adjacencyMatrix))
                {
                    Console.WriteLine("=> Do thi chua canh boi.");
                }

            }
        }

        public static void requestNumberFive(Graph graph)
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("\nYeu cau 5: Tim chu trinh hoac duong di Euler");
            if (graph.IsUnDirected(graph.adjacencyMatrix) && graph.IsConnected(graph)) // Kiểm tra đồ thị có vô hướng và liên thông
            {
                if (Euler.IsEulerian(graph))
                {
                    Console.WriteLine("\nDo thi Euler.");
                    Console.WriteLine("--------------");


                    Euler.FindEulerCycle(graph);

                }
                else if (Euler.IsSemiEulerian(graph))
                {
                    Console.WriteLine("\nDo thi nua Euler.");
                    Console.WriteLine("--------------");
                    Euler.FindEulerPath(graph);
                }
                else
                {
                    Console.WriteLine("Do thi khong Euler.");
                }

            }

            else
            {
                Console.WriteLine("Do thi khong thoa man dieu kien de xet yeu cau 5.");
                if (!graph.IsUnDirected(graph.adjacencyMatrix))
                {
                    Console.WriteLine("=> Do thi la do thi co huong.");
                }
                if (!graph.IsConnected(graph))
                {
                    Console.WriteLine("=> Do thi khong lien thong.");
                }
            }
        }
    }

}