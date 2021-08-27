using System;

namespace stage2
{
    class Program
    {
        // решения задач на семинар, Ulear, Основы программирования, Ошибки
        // https://ulearn.me/course/basicprogramming/Zadachi_na_seminar_d6cc4e30-a72f-4daa-ac1f-aa9e69deb91f

        static int Task1(int upperbound = 100) // Expr10
        {
            int totalNumbers = upperbound - 1;
            // на промежутке 1 .. k натуральных чисел на натуральное n > 1 делится ровно k / n (округлённое вниз) чисел.
            // далее применяем известную ещё со школы формулу нахождения кол-ва элементов в объединении двух множ.
            return totalNumbers / 3 + totalNumbers / 5 - totalNumbers / 15;
        }

        static double Task2(byte hours, byte mins) // Expr11, возвращает коэф. угла ( единица = 360 градусов )
        {
            // вычислим коэф полуоборота минутной стрелки (потом можно домножить на pi и получить угол в радианах)
            double minsArrowCoef = mins / 30.0;
            // найдём коэф угла м/у стрелками (1 эквивалентна углу pi в радианах), может вернуться коэф выпуклого угла!
            double delta = Math.Abs( ((hours % 12) - 5.5 * minsArrowCoef) / 6);
            
            // k = 1, если угол выпуклый, и 0 в противном случае
            byte k = (byte)Math.Floor(delta / 1.0);
            // if через сложение и умножение. работает быстрее, т.к. эти операции быстрее логических и ветвлений
            return (1 - k) * delta + k * (2 - delta); 
        }

        static void Main(string[] args)
        {
            //Console.Write("[Task1] Type the upper bound: ");
            //Console.WriteLine("Out: "+Task1(int.Parse(Console.ReadLine())).ToString());
            //Console.Write("[Task2] Type digital clock hours and mins, separated by space: ");
            //var inp = Console.ReadLine().Split();
            //Console.WriteLine("Out: "+(Task2(byte.Parse(inp[0]), byte.Parse(inp[1]))*180).ToString()+" degrees");
            Console.WriteLine(Math.Acos(Double.NaN));
        }
    }
}
