using System;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // LQ1:
            // var a = new[] {1, 2, 3, 5, 6, 9};
            // var k = 3;
            // var result = a.Skip(k % a.Length)
            //     .Concat(a.Take(k % a.Length))
            //     .ToArray();
            // foreach (var e in result)
            // {
            //     Console.Write(e); 
            //     Console.Write(" ");
            // }
            
            // LQ3:
            var lines = new[] { "aba", "aaa", "aabb" };
            var result = lines.Select(x => x.ToLowerInvariant())
                .Where(line 
                    => line.GroupBy(c => c).All(c => c.Count() <= 2))
                .ToArray();
            foreach (var line in result)
                Console.WriteLine(line);

        }
    }
}