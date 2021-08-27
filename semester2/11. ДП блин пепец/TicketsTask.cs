using System.Collections.Generic;
using System.Numerics;

namespace Tickets
{
    public class TicketsTask
    {
        public static BigInteger Solve(int halfLength, int totalSum)
        {
            if (totalSum % 2 == 1)
                return 0;

            var table = new Dictionary<(int length, BigInteger sum), BigInteger>();
            var callStack = new Stack<(int length, BigInteger sum)>();
            var initialStep = (halfLength, totalSum / 2);
            callStack.Push(initialStep);

            while (callStack.Count > 0)
            {
                var step = callStack.Peek();
                var result = BigInteger.Zero;
                for (var i = 0; i < 10; i++)
                {
                    var substep = (length: step.length - 1, sum: step.sum - i);
                    var substepValue = substep.length == 0 && substep.sum == 0 ? BigInteger.One : BigInteger.Zero;
                    if (substep.length == 0 || substep.sum < 0 || table.TryGetValue(substep, out substepValue))
                        result += substepValue;
                    else
                        callStack.Push(substep);
                }

                if (step != callStack.Peek())
                    continue;
                
                callStack.Pop();
                table.Add(step, result);
            }

            return table[initialStep] * table[initialStep];
        }
    }
}
