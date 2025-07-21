using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SongManager : MonoBehaviour
{
    public Sound[] songs;

    public static SongManager Instance;

    public Button songVolButton;

    public float volumeStep = 0.05f;

    public float volumeMultiplier = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in songs)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.defaultVolume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void MainMenuLoad()
    {
        ChangeSong("MainMenuSong");
    }

    public void GameLoad()
    {
        ChangeSong(GetRandomSong());
    }

    public void Play(string name)
    {
        for (int i = 0; i < songs.Length; i++)
        {
            if (songs[i].name == name)
            {
                Sound s = songs[i];
                s.source.Play();
            }
        }
    }

    public string GetRandomSong()
    {
        Sound s = songs[Random.Range(1, songs.Length)];
        return s.name;
    }

    public void StopAllSongs()
    {
        for (int i = 0; i < songs.Length; i++)
        {
            Sound s = songs[i];
            s.source.Stop();
        }
    }
    public void ChangeSong(string name)
    {
        Sound targetSong = null;

        foreach (Sound s in songs)
        {
            if (s.source.isPlaying)
                s.source.Stop();

            if (s.name == name)
                targetSong = s;
        }

        targetSong?.source.Play();
    }
}