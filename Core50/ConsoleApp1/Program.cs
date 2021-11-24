using System;

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
            IThing thing = new ClassB();
            ClassA classA = new ClassA(thing);
            classA.DoWork();
        }
    }
}
