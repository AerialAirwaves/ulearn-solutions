using System;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot
{
    public partial class Bot
    {
        public Rocket GetNextMove(Rocket rocket)
        {
            var iterationsCountPerThread = iterationsCount / threadsCount;
            var tasks = Task.WhenAll(Enumerable.Range(0, threadsCount)
                .Select(x => Task.Run(() => SearchBestMove(rocket, 
                    new Random(random.Next()), iterationsCountPerThread))));
            
            tasks.Wait();
            var bestTurn = tasks.Result
                .OrderBy(x => x.Item2)
                .Last().Item1;
            
            return rocket.Move(bestTurn, level);
        }
    }
}