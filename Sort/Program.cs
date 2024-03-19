
using BenchmarkDotNet.Running;

namespace Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<SortingAlgorithms>();
        }
    }
}