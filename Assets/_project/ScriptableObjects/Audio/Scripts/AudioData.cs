using System;
using UnityEngine;
[Serializable]
public class AudioData
{
    public SoundEnum soundName;

    public AudioClip clip;

    [Range(0, 1f)]
    public float valume = 0.5f;

    [Range(0.1f, 2f)]
    public float pitch = 1f;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
