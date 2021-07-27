using System;

namespace disample
{
    class Program
    {
        static void Main(string[] args)
        {
            var diResolver = new DependencyResolver();            
            var service  = diResolver.CreateService<HelloMessageService>();
            //var service  = diResolver.CreateService<GreetingMessageService>("eka");
            var consumer = new ServiceConsumer(service);
            consumer.DisplayGreetings();
            Console.WriteLine("Done");
        }
    }

    public class DependencyResolver{


        public T CreateService<T>(){
            return (T)Activator.CreateInstance(typeof(T));

        }

        public T CreateService<T>(object param){
            return (T)Activator.CreateInstance(typeof(T),param);

        }

    }

    public class ServiceConsumer{

        IMessageService _service;
        public ServiceConsumer(IMessageService service){
            _service = service;
        }

        public void DisplayGreetings(){
            _service.ShowMessage();
        }        
    }

    public interface IMessageService{
        void ShowMessage();
    }
    public class GreetingMessageService : IMessageService
    {
        private string _name;
        public GreetingMessageService(string name){
            _name = name;
        }

        public void ShowMessage(){
            Console.WriteLine($"Greetings {_name}");
        }

    }

    public class HelloMessageService : IMessageService
    {
        public HelloMessageService(){

        }

        public void ShowMessage(){
            Console.WriteLine("Hello there");
        }

    }
}
