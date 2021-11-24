using System;
using System.Collections;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace Benchmarks
{
    [Config(typeof(Config))]
    [MemoryDiagnoser]
    [DisassemblyDiagnoser(printSource: true)]
    public class IndividualAddingToArrayListBenchmark
    {
        private class Config : ManualConfig
        {
            // Force just one iteration, with 1 ops/iteration
            public Config()
            {
                Add(Job.Default.WithInvocationCount(1).WithUnrollFactor(1).WithIterationCount(20));
            }
        }

        public IndividualAddingToArrayListBenchmark()
        {
            // Build an array storing the powers of 2 that interest us in term of inner
            //  array length,  so no time is wasted within the benchmarked methods
            //  themselves for computing this

            // The largest array generated must be able to store our target number of numbers
            int LargestPowerOfTwoNeededToFit = (int)Math.Ceiling(Math.Log(noNumbers, 2));
            int[] powersOfTwo = new int[LargestPowerOfTwoNeededToFit - 1];

            for (int i = 2; i <= LargestPowerOfTwoNeededToFit; i++)
            {
                powersOfTwo[i - 2] = (int)Math.Pow(2, i);
                Console.WriteLine("{0}: {1}", i - 1, powersOfTwo[i - 2]);
            }

            PowersOfTwo = powersOfTwo;
            NumberOfArraysRequiredForNoNumbers = LargestPowerOfTwoNeededToFit - 1;

            Random random = new Random(1);

            // Force the gen buffer for LOH to be big, so we don't have GCs within
            //  the iterations, by allocating a large enough ArrayList instance
            //  at the class level. The GC time will be larger, of course, but we
            //  don't care for just the sample of allocating the ArrayLists, since
            //  the GCs occur in-between iterations. It'll serve a double purpose:
            //  a source for the copying-to-arrays benchmark
            masterSourceArray = new object[2 * noNumbers];


            for (int i = 0; i < 2 * noNumbers; i++)
            {
                masterSourceArray[i] = random.Next(10);
            }

            // Give time for attaching dotTrace to the running process
            //Thread.Sleep(20000);
        }

        object[] masterSourceArray = null;
        const int noNumbers = 10000000; // 10 mil
        private int[] PowersOfTwo = null;
        private int NumberOfArraysRequiredForNoNumbers = 0;


        [Benchmark]
        public void OnlyDefineOneArrayList()
        {
            ArrayList numbers = new ArrayList(noNumbers);
        }


        [Benchmark]
        public void DefineAllArrayListsEnRoute()
        {
            ArrayList numbers = null;

            for (int i = 0; i < NumberOfArraysRequiredForNoNumbers; i++)
            {
                //Console.WriteLine("Creating an array of length: {0}", PowersOfTwo[i]);
                numbers = new ArrayList(PowersOfTwo[i]);
            }

            // Keep a reference so that DCE doesn't kick in
            int dummy = numbers.Count;
        }


        [Benchmark]
        public void DefineObjectArraysEnRouteAndCopyDataToThem()
        {
            // Create an object[] array that is double the previous number of elements
            //  and copy those elements across. Start from the array with 8 elements,
            //  therefore from i=1

            object[] numbers = null;

            for (int i = 1; i < NumberOfArraysRequiredForNoNumbers; i++)
            {
                //Console.WriteLine("Create an object[] array of size: {0}", PowersOfTwo[i]);
                numbers = new object[PowersOfTwo[i]];
                //Console.WriteLine("Copy across: {0} no of elements", PowersOfTwo[i-1]);
                Array.Copy(masterSourceArray, 0, numbers, 0, PowersOfTwo[i - 1]);
            }

            // Keep a reference so that DCE doesn't kick in
            int dummy = numbers.Length;
        }


        [Benchmark]
        public void DefineObjectArraysEnRouteCopyDataToThemAndFillRestWithSameElement()
        {
            // Create an object[] array that is double the previous number of elements
            //  and copy them across. For the other half of the new array, simply fill
            //  it with references to the same pre-boxed int. This will simulate the
            //  behavior of the parameterless-constructed ArrayList and adding the same
            //  element, allowing us to compare performance of the same opertaion. Start
            //  from the array with 8 elements, therefore from i=1

            object[] numbers = null;
            object boxedInt = 1;

            for (int i = 1; i < NumberOfArraysRequiredForNoNumbers; i++)
            {
                //Console.WriteLine("Create an object[] array of size: {0}", PowersOfTwo[i]);
                numbers = new object[PowersOfTwo[i]];
                //Console.WriteLine("Copy across: {0} no of elements", PowersOfTwo[i-1]);
                Array.Copy(masterSourceArray, 0, numbers, 0, PowersOfTwo[i - 1]);
                // Use the array's explicit length in order to help BCE (bound check elimination)
                int maxBracket = numbers.Length < noNumbers ? numbers.Length : noNumbers;
                //Console.WriteLine("Fill the 2nd half of the new object[] array: {0} no of elements", maxBracket-PowersOfTwo[i-1]);
                for (int j = PowersOfTwo[i - 1]; j < maxBracket; j++)
                {
                    numbers[j] = boxedInt;
                }
            }

            // Keep a reference so that DCE doesn't kick in
            int dummy = numbers.Length;
        }


        [Benchmark]
        public void AddSameElementToArrayListWithParameterlessConstructor()
        {
            ArrayList numbers = new ArrayList();
            object boxedInt = 1;

            for (int i = 0; i < noNumbers; i++)
            {
                numbers.Add(boxedInt);
            }
        }
    }
}