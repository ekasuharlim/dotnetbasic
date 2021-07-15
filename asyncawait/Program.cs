using System;
using System.Threading;
using System.Threading.Tasks;

namespace asyncawait
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = Method1_LongRunning();
            Method2_ShortRunning();            
            var count = a.Result;            
            Console.WriteLine($"Method 1 length {count}");
            Console.WriteLine("End");
            Console.ReadKey();
        }


        static async Task<int> Method1_LongRunning(){
            var count = 0;
            await Task.Run( () => {
                    for(var i=0; i < 50;i++){
                        Console.WriteLine($"From Method 1 {i}");
                        count++;
                        Thread.Sleep(100);                        
                    }
                }
            );
            return count;
        }
        static void Method2_ShortRunning(){
            Thread.Sleep(1000);
            for(var i=0; i < 100;i++){
                Console.WriteLine($"---- Method 2 {i}");
            }
        }
    }
}
