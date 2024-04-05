using System;
using LyThuyetDoThi_DoAn.Entity;
using LyThuyetDoThi_DoAn.ReadFile;


namespace MyApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            try
            {
                Graph doThi = ReadInputFile.DocDoThiTuFile("C:\\Learning\\LyThuyetDoThi\\test.txt");
                doThi.InDanhSachKe();

                doThi.InMaTranKe();

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Error: Khong tim thay file.");
                Console.WriteLine(e.Message);
            }
        }
    }

}