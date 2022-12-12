using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;
    void Start()
    {
        audioSource.PlayOneShot(clip, volume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
