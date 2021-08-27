using System;
using System.Globalization;

namespace Stage1
{
    class Program
    {

        // решение задач на семинар модуля "первое знакомство с C#"
        // https://ulearn.me/course/basicprogramming/Zadachi_na_seminar_aef25d82-818b-4a50-a2f0-bba842eeeedc
        // в ходе практики следующего модуля к коду был применён рефакторинг


        // task 1
        // Как поменять местами значения двух переменных?
        // Можно ли это сделать без ещё одной временной переменной. Стоит ли так делать?

        // ответ:
        //  кратко: можно, но лучше не надо;
        //  подробно:
        // можно воспользоваться XOR swap ( https://en.wikipedia.org/wiki/XOR_swap_algorithm )
        // однако он влечёт излишнюю вычислительную нагрузку

        // можно воспользоваться синтаксическим сахаром через кортежи (tuple swap)
        // в этом случае, фактически, компилятор заменит tuple swap заменой с временной переменной (пруф по ссылке ниже)
        // https://sharplab.io/#v2:C4LghgzgtgPgAgJgAQGECwAoA3ppekCWAdsEmADRIBGA3LvvXnACxICyAjABQCUjSODPmFIuFajyQBeUVUpgedIfgC+/fi3YJe/QSPzFSwAKZQADtLJL9eMJdr9hVSyfM0k/NRhVA===

        static void Task1()
        {
            int a = 652;
            int b = 31;

            // tuple swap
            (a, b) = (b, a);

            Console.WriteLine(a.ToString()+" "+b.ToString());

            // xor swap
            a ^= b;
            b ^= a;
            a ^= b;

            Console.WriteLine(a.ToString() + " " + b.ToString());
        }

        static void Task2()
        {
            int input = int.Parse(Console.ReadLine());

            int output = 0;
            while (input != 0)
            {
                output = output * 10 + (input % 10);
                input /= 10;
            }
            Console.WriteLine(output);
        }

        static void Task3()
        {
            byte degrees = (byte)((byte.Parse(Console.ReadLine()) % 12) * 30);
            byte isAngleConvex = (byte)(degrees / 180);
            Console.WriteLine( degrees * (1 - isAngleConvex) + (360 - degrees) * isAngleConvex );
        }

        // task 4
        static void Task4()
        {
            long N = long.Parse(Console.ReadLine()) - 1;
            int x = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());
            Console.WriteLine( N / x + N / y - N / (x*y) );

            // для проверки прогоним перебором
            int t = 0;
            for(int i = 1; i <= N; i++)
            {
                if (((i % x) == 0) || ((i % y) == 0)) t++;
            }
            Console.WriteLine(t);
        }

        static void Task5()
        {
            // int a = int.Parse(Console.ReadLine());
            // int b = int.Parse(Console.ReadLine());
            // return (b / 4) - (a / 4) - (b / 100) + (a / 100) + (b / 400) - (a / 400);
            // но сделаем чуть экономичнее
            int aSubstractB = int.Parse(Console.ReadLine()) - int.Parse(Console.ReadLine());
            Console.WriteLine((aSubstractB / 100) - (aSubstractB / 4) - (aSubstractB / 400));
        }


        // task 6

        static double PlainDistance(double a, double b, double c, double x, double y)
        {
            return Math.Abs(a*x + b*y + c) / Math.Sqrt(a*a + b*b);
        }
        static void Task6()
        {

            Console.Write("Пусть прямая l на плоскости задана двумя точками - N и M.\nНайти расстояние от точки P до прямой l\n\nВведите через пробел плоскостные координаты точки N(x1, y1): ");
            var input = Console.ReadLine().Split();
            var x1 = double.Parse(input[0], CultureInfo.InvariantCulture);
            var y1 = double.Parse(input[1], CultureInfo.InvariantCulture);
            Console.Write("Введите через пробел плоскостные координаты точки M(x2, y2): ");
            input = Console.ReadLine().Split();
            var A = double.Parse(input[0], CultureInfo.InvariantCulture) - x1;
            var B = double.Parse(input[1], CultureInfo.InvariantCulture) - y1;
            double C = 0 - A*x1 - B*y1;

            Console.WriteLine("Коэф. обшего уравнения прямой l: Ax+By+C = 0: \n A = " + A.ToString() + "\n B = " + B.ToString() + "\n C = "+C.ToString());

            Console.Write("Введите через пробел плоскостные координаты точки P(x0, y0): ");
            input = Console.ReadLine().Split();
            var x0 = double.Parse(input[0], CultureInfo.InvariantCulture);
            var y0 = double.Parse(input[1], CultureInfo.InvariantCulture);


            Console.WriteLine("\nОтвет: Distance(P, l) = " + PlainDistance(A, B, C, x0, y0).ToString());
        }

        // task 7
        static void Task7()
        {
            Console.Write("Введите через пробел коэфициенты общего уравнения прямой l на плоскости (A, B, [C]): ");
            var input = Console.ReadLine().Split();
            var A = double.Parse(input[0], CultureInfo.InvariantCulture);
            var B = double.Parse(input[1], CultureInfo.InvariantCulture);

            // поскольку прямая задана общим уравнением, нормальный вектор искать вообще не надо

            Console.WriteLine("\nОтвет:\n N = (" + A.ToString() + "; "+B.ToString()+") - нормальный вектор прямой l");

            // с вектором, коллинеарным l, проблем так же не возникнет, ибо никто не забывал про оператор поворота
            // x_new = cos(angle) * x_old + sin(angle) * y_old
            // y_new = -sin(angle) * x_old + cos(angle) * y_old
            // очевидно, при повороте на pi/2 на плоскости вектор будет иметь координаты (y; -x) - очень частый частный случай

            Console.WriteLine(" L = ("+B.ToString()+"; "+(A * -1).ToString()+") - направляющий прямой l");
        }

        // task 8
        static void Task8()
        {
            Console.Write("Введите через пробел коэфициенты общего уравнения прямой l на плоскости (A, B, C): ");
            var input = Console.ReadLine().Split();
            var A = double.Parse(input[0], CultureInfo.InvariantCulture);
            var B = double.Parse(input[1], CultureInfo.InvariantCulture);
            var C0 = double.Parse(input[2], CultureInfo.InvariantCulture);

            Console.Write("Введите через пробел плоскостные координаты точки A(x, y): ");
            input = Console.ReadLine().Split();
            var x = double.Parse(input[0], CultureInfo.InvariantCulture);
            var y = double.Parse(input[1], CultureInfo.InvariantCulture);

            if (A*x+B*y+C0 == 0) // заранее проверим, чтобы не делать лишних вычислений.
            { 
                Console.WriteLine("Точка A лежит на прямой l. Решать нечего.");
                return;
            }

            // найдём общее уравнение прямой k, проходящей через т.A и ортогональной l следующим образом:
            // 1) из коэф. A, B общего уравнения l получим нормальный вектор прямой l.
            //  этот же вектор является направляющим вектором искомой прямой
            // 2) применив оператор поворота на 90 градусов на плоскости, получим направляющий вектор прямой l.
            //  этот же вектор является нормальным вектором искомой прямой, тогда мы имеем право сделать следующее:
            //  координаты только что найденного вектора подставим как A, B в общее уравнение прямой
            // 3) подставив в это уравнение координаты точки P, найдём C
            // с точки зрения оптимизации: поскольку (A; B) - нормальный вектор для k есть ни что иное, как (B; -A) - нормальный вектор для l
            //  вывод: новые переменные для A и B вводить не будем

            // k: B*x - A*y + C1 = 0. выразим отсюда C1 = A*y - B*x
            double C1 = A * y - B * x;

            // теперь, имея общие уравнения обеих прямых, объединив их в систему, получим квадратную СЛУ второго порядка:
            // l: A*x + B*y + C0 = 0 -в--у--\ A*x + B*y = -C0
            // k: B*x - A*y + C1 = 0 --ж--х-/ B*x - A*y = -C1 - привели систему к правильному для СЛУ виду
            // нас устраивает только тот случай, когда СЛУ имеет единственное решение - т.е. координаты т. A - проекции P на l
            // отсутствие решений либо целое пространство решений нам не нужно.
            // итак, нам нужно единственное решение, СЛУ квадратная и имеет небольшой порядок
            // вывод: в конкретной ситуации пользуемся, не заморачиваясь, формулами Крамера
            // !!! но будь бы порядок СЛУ произвольным, этот метод нанес бы серьезный удар по оптимальности (сложность O(n!) такое себе).
            // но городить универсальную авторешалку СЛУ методом Жордана-Гаусса мне сейчас влом, да и выше я показал,
            // почему формул Крамера в этой задаче достаточно и серьезного удара по оптимальности они не произведут

            double delta = - (A * A + B * B) ;
            if (delta == 0) Console.WriteLine("Решений нет.");
            else
            {
                Console.WriteLine("Ответ: P(" + ((C0 * A + B * C1) / delta).ToString() + "; " + ((A * (-C1) + C0 * B) / delta).ToString() + ") - проекция точки A на прямую l, т.е. искомая т. пересечения");
            }
        }

        // program entry point
        static void Main()
        {
            Console.WriteLine( string.CompareOrdinal("cy", "cz"));
            //Task8();
        }
    }
}
