using System.Collections.Generic;

namespace StructBenchmarking
{
    public interface ITaskFactory
    {
        ITask CreateTaskForStructure(int size);
        ITask CreateTaskForClass(int size);
    }

    public class ArrayCreationTaskFactory : ITaskFactory
    {
        public ITask CreateTaskForStructure(int size) => new StructArrayCreationTask(size);
        public ITask CreateTaskForClass(int size) => new ClassArrayCreationTask(size);
    }

    public class MethodCallTaskFactory : ITaskFactory
    {
        public ITask CreateTaskForStructure(int size) => new MethodCallWithStructArgumentTask(size);
        public ITask CreateTaskForClass(int size) => new MethodCallWithClassArgumentTask(size);
    }

    public class Experiments
    {
        private static ChartData BuildChart(ITaskFactory taskFactory,
            IBenchmark benchmark, int repetitionsCount, string title)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            foreach (var size in Constants.FieldCounts)
            {
                classesTimes.Add(new ExperimentResult(size,
                    benchmark.MeasureDurationInMs(taskFactory.CreateTaskForClass(size), repetitionsCount)));
                structuresTimes.Add(new ExperimentResult(size,
                    benchmark.MeasureDurationInMs(taskFactory.CreateTaskForStructure (size), repetitionsCount)));
            }

            return new ChartData
            {
                Title = title,
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        public static ChartData BuildChartDataForArrayCreation(IBenchmark benchmark,
            int repetitionsCount) => BuildChart(new ArrayCreationTaskFactory(),
                benchmark, repetitionsCount, "Create array");

        public static ChartData BuildChartDataForMethodCall(IBenchmark benchmark,
            int repetitionsCount) => BuildChart(new MethodCallTaskFactory(),
                benchmark, repetitionsCount, "Call method with argument");
    }
}