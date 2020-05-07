using System;
using System.IO;
using System.Text;

namespace Files
{
    class Program
    {
        static void Main(string[] args)
        {
            string text;
            text = ReadAllFile("disposable_task_file.txt");
            try
            {
                using (StreamWriter sw = new StreamWriter("disposable_task_file.txt"))
                {
                    foreach (var num in text.Split('\n'))
                    {
                        sw.WriteLine(Math.Pow(int.Parse(num), 2));
                    }
                }
            }
            catch (FormatException ex)
            {
                PrintEx(ex.Message + "\nСкорее всего файл уже записался при прошлом запуске(кстати он теперь стёрся)");
            }
            catch (Exception ex) {
                PrintEx(ex.Message);
            }

            string path = @"C:\temp\";
            Directory.CreateDirectory(path + "K1");
            WriteFile(path + @"K1\t1.txt", "Иванов Иван Иванович, 2000 года рождения, место жительства г. Рязань", false);
            WriteFile(path + @"K1\t2.txt", "Петров Сергей Федорович, 2002 года рождения, место жительства г. Бежицк", false);
            Directory.CreateDirectory(path + @"K2");
            text = ReadAllFile(path + @"K1\t1.txt");
            WriteFile(path + @"K2\t3.txt", text, false);
            text = ReadAllFile(path + @"K1\t2.txt");
            WriteFile(path + @"K2\t3.txt", text, true);
            AllInfoAboutFile(path + @"K1\t1.txt");
            AllInfoAboutFile(path + @"K1\t2.txt");
            AllInfoAboutFile(path + @"K2\t3.txt");
            try
            {
                if (!File.Exists(path + @"K2\t2.txt") && File.Exists(path + @"K1\t2.txt"))
                    File.Move(path + @"K1\t2.txt", path + @"K2\t2.txt");
                if (!File.Exists(path + @"K2\t1.txt") && File.Exists(path + @"K1\t1.txt"))
                    File.Copy(path + @"K1\t1.txt", path + @"K2\t1.txt");
                Directory.Move(path + "K2", path + "All");
                Directory.Delete(path + "K1", true);
            }
            catch (Exception e)
            {
                PrintEx(e.Message + "Некоторые операции возможно не завершились, исправьте ошибку и повторите попытку");
            }
            foreach (var file in Directory.EnumerateFiles(path + "All"))
            {
                AllInfoAboutFile(file);
            }


            Console.ReadKey();

        }
        public static void WriteFile(string filepath, string message, bool append)
        {
            using (StreamWriter sw = new StreamWriter(filepath, append, Encoding.UTF8))
            {
                sw.WriteLine(message);
            }
        }
        public static string ReadAllFile(string path)
        {
            using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
            {
                return sr.ReadToEnd();
            }
        }
        public static void AllInfoAboutFile(string path)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                FileInfo fileInfo = new FileInfo(path);
                Console.WriteLine("/ " + fileInfo.FullName);
                Console.WriteLine("| Атрибуты: " + fileInfo.Attributes);
                Console.WriteLine("| Размер: " + fileInfo.Length + " байт");
                Console.WriteLine(@"\ Создано: " + fileInfo.CreationTime);
                Console.WriteLine("----------");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception e)
            {
                PrintEx(e.Message);
            }

        }
        public static void PrintEx(string exeption)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(exeption);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
