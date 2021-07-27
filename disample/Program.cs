using System;

namespace disample
{
    class Program
    {
        static void Main(string[] args)
        {
            var dependency = new DependencyResolver();
            //var service  = (HelloMessageService)dependency.CreateService(typeof(HelloMessageService));
            var service  = dependency.CreateService<HelloMessageService>();
            var consumer = (ServiceConsumer)dependency.CreateService(typeof(ServiceConsumer),service);
            consumer.DisplayGreetings();
            Console.WriteLine("Done");
        }
    }

    public class DependencyResolver{

        public object CreateService(Type t){
            return Activator.CreateInstance(t);
        }

        public T CreateService<T>(){
            return (T)Activator.CreateInstance(typeof(T));

        }


        public object CreateService(Type t,object parameter){
            return Activator.CreateInstance(t,parameter);

        }

    }

    public class ServiceConsumer{

        HelloMessageService _service;
        public ServiceConsumer(HelloMessageService service){
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
