using UnityEngine;
using Cysharp.Threading.Tasks;

public class AudioVolumeController : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeTime = 1.0f;
    public float largeVolume = 1.0f;
    public float smallVolume = 0.2f;
    private bool isLouder = true;

    private void Start()
    {
        if (audioSource == null)
        {
            #if UNITY_EDITOR
            Debug.LogWarning("AudioSource is missing");
            #endif
            return;
        }
    }

    private async UniTask Fade(float duration, float fromVolume, float toVolume)
    {
        if (audioSource == null)
        {
            #if UNITY_EDITOR
            Debug.LogWarning("audiosource is null");
            #endif
            return;
        }
        
        float elapsedTime = 0.0f;

        //fade loop
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(fromVolume, toVolume, elapsedTime/duration);
            await UniTask.Yield();
        }
        audioSource.volume = toVolume;
    }

    public void FadeLoudeMethod()
    {
        if (!isLouder)
        {
            Fade(fadeTime, smallVolume, largeVolume).Forget();
            isLouder = true;
        }
    }

    public void FadeQuietMethod()
    {
        if (isLouder)
        {
            Fade(fadeTime, largeVolume, smallVolume).Forget();
            isLouder = false;
        }
    }

    public void ToggleVolumeSwithcer()
    {
        if(isLouder)
            FadeQuietMethod();
        else if(!isLouder)
            FadeLoudeMethod();
    }

}