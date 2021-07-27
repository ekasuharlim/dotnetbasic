using System;

namespace disample
{
    class Program
    {
        static void Main(string[] args)
        {
            var dependency = new DependencyServices();
            var service  = (HelloMessageService)dependency.CreateService(typeof(HelloMessageService));
            MessageConsumer consumer = new MessageConsumer(service);
            consumer.DisplayGreetings();
            Console.WriteLine("Done");
        }
    }

    public class DependencyServices{

        public object CreateService(Type t){
            return Activator.CreateInstance(t);

        }
    }

    public class MessageConsumer{

        HelloMessageService _service;
        public MessageConsumer(HelloMessageService service){
            _service = service;
        }

        public void DisplayGreetings(){
            _service.ShowMessage();
        }        
    }

    public class HelloMessageService
    {
        public HelloMessageService(){

        }

        public void ShowMessage(){
            Console.WriteLine("Hello there");
        }

    }
}
