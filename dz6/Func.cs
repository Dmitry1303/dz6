using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz6
{
    class Func
    {
        public delegate double Function(double x); // чтобы делать делегаты
        public delegate double Functions(double x, double a); // чтобы делать делегаты c параметром а
        public static void Start()
        {
            //создаем лист с названиями функций
            List<string> fncs = new List<string>
            {
                "Sqr",
                "Cube",
                "Sin"
            };

            //аля интерфейс для пользователя
            switch (Helps.Msg_int("Выберите: Функция с параметром или нет:\n1 - без\n2 - с"))
            {
                case 1:
                    for( int i = 0; i<fncs.Count; i++)
                    {
                        Console.WriteLine($"{fncs[i]} - [{i+1}]");
                    }
                    int a = Helps.Msg_int("Введите значение: ");
                    switch (a)
                    {
                        case 1:
                            //квадрат
                            Tabulate(Sqr, Helps.Msg_double("Введите х:"), Helps.Msg_double("Введите максимум:"), Helps.Msg_double("Введите шаг:"));
                            break;
                        case 2:
                            //куб
                            Tabulate(Cube, Helps.Msg_double("Введите х:"), Helps.Msg_double("Введите максимум:"), Helps.Msg_double("Введите шаг:"));
                            break;
                        case 3:
                            //синус
                            Tabulate(Sin, Helps.Msg_double("Введите х:"), Helps.Msg_double("Введите максимум:"), Helps.Msg_double("Введите шаг:"));
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    for (int i = 0; i < fncs.Count; i++)
                    {
                        Console.WriteLine($"{fncs[i]} - [{i + 1}]");
                    }
                    int b = Helps.Msg_int("Введите значение: ");
                    switch (b)
                    {
                        case 1:
                            //квадрат с параметром
                            Tabulates(Sqr, Helps.Msg_double("Введите х:"), Helps.Msg_double("Введите максимум:"), Helps.Msg_double("Введите параметр:"), Helps.Msg_double("Введите шаг:"));
                            break;
                        case 2:
                            //куб с параметром
                            Tabulates(Cube, Helps.Msg_double("Введите х:"), Helps.Msg_double("Введите максимум:"), Helps.Msg_double("Введите параметр:"), Helps.Msg_double("Введите шаг:"));
                            break;
                        case 3:
                            //синус с параметром
                            Tabulates(Sin, Helps.Msg_double("Введите х:"), Helps.Msg_double("Введите максимум:"), Helps.Msg_double("Введите параметр:"), Helps.Msg_double("Введите шаг:"));
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;

            }
            //Function funct = new Function(Sqr);
            //double x = 2;
            //Helps.Printm($"Значение функции f({x})={funct.Invoke(x)}");

            //Tabulate(new Function(Cube), 2, 15, 2);
            //Console.WriteLine();
            //Tabulates(new Functions(Cube), 2, 15, 7, 2);

            //Tabulate(Sqr, 2, 15, 2); // делегат создается автоматически
            //Tabulates(new Functions(Sqr), 2, 15, 7, 2); // Делегат с параметром
            Helps.Printm("Далее найден минимум функции X^2 на отрезке от 2 до 15, с шагом 2:");
            MinF(new Function(Sqr), 2, 15, 2); // поиск минимума и выведение в таблицу функции
            SaveFunc("data1.bin", Sqr,2, 15, 2); // запись массива в файл
            double[] massi = LoadMin("data1.bin");  // чтение массива и минимума из файла
            Helps.Pause();
        }
        public static Function Summ(Function f1, Function f2)
        {
            //сумма двух функций, в виде лямбда выражения, как я понимаю
            return x => f1(x) + f2(x);
            //return delegate (double x) { return f1(x) + f2(x); };
        }
        public static Function Multiply(Function f1, Function f2)
        {
            return x => f1(x) * f2(x);
        }
        public static double Sqr(double x)
        {
            //квадрат
            return x * x;
        }
        public static double Sqr(double x, double a = 1)
        {
            //квадрат с параметром
            return a * x * x;
        }
        public static double Cube(double x)
        {
            //куб
            return x * x * x;
        }
        public static double Cube(double x, double a = 1)
        {
            //куб с параметром(по дефалту 1)
            return a * x * x * x;
        }
        public static double Sin(double x)
        {
            //синус
            return Math.Sin(x);
        }
        public static double Sin(double x, double a = 1)
        {
            //синус с параметром
            return a * Math.Sin(x);
        }
        public static void Tabulate(Function f, double a, double b, double dx)
        {
            // a - первое значение переменной
            // b- максимум
            // dx - шаг
            // функция вывода функции на консоль, в виде таблицы с шагом dx
            if (f is null) throw new ArgumentNullException(nameof(f));
            if (b - a < dx) throw new InvalidOperationException("Интервал расчёта функции меньше шага вычисления ее аргумента");
            Console.WriteLine($"Изначальные аргументы:\n{a} - X\n{dx}- Шаг\n{b}-Макс.Х");
            Console.WriteLine("------X----------Y-----");
            double x = a;
            while (x <= b)
            {
                var y = f(x);
                //вывод на консоль
                Console.WriteLine($"| {x,8:f2} | {y,8:f2} |");
                x += dx;
                // прибавляем шаг и пересчитываем значение функции

            }
            Console.WriteLine("-----------------------");
        }
        public static void Tabulates(Functions f, double a, double b, double c, double dx)
        {
            // a - первое значение переменной
            // b- максимум
            // dx - шаг
            // функция вывода функции на консоль, в виде таблицы с шагом dx
            if (f is null) throw new ArgumentNullException(nameof(f));
            if (b - a < dx) throw new InvalidOperationException("Интервал расчёта функции меньше шага вычисления ее аргумента");
            Console.WriteLine($"Изначальные аргументы:\n{a} - X\n{c}- Параметр\n{dx}- Шаг\n{b}-Макс.Х");
            Console.WriteLine("------X----------Y-----");
            double x = a;
            while (x <= b)
            {
                var y = f(x, c);
                //вывод на консоль
                Console.WriteLine($"| {x,8:f2} | {y,8:f2} |");
                x += dx;
                // прибавляем шаг и пересчитываем значение функции
            }
            Console.WriteLine("-----------------------");
        }

        //public static double F(double x)
        //{
        //    return x * x - 50 * x + 10;
        //}


        public static void MinF(Function f, double a, double b, double dx)
        {
            // a - первое значение переменной
            // b- максимум
            // dx - шаг
            // функция вывода функции на консоль, в виде таблицы с шагом dx
            if (f is null) throw new ArgumentNullException(nameof(f));
            if (b - a < dx) throw new InvalidOperationException("Интервал расчёта функции меньше шага вычисления ее аргумента");
            Console.WriteLine($"Изначальные аргументы:\n{a} - X\n{dx}- Шаг\n{b}-Макс.Х");
            Console.WriteLine("------X----------Y-----");
            double x = a;
            double minx = double.MaxValue;
            double miny = double.MaxValue;
            while (x <= b)
            {
                var y = f(x);
                if (y < miny)
                {
                    miny = y;
                    minx = x;
                }

                //вывод на консоль
                Console.WriteLine($"| {x,8:f2} | {y,8:f2} |");
                x += dx;
                // прибавляем шаг и пересчитываем значение функции

            }
            Console.WriteLine($"Минимум функции: {miny}, при Х: {minx}\n-----------------------");
        }
        public static void SaveFunc(string FilePath, Function f,double a, double b, double dx)
        {
            // сохраняем значение функции в бинарный файл
            using (FileStream file_stream = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
            using (BinaryWriter binary_writer = new BinaryWriter(file_stream))
            {
                double x = a;
                while (x <= b)
                {
                    binary_writer.Write(f(x));
                    x += dx;// x=x+h;
                }
            }
            Console.WriteLine("Сохранение выполнено успешно!");
        }
        public static double[] LoadMin(string FilePath)
        {
            // считываем даблы из файлов
            double min = double.MaxValue;
            double[] mass = new double[0];
            using (var file_stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(file_stream))
            {
                // вынужденная мера, т.к. что-то всегда должно вернуться,
                // поэтому изначально объявляем пустой массив, потом делаем ресайз
                var b = file_stream.Length / sizeof(double);
                Array.Resize(ref mass, (int)file_stream.Length/sizeof(double));
                for (int i = 0; i < file_stream.Length / sizeof(double); i++)
                {
                    double dble = reader.ReadDouble();
                    mass[i] = dble;
                    if (dble < min) min = dble;
                }
            }
            Console.WriteLine($"Массив загружен из файла \"{FilePath}\" успешно!");
            Console.WriteLine($"Минимум в массиве: {min}");
            return mass;
        }

    }
}
