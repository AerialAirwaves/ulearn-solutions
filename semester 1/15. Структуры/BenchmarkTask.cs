using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using System.Text;
using System.Runtime.CompilerServices;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
    {
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            task.Run(); // "прогревочный" вызов, чтобы заставить JIT скомпилировать код задачи
            var stopwatch = new Stopwatch();

            GC.Collect();                   // Эти две строчки нужны, чтобы уменьшить вероятность того,
            GC.WaitForPendingFinalizers();  // что Garbadge Collector вызовется в середине измерений
                                            // и как-то повлияет на них.
            stopwatch.Start();
            for (var i = 0; i < repetitionCount; i++)
                task.Run();
            stopwatch.Stop();

            return stopwatch.Elapsed.TotalMilliseconds / repetitionCount;
        }
    }

    class StringConstructorBenchmarkTask : ITask
    {
        public int StringLength {get;}

        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public void Run()
        {
            new string('a', StringLength);
        }

        public StringConstructorBenchmarkTask(int stringLength)
        {
            this.StringLength = stringLength;
        }
    }

    class StringBuilderBenchmarkTask : ITask
    {
        public int StringLength {get;}

        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public void Run()
        {
            var stringbuilder = new StringBuilder();
            var a = "a";

            for (var i = 0; i < StringLength; i++)
                stringbuilder.Append(a);

            stringbuilder.ToString();
        }

        public StringBuilderBenchmarkTask(int stringLength)
        {
            this.StringLength = stringLength;
        }
    }

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            var repetitionsCount = 20000;
            var stringLength = 10000;
            var benchmark = new Benchmark();
            var stringConstructorBenchmarkTask = new StringConstructorBenchmarkTask(stringLength);
            var stringBuilderBenchmarkTask = new StringBuilderBenchmarkTask(stringLength);
            
            var stringConstructorAverageTime = benchmark.MeasureDurationInMs(
                stringConstructorBenchmarkTask, repetitionsCount);
            
            var stringBuilderAverageTime = benchmark.MeasureDurationInMs(
                stringBuilderBenchmarkTask, repetitionsCount);

            Assert.Less(stringConstructorAverageTime, stringBuilderAverageTime);
        }
    }
}
