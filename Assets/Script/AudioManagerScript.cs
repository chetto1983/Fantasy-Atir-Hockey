using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{

    #region Varaible

    public AudioSource audioSource;


    #endregion


    public static AudioManagerScript current;


 

    private void Awake()
    {
        current = this;
    }


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip Clip, float volume)
    {
        audioSource.PlayOneShot(Clip, volume);
        
    }

}
