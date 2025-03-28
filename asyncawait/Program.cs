using System;
using System.Threading;
using System.Threading.Tasks;

namespace asyncawait
{
    class Program
    {
        static void Main(string[] args)
        {

            var taskMethod1Run = GetResponseFromHttp();
            Method2_ShortRunning();            

            //Task<int> nf = new (NormalFunction);
            //nf.Start();
            var count = taskMethod1Run.Result;
            //Console.WriteLine(nf.Result);
            //var count = taskMethod1Run.Result;
            Console.WriteLine($"Method 1 length {count}");

            Console.WriteLine("End");
            Console.ReadKey();
        }


        static async Task<int> GetResponseFromHttp(){
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

        static async Task void_method(){
            await GetResponseFromHttp();
        }

        private static int  NormalFunction() {
            int loop = 30;
            for (var i = 0; i < loop; i++) {
                Thread.Sleep(1000);
                Console.WriteLine("Normal function {0}", i);
            }

            return loop;
        }
    }
}
