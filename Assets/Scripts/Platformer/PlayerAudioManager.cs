using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{ 
    // Start is called before the first frame update
    public AudioClip jumpSound;
    public AudioClip collectHeartSound;
    public AudioClip dieSound;
    public List<AudioSource> AudioSources = new List<AudioSource>();
    
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            AudioSources.Add(audioSource);
        }
    }

    public AudioSource FindFirstAvailableAudioSource()
    {
        
        foreach (AudioSource source in AudioSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }

        }
        // if all are busy just return the first i get
        return GetComponent<AudioSource>();
    }

    public void PlayClip(string state)
    {
        switch (state)
        {
            case "JUMP":
                FindFirstAvailableAudioSource().PlayOneShot(jumpSound);
                break;
            case "COLLECT_HEART":
                FindFirstAvailableAudioSource().PlayOneShot(collectHeartSound);
                break;
            case "DIE":
                FindFirstAvailableAudioSource().PlayOneShot(dieSound);
                break;
        }
    }
    
}
