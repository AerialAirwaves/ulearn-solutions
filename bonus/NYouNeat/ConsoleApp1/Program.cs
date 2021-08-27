using System;
using System.Linq;
using System.Reflection;

namespace ConsoleApp1
{
    public class TestFixtureAttribute : Attribute
    {
        
    }

    public class TestAttribute : Attribute
    {
        
    }

    [TestFixture]
    class SampleTests
    {
        [Test]
        void PassingTest()
        {
            Console.WriteLine("Hello world!");
        }

        [Test]
        void FailingTest()
        {
            throw new NotImplementedException();
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var testClasses = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.GetCustomAttribute(typeof(TestFixtureAttribute)) != null)
                .Select(x => Activator.CreateInstance(x));

            foreach (var classItem in testClasses)
            foreach (var item in classItem.GetType().GetMethods()
                .Where(x => x.GetCustomAttribute<TestAttribute>() != null))
            {
                try
                {
                    item.Invoke(classItem, new object[] {});
                    Console.WriteLine($"Test of {item.Name} passed");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Test of {item.Name} failed: {e}");
                }
            }
                
        }
    }
}