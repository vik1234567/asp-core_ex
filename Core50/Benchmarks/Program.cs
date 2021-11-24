using System;
using BenchmarkDotNet.Running;

namespace Benchmarks
{
    // dotnet BenchmarkAndSpanExample.dll

    class Program
    {
        static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<NameParserBenchmarks>();
            //var summary = BenchmarkRunner.Run<SingleVsFirst>();
            //var summary = BenchmarkRunner.Run<IndividualAddingToArrayListBenchmark>();
            //var summary = BenchmarkRunner.Run<GetFullString>();
            //var summary = BenchmarkRunner.Run<Md5VsSha256>();
            var summary = BenchmarkRunner.Run<JsonTest>();

            Console.ReadLine();
        }
    }
}
