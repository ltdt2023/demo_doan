using System;
using LyThuyetDoThi_DoAn.Entity;
using LyThuyetDoThi_DoAn.ReadFile;
using LyThuyetDoThi_DoAn.XacDinhLienThongManh;


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
                if (doThi != null)
                {
                    doThi.InDanhSachKe();
                    doThi.InMaTranKe();
                }

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Error: Khong tim thay file.");
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(); // This waits for a key press before exiting
        }
    }

}