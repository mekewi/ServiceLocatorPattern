using ServiceLocatorPattern;
using UnityEngine;
public interface IAudioManager
{
    public void PlayAudio();
}
public class ServiceLocatorAudioManager_Test : MonoBehaviour, IAudioManager
{
    private void Awake()
    {
        this.Register<IAudioManager>(ServiceScopeType.Global, this);
    }
    public void PlayAudio()
    {
        Debug.Log("Play Audio");
    }
}
