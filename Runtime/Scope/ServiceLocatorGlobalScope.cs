using UnityEngine;

namespace ServiceLocatorPattern
{
    public class ServiceLocatorGlobalScope : ServiceLocatorScopeBase
    {
        public ServiceLocator serviceLocator;
        const string k_globalServiceLocatorName = "ServiceLocator [Global]";
        public override bool TryGetServiceLocator(MonoBehaviour context, out ServiceLocator serviceLocatorFounded)
        {
            if (serviceLocator != null)
            {
                serviceLocatorFounded = serviceLocator;
                return true;
            }
            var container = new GameObject(k_globalServiceLocatorName, typeof(ServiceLocator));
            serviceLocator = container.GetComponent<ServiceLocator>();
            serviceLocator.DontDestory();
            serviceLocator.serviceScope = ServiceScopeType.Global;
            serviceLocator.dontDestroy = true;
            serviceLocatorFounded = serviceLocator;
            return true;
        }
    }
}
