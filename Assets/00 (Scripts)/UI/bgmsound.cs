using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmsound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bgm;

    void Awake()
    {
        audioSource.clip = bgm;
    }
}
