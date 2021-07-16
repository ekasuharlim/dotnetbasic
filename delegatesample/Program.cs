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
            Console.WriteLine("End");
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

        
    }
}
