using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectableButton : MonoBehaviour,  ISelectHandler, IDeselectHandler, ISubmitHandler
{
    public RawImage icon;
    public bool isSelected = false;
    public bool isInitialSelected = false;
    public AudioClip highlightAudioClip;
    public AudioClip submitAudioClip;
    public bool shouldPlaySound = true;
    public string levelTargetToLoad = "SaveGame";

    private AudioSource audioSource;
    
    public void Start()
    {
        if (!isSelected)
        {
            icon.gameObject.SetActive(false);    
        }
        else
        {
            isInitialSelected = true;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        isSelected = true;
        icon.gameObject.SetActive(true);
        if (shouldPlaySound && audioSource != null && highlightAudioClip != null && !isInitialSelected)
        {
            audioSource.PlayOneShot(highlightAudioClip);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (isInitialSelected)
        {
            isInitialSelected = false;
        }

        isSelected = false;
        icon.gameObject.SetActive(false);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        if (shouldPlaySound && audioSource != null)
        {
            audioSource.PlayOneShot(submitAudioClip);
            Invoke(nameof(LoadScene),2);
        }
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(levelTargetToLoad);
    }
    
   
}
