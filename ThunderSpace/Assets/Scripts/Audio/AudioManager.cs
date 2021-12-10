using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] AudioSource sFXPlayer;
    public void PlaySFX(AudioClip audioClip, float volume){
        //sFXPlayer.clip = audioClip;
        //sFXPlayer.volume = volume;
        sFXPlayer.PlayOneShot(audioClip,volume);
    }

}
