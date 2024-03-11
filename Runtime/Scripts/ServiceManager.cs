using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocatorPattern
{
    public static class ServiceManager
    {
        private static ServiceScopeFactory serviceScopeFactory;
        public static T GetService<T>(this MonoBehaviour context) where T : class
        {
            foreach (ServiceScopeType serviceScope in (ServiceScopeType[])Enum.GetValues(typeof(ServiceScopeType)))
            {
                var scope = serviceScopeFactory.GetServiceLocatorByType(serviceScope);
                var isFounded = GetServiceLocatorFromScope(scope, context, out ServiceLocator serviceLocator);
                if (!isFounded)
                {
                    continue;
                }
                var service = GetServiceFromLocator<T>(serviceLocator);
                if (service != null)
                {
                    return service;
                }
            }
            return null;
        }
        public static T GetService<T>(this MonoBehaviour context,ServiceScopeType serviceScope) where T : class
        {
            var scope = serviceScopeFactory.GetServiceLocatorByType(serviceScope);
            var isFounded = GetServiceLocatorFromScope(scope,context,out ServiceLocator serviceLocator);
            if (!isFounded)
            {
                throw new ArgumentException($"ServiceManager.GetServiceByScopeType: ServiceLocator of scope : {serviceLocator} not created");
            }
            return GetServiceFromLocator<T>(serviceLocator);
        }
        public static bool GetServiceLocatorFromScope(ServiceLocatorScopeBase scope, MonoBehaviour context,out ServiceLocator serviceLocator)
        {
            return scope.TryGetServiceLocator(context,out serviceLocator);
        }
        public static T GetServiceFromLocator<T>(ServiceLocator serviceLocator) where T : class
        {
            serviceLocator.TryGet(out T service);
            return service;
        }
        public static void Register<T>(this MonoBehaviour context, ServiceScopeType serviceScope,T service) where T : class
        {
            var scope = serviceScopeFactory.GetServiceLocatorByType(serviceScope);
            var isFounded = GetServiceLocatorFromScope(scope, context, out ServiceLocator serviceLocator);
            if (!isFounded)
            {
                throw new ArgumentException($"ServiceManager.GetServiceByScopeType: ServiceLocator of scope : {serviceLocator} not created");
            }
            serviceLocator.Register(service);
        }
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void ResetStatics()
        {
            serviceScopeFactory = new ServiceScopeFactory();
        }
    }
}
