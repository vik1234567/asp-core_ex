using System;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp1
{
    public class ClassA
    {
        //public void DoWork()
        //{
        //    var b = new ClassB();
        //    b.DoStuff();
        //}

        private readonly IThing _dependency;

        public ClassA(IThing thing) => _dependency = thing;
        public void DoWork() => _dependency.DoStuff();
    }
    
    public class ClassB : IThing
    {
        public void DoStuff()
        {
            
            // Imagine implementation
            Console.WriteLine($"ClassB:DoStuff");
        }
    }

    public interface IThing
    { 
        void DoStuff();
    }

    class Program
    {
        static void Main(string[] args)
        {
            // See https://www.stevejgordon.co.uk/aspnet-core-dependency-injection-what-is-the-iservicecollection
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<ClassA>();
            serviceCollection.AddSingleton<IThing, ClassB>();

            // See https://www.stevejgordon.co.uk/aspnet-core-dependency-injection-what-is-the-iserviceprovider-and-how-is-it-built
            var serviceProvider = serviceCollection.BuildServiceProvider();


            IThing thing = new ClassB();
            ClassA classA = new ClassA(thing);
            classA.DoWork();

            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
