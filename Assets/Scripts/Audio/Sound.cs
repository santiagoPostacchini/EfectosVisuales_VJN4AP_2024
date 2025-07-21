using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0, 1)]
    public float volume;

    [HideInInspector] public float defaultVolume;

    [Range(0.1f, 3)]
    public float pitch;

    public bool loop = false;

    [HideInInspector] public AudioSource source;
}
