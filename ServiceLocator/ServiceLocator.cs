namespace CoreSystems.ServiceLocator
{
    using System.Collections.Generic;
    using System;

    public class ServiceLocator: IServiceLocator
    {
        private Dictionary<object, object> _services = new Dictionary<object, object>();


        public T GetService<T>()
        {
            if (_services.TryGetValue(typeof(T), out object value))
            {
                return (T)value;
            }
            else
                throw new Exception("The service was not finded");
        }

        public void RegisterService<T>(object service)
        {
            if (_services.ContainsKey(typeof(T)))
            {
                _services[typeof(T)] = service;
            }
            else
                _services.Add(typeof(T), service);
        }

        public void RemoveService<T>()
        {
            if (_services.ContainsKey(typeof(T)))
            {
                _services.Remove(typeof(T));
            }
        }
    }

    public static class PublicServiceLocator
    {
        private static IServiceLocator _serviceLocator;
        public static IServiceLocator s_serviceLocator => _serviceLocator == null ? SetService() : _serviceLocator;

        private static IServiceLocator SetService()
        {
            return _serviceLocator = new ServiceLocator();
        }
    }
}
