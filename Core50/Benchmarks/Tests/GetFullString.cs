using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Benchmarks
{
    //[SimpleJob(RuntimeMoniker.Net472, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [MemoryDiagnoser]
    public class GetFullString
    {
        [Benchmark]
        public string Normally()
        {
            string output = "";

            for (int i = 0; i < 100; i++)
            {
                output += i;
            }

            return output;
        }

        [Benchmark]
        public string Interpolation()
        {
            string output = "";

            for (int i = 0; i < 100; i++)
            {
                output += $"{i}";
            }

            return output;
        }

        [Benchmark]
        public string StringBuilder()
        {
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < 100; i++)
            {
                output.Append(i);
            }

            return output.ToString();
        }
    }
}