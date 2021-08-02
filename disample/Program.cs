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
            diContainer.AddDependency(typeof(IServiceConsumer), typeof(ServiceConsumer));
            diContainer.AddDependency(typeof(IMessageService), typeof(GreetingMessageService));

            var diResolver = new DependencyResolver(diContainer);            
            
            var service  = diResolver.GetService<IServiceConsumer>();
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
            return (T)GetService(typeof(T));
        }

        public object GetService(Type type){
            var objectToCreate =  _container.GetDependency(type);
            var construtor = objectToCreate.GetConstructors().Single();
            var constructorParameters = construtor.GetParameters().ToArray();
            var constructorParametersImpelementation = new  object[constructorParameters.Length];
            if(constructorParameters.Length > 0){
                for(int i=0; i < constructorParameters.Length; i++){
                    constructorParametersImpelementation[i] =  GetService(constructorParameters[i].ParameterType);
                }
            }
            return Activator.CreateInstance(objectToCreate,constructorParametersImpelementation);
        }
    }

    public class DependencyContainer{

        Dictionary<Type,Type> _containers;

        public DependencyContainer(){
            _containers = new Dictionary<Type, Type>();
        }

        public void AddDependency(Type parent, Type child){
            _containers.Add(parent,child);
        }

        public Type GetDependency(Type findInterface){            
            return _containers[findInterface];
        }
    }

    public class ServiceConsumer : IServiceConsumer{

        IMessageService _service;
        public ServiceConsumer(IMessageService service){
            _service = service;
        }

        public void DisplayGreetings(){
            _service.ShowMessage();
        }        
    }

    public interface IServiceConsumer
    {
        void DisplayGreetings();
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
