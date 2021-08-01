using System;
using System.Collections.Generic;
using System.Linq;

namespace disample
{
    class Program
    {
        static void Main(string[] args)
        {
            var diContainer = new DependencyContainer();
            diContainer.AddDependency(typeof(ServiceConsumer));
            diContainer.AddDependency(typeof(HelloMessageService));

            var diResolver = new DependencyResolver(diContainer);            
            
            var service  = diResolver.GetService<ServiceConsumer>();
            service.DisplayGreetings(); 
            Console.WriteLine("Done");
        }
    }

    public class DependencyResolver{

        private DependencyContainer _container;
        public DependencyResolver(DependencyContainer container){
            _container = container;
        }

        public T GetService<T>(){
            //get type
            //get consturctor
            //get construtor parameter

            var objectToCreate =  _container.GetDependency(typeof(T));
            var construtor = objectToCreate.GetConstructors().Single();
            var constructorParameters = construtor.GetParameters().ToArray();
            var constructorParametersImpelementation = new  object[constructorParameters.Length];
            if(constructorParameters.Length > 0){
                for(int i=0; i < constructorParameters.Length; i++){
                    constructorParametersImpelementation[i] = Activator.CreateInstance(constructorParameters[i].ParameterType);
                }
            }
            return (T)Activator.CreateInstance(objectToCreate,constructorParametersImpelementation);
        }

    }

    public class DependencyContainer{

        List<Type> _containers;

        public DependencyContainer(){
            _containers = new List<Type>();
        }

        public void AddDependency(Type t){
            _containers.Add(t);
        }

        public Type GetDependency(Type find){
            return _containers.First(t => t.Name  == find.Name);
        }
    }

    public class ServiceConsumer{

        IMessageService _service;
        public ServiceConsumer(GreetingMessageService service){
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
        public GreetingMessageService(){

        }

        public void ShowMessage(){
            Console.WriteLine($"Greetings there");
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
