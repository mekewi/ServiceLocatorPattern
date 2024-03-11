
using System;
using System.Collections.Generic;
using UnityEngine;
namespace ServiceLocatorPattern
{
    public class ServiceLocator : MonoBehaviour
    {
        public ServiceScopeType serviceScope;
        public bool addServiceAsChild;
        readonly Dictionary<Type, object> services = new Dictionary<Type, object>();
        public IEnumerable<object> RegisteredServices => services.Values;
        public bool TryGet<T>(out T service) where T : class
        {
            Type type = typeof(T);
            if (services.TryGetValue(type, out object serviceObj))
            {
                service = serviceObj as T;
                return true;
            }
            service = null;
            return false;
        }
        public T Get<T>() where T : class
        {
            Type type = typeof(T);
            if (services.TryGetValue(type, out object service))
            {
                return service as T;
            }
            throw new ArgumentException($"ServiceManager.Get: Service of type {type.FullName} not registered");
        }
        public ServiceLocator Register<T>(T service)
        {
            Type type = typeof(T);
            var addedSuccessfully = services.TryAdd(type, service);
            if (!addedSuccessfully)
            {
                Debug.LogError($"ServiceManager.Register: Service of type {type.FullName} already registered");
            }
            AfterServiceAdded(service);
            return this;
        }
        public ServiceLocator Register(Type type, object service)
        {
            if (!type.IsInstanceOfType(service))
            {
                throw new ArgumentException("Type of service does not match type of service interface", nameof(service));
            }
            if (!services.TryAdd(type, service))
            {
                Debug.LogError($"ServiceManager.Register: Service of type {type.FullName} already registered");
            }
            AfterServiceAdded(service);
            return this;
        }
        private void AfterServiceAdded<T>(T serviceType) 
        {
            if (addServiceAsChild)
            {
                if (serviceType.GetType().IsSubclassOf(typeof(MonoBehaviour)))
                {
                    var serviceMonoBehaviour = serviceType as MonoBehaviour;
                    serviceMonoBehaviour.transform.SetParent(transform);
                }
            }
        }
        public void DontDestory()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
