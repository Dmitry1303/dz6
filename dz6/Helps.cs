using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz6
{
    class Helps
    {
        /// <summary>
        /// Дополнительные плюшки для продуктивности
        /// </summary>
        public static void Print(string ms, int x, int y)
        {
            // печатает в нужной области экрана
            Console.SetCursorPosition(x, y);
            Console.WriteLine(ms);
        }
        public static void Print(string ms)
        {
            // просто печатает
            Console.WriteLine(ms);
        }
        public static void Printm(string ms)
        {
            // просто печатает
            Console.Write(ms);
        }
        public static void Pause()
        {
            // пауза, чтобы консоль не закрылась
            Console.ReadKey();
        }
        public static string Msg_string(string ms)
        {
            //выводит сообщение пользователя и собирает его в string. Проверки не нужны
            Console.WriteLine(ms);
            string result = Console.ReadLine();
            return result;
        }
        public static double Msg_double(string msg)
        {

            //выводит сообщение пользователя и собирает его в double
            Console.Write(msg + " ");
            var input = Console.ReadLine();
            double result;
            while (!double.TryParse(input, out result))
            {
                Console.WriteLine("Ошибка ввода числа.");
                Console.Write(msg + " ");
                input = Console.ReadLine();
            }
            return result;
        }
        public static int Msg_int(string msg)
        {
            //выводит сообщение пользователя и собирает его в int
            Console.Write(msg + " ");
            var input = Console.ReadLine();
            int result;
            while (!int.TryParse(input, out result))
            {
                Console.WriteLine("Ошибка ввода числа.");
                Console.Write(msg + " ");
                input = Console.ReadLine();
            }
            return result;
        }

        /// <summary>
        /// метод, который загружает в двумерный массив строки из файла с разделителем ;
        /// </summary>
        /// <param name="FilePath">имя файла</param>
        /// <returns></returns>
        public static string[,] LoadFromFile(string FilePath)
        {

            int length = 0;
            var list_acc = new List<string>();
            var list_pass = new List<string>();
            using (var file_stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(file_stream))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var value1 = line.Substring(0, line.IndexOf(';'));
                    var value2 = line.Substring(line.IndexOf(';') + 1);
                    list_acc.Add(value1);
                    list_pass.Add(value2);
                    length = list_acc.Count;
                }
            }
            string[,] mass = new string[2, length];
            for (int i = 0; i < length; i++)
            {
                mass[0, i] = list_acc[i];
                mass[1, i] = list_pass[i];
            }

            return mass;
        }

        public static string[,] LoadGameFromFile(string FilePath)
        {
            // подгрузка игры
            int length = 0;
            var list_zag = new List<string>();
            var list_otv = new List<string>();
            using (var file_stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(file_stream))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var value1 = line.Substring(0, line.IndexOf(';'));
                    var value2 = line.Substring(line.IndexOf(';') + 1);
                    list_zag.Add(value1);
                    list_otv.Add(value2);
                    length = list_zag.Count;
                }
            }
            string[,] mass = new string[2, length];
            for (int i = 0; i < length; i++)
            {
                mass[0, i] = list_zag[i];
                mass[1, i] = list_otv[i];
            }

            return mass;
        }

        public static string LoadFile(string FilePath)
        {
            // просто берем весь текст в строку
            string str = "";
            using (var file_stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(file_stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    str = str + line;
                }
            }
            return str;
        }
    }
}
