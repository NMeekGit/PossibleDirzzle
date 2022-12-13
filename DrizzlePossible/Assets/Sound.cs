using UnityEngine.Audio;
using UnityEngine;

[System.SerializableAttribute]
public class Sound
{
    public string _name;
    public AudioClip _clip;
    public AudioSource _source;

    [Range(0f, 1f)]
    public float _volume;
    [Range(.1f, 3f)]
    public float _pitch;
    public bool _loop;

}
