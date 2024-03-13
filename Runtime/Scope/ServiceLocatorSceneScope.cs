using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ServiceLocatorPattern
{
    public class ServiceLocatorSceneScope : ServiceLocatorScopeBase
    {
        private Dictionary<Scene, ServiceLocator> sceneContainers = new Dictionary<Scene, ServiceLocator>();
        const string k_sceneServiceLocatorName = "ServiceLocator [Scene]";
        public override bool TryGetServiceLocator(MonoBehaviour context, out ServiceLocator serviceLocatorFounded)
        {
            Scene scene = context.gameObject.scene;
            if (GetLocatorFromDic(scene, out ServiceLocator serviceLocatorFoundedInDic))
            {
                serviceLocatorFounded = serviceLocatorFoundedInDic;
                return true;
            }
            if (FindServiceLocatorFromTheScene(scene, out ServiceLocator serviceLocatorFoundedInScene))
            {
                sceneContainers.Add(scene, serviceLocatorFoundedInScene);
                serviceLocatorFounded = serviceLocatorFoundedInScene;
                return true;
            }
            var newLocator = CreateNewServiceLocator(scene);
            sceneContainers.Add(scene, newLocator);
            serviceLocatorFounded = newLocator;
            return true;
        }
        private bool GetLocatorFromDic(Scene scene, out ServiceLocator serviceLocator)
        {
            if (sceneContainers.TryGetValue(scene, out serviceLocator))
            {
                return true;
            }
            return false;
        }
        private bool FindServiceLocatorFromTheScene(Scene scene, out ServiceLocator serviceLocator)
                {
            var tmpSceneGameObjects = new List<GameObject>();
            scene.GetRootGameObjects(tmpSceneGameObjects);
            foreach (GameObject go in tmpSceneGameObjects.Where(go => go.GetComponent<ServiceLocator>() != null))
            {
                if (go.TryGetComponent(out ServiceLocator serviceLocatorComponent))
                {
                    if (serviceLocatorComponent.serviceScope == ServiceScopeType.Scene)
                    {
                        serviceLocator = serviceLocatorComponent;
                        return true;
                    }
                }
            }
            serviceLocator = null;
            return false;
        }
        private ServiceLocator CreateNewServiceLocator(Scene scene)
        {
            var newServiceLocatorGameobject = new GameObject(k_sceneServiceLocatorName);
            var newServiceLocator = newServiceLocatorGameobject.AddComponent<ServiceLocator>();
            newServiceLocator.serviceScope = ServiceScopeType.Scene;
            SceneManager.MoveGameObjectToScene(newServiceLocatorGameobject, scene);
            return newServiceLocator;
        }
    }
}
