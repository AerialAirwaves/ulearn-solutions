using System;
using NUnit.Framework;

namespace ConsoleApp1
{
    public class Program
    {
        public static bool InvokeWithRetry(Action action, int maxAttempts)
        {
            for (var i = 0; i < maxAttempts; i++)
                try
                {
                    action();
                    return true;
                }
                catch
                {
                    // ignored
                }

            return false;
        }
    }
    
    [TestFixture]
    public class InvokeWithRetryTests
    {
        [Test]
        public void ReturnTrueSimple()
        {
            var invokeCount = 0;
            Action func = () =>
            {
                invokeCount++;
                Console.WriteLine("Hello world");
            };
            Assert.True(Program.InvokeWithRetry(func, 3));
            Assert.AreEqual(1, invokeCount);
        }
        
        [Test]
        public void ReturnFalseGenericException()
        {
            var invokeCount = 0;
            Action func = () =>
            {
                invokeCount++;
                throw new Exception();
            };
            Assert.False(Program.InvokeWithRetry(func, 3));
            Assert.AreEqual(3, invokeCount);
        }
        
        [Test]
        public void ReturnTrueAfterOneFailure()
        {
            var invokeCount = 0;
            var a = -1;
            Action func = () =>
            {
                invokeCount++;
                a++;
                Console.WriteLine(3 / a);
            };
            Assert.True(Program.InvokeWithRetry(func, 3));
            Assert.AreEqual(2, invokeCount);
        }
        
        [Test]
        public void ReturnTrueAfterSuccessOnLastRetry()
        {
            var invokeCount = 0;
            var a = 0;
            Action func = () =>
            {
                invokeCount++;
                a++;
                Console.WriteLine(3 / (a / 999));
            };
            Assert.True(Program.InvokeWithRetry(func, 999));
            Assert.AreEqual(999, invokeCount);
        }
    }
}