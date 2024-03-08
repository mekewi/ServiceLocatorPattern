# This is an Implementation of the Service Locator Design Pattern for unity you can register and get services along the project without coupling classes and for more decoupling between classes 

# Scopes
you have 3 scopes you choose between them 
1. Global scope: this scope will make the service like a singleton so it lives along the project runtime session and will ensure only one instance of the service will be returned.
2. Scene Scope: this scope will make the service available in a specific scene only, and once the scene gets unloaded all services in this scene will be unloaded.
3. GameObject Scope: this scope will register a service for a specific game object.

# How To use it 
ServiceManager.Register<IAudioManager>(ServiceScopeType.Global, this);
ServiceManager.GetService<IAudioManager>().PlayAudio();
