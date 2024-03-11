
using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace ServiceLocatorPattern
{
    public class ServiceLocatorScopeBase
    {
        public virtual bool TryGetServiceLocator(MonoBehaviour context, out ServiceLocator serviceLocatorFounded)
        {
            throw new NotImplementedException();
        }
    }
}
