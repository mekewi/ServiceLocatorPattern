using UnityEngine;

namespace ServiceLocatorPattern
{
    public class ServiceLocatorGameObjectScope : ServiceLocatorScopeBase
    {
        public override bool TryGetServiceLocator(MonoBehaviour context, out ServiceLocator serviceLocatorFounded)
        {
            var serviceLocator = context.GetComponentInParent<ServiceLocator>();
            if (serviceLocator != null)
            {
                serviceLocatorFounded = serviceLocator;
                return true;
            }
            serviceLocatorFounded = null;
            return false;
        }
    }
}
