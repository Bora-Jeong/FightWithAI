using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("AudioSource")]
    [SerializeField]
    AudioSource uiAudio;
    [SerializeField]
    AudioSource backgroundAudio;
    [SerializeField]
    AudioSource trayAudio;

    [Header("Sound Clip")]
    
    [SerializeField]
    AudioClip drop_ingredient;
    [SerializeField]
    AudioClip serve;
    [SerializeField]
    AudioClip dump;

    public void DropSound()
    {
        trayAudio.clip = drop_ingredient;
        trayAudio.Play();
    }
    public void ServeSound()
    {

        uiAudio.clip = serve;
        uiAudio.Play();
    }
    public void DumpSound()
    {

        if (!uiAudio.isPlaying)
        {
            uiAudio.clip = dump;
            uiAudio.Play();
        }

    }

}
