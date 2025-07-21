using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager Instance;

    public float pitchVariationRange = .3f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.defaultVolume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("Ambiance", false);
        Play("Crickets", false); 
        Play("Rain", false);
    }

    public void Play(string name, bool varyPitch = false)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                Sound s = sounds[i];
                float p = s.source.pitch;
                if (varyPitch)
                {
                    s.source.pitch = Random.Range(s.source.pitch - pitchVariationRange, s.source.pitch + pitchVariationRange);
                }
                s.source.Play();
                s.source.pitch = p;
            }
        }
    }
}

