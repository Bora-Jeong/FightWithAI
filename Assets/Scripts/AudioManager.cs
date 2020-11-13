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

    private void PlaySound()
    {
       
        
    }

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
        uiAudio.clip = dump;
        if(!uiAudio.isPlaying) uiAudio.Play();
    }

}
