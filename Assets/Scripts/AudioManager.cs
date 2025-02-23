using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip sugarPlum;
    [SerializeField] private AudioClip littleSwans;
    [SerializeField] private AudioClip dunmeshi;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource sfxSource;

    public int turns = 0; 
    private void Awake()
    {
        var sample = Random.value; 
        if(sample <= 0.45)
        {
            audioSource.clip = sugarPlum; 
        }
        else if (sample <= (0.45 + 0.45))
        {
            audioSource.clip = littleSwans; 
        }
        else
        {
            audioSource.clip = dunmeshi; 
        }
        DontDestroyOnLoad(this);
        audioSource.Play(); 
    }
    
    public void PlayWoodBlock()
    {
        sfxSource.Play();
    }
}
