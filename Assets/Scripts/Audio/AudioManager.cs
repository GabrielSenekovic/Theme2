using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;

    private void Start() 
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public static void PlaySound(AudioClip clip)
    {
        instance.GetComponent<AudioSource>().clip = clip;
        instance.GetComponent<AudioSource>().Play();
    }
}
