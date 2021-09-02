using System;

namespace delegatesample
{
    class Program
    {
        private delegate int mathOperation(int x, int y);        
        
        static void Main(string[] args)
        {
            mathOperation op1 = add;
            mathOperation op2 = addPlus4;
            Console.WriteLine(Calculate2plus3(op1));
            Console.WriteLine(Calculate2plus3(op2));

            mathOperation chainedOperation = add;
            chainedOperation += addPlus4;
            //below command will run add and addPlus4 method
            Console.WriteLine(Calculate2plus3(chainedOperation));
            

            Action<int> p1 = Print;
            p1(100);

            Action<ISampleClass> p2 = PrintSample;
            p2(new SampleClass());

            WriteInt(str => Convert.ToInt32(str));

            Console.WriteLine("End");
        }

        static void WriteInt(Func<string,int> f1){
            Console.WriteLine(f1("120"));
        }

        static void PrintSample(ISampleClass smpl){
            smpl.ShowSampleClass("abc");
        }

        static void Print(int x){
            Console.WriteLine(x);
        }

        static int Calculate2plus3(mathOperation op){            
            return op(2,3);
        }

        static int add(int x, int y){
            Console.WriteLine("using normal add");
            return x + y;
        }

        static int addPlus4(int x, int y){
            Console.WriteLine("using addPlus4");
            return x + y + 4;
        }

        
        public interface ISampleClass{
            void ShowSampleClass(string msg);
        }

        public class SampleClass : ISampleClass
        {
            public void ShowSampleClass(string msg)
            {
                Console.WriteLine($"From sample class {msg}");
            }            
        }
    }
}
