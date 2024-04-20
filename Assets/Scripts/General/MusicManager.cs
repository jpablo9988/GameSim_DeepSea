using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource speakers;
    public bool IsPlayingMusic { get; private set; }
    public static MusicManager Instance { get; private set; }

    [SerializeField]
    private List<AudioSource> audioSources;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            Debug.LogWarning("More than one Music Manager found in scene.");
        }
        else Instance = this;
        speakers = GetComponent<AudioSource>();
        FactorySettings();
    }
    /*              startFade:  
     *  takes into account an existing audio. Fades out audio and replaces it with a new one. 
     */
    private IEnumerator StartFade(AudioClip audio, float duration)
    {
        float currentTime = 0;
        float start;
        // --- Fade out previous track ....
        if (IsPlayingMusic)
        {
            start = speakers.volume;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                speakers.volume = Mathf.Lerp(start, 0, currentTime / duration);
                yield return null;
            }
            speakers.Stop();
        }
        // ---- Fade in new Track .... //
        IsPlayingMusic = true;
        speakers.clip = audio;
        speakers.Play();
        currentTime = 0;
        start = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            speakers.volume = Mathf.Lerp(start, Settings.Volume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
    private IEnumerator StopFade(float duration)
    {
        float currentTime = 0;
        float start;
        // --- Fade out previous track ....
        if (IsPlayingMusic)
        {
            start = speakers.volume;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                speakers.volume = Mathf.Lerp(start, 0, currentTime / duration);
                yield return null;
            }
            speakers.Stop();
            IsPlayingMusic = false;
        }
        speakers.volume = Settings.Volume;
        yield break;
    }
    public void PlayAudio(AudioClip audio, float duration)
    {
        StartCoroutine(StartFade(audio, duration));
    }
    private void FactorySettings()
    {
        speakers.Stop();
        speakers.volume = 0;
        IsPlayingMusic = false;
    }
    public void StopAudio(float duration)
    {
        StartCoroutine(StopFade(duration));
    }
    public void RefreshVolume()
    {
        if (IsPlayingMusic)
        {
            speakers.volume = Settings.Volume;
            for (int i = 0; i < audioSources.Count; i++)
            {
                audioSources[i].volume = Settings.Volume;
            }
        }
    }
}
