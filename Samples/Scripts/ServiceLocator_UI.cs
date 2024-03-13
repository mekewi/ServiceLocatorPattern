using ServiceLocatorPattern;
using UnityEngine;

public class ServiceLocator_UI : MonoBehaviour
{
    public void PlayAudio()
    {
        this.GetService<IAudioManager>().PlayAudio();
    }
}
