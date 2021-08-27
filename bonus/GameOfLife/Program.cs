using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Program
    {
        public static void Paint(bool[,] field)
        {
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < field.GetLength(1); y++)
            {
                for (int x = 0; x < field.GetLength(0); x++)
                {
                    var symbol = field[x, y] ? '#' : ' ';
                    Console.Write(symbol);
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            var field = new bool[20, 20];
            field[5, 5] = true;
            field[6, 5] = true;
            field[7, 5] = true;
            while (true)
            {
                Paint(field);
                Thread.Sleep(500);
                field = Game.NextStep(field);
            }
        }
    }
}
