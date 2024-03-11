namespace ServiceLocatorPattern
{
    public class ServiceScopeFactory
    {
        private readonly ServiceLocatorGlobalScope globalService = new ServiceLocatorGlobalScope();
        private readonly ServiceLocatorSceneScope sceneService = new ServiceLocatorSceneScope();
        private readonly ServiceLocatorGameObjectScope gameObjectService = new ServiceLocatorGameObjectScope();
        public ServiceLocatorScopeBase GetServiceLocatorByType(ServiceScopeType serviceScope)
        {
            switch (serviceScope)
            {
                case ServiceScopeType.Global:
                    return globalService;
                case ServiceScopeType.Scene:
                    return sceneService;
                case ServiceScopeType.GameObject:
                    return gameObjectService;
            }
            return globalService;
        }
    }
}
