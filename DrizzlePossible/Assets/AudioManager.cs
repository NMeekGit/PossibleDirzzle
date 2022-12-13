using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    [SerializeField] private Sound[] _sounds;

    void Awake() {
        
        DontDestroyOnLoad(gameObject);

        foreach ( Sound s in _sounds) {
            s._source = gameObject.AddComponent<AudioSource>();
            s._source.clip = s._clip;
            s._source.volume = s._volume;
            s._source.pitch = s._pitch;
            s._source.loop = s._loop;
        }
    }
    
    void Start() {

        Play("Theme");

    }

    public void Play(string name) {

        Sound s = Array.Find(_sounds, sound => sound._name == name);
        if (s == null) return;
        s._source.Play();
        Debug.Log("[Audio] Playing " + s._name);
    }
}
