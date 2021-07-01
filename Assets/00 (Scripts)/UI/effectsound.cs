using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectsound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip typing;

    void Awake()
    {
        audioSource.clip = typing;
    }
    
    void Update()
    {
        
    }
}
