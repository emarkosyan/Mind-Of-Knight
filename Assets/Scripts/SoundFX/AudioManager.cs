using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIController;

public class AudioManager : MonoBehaviour
{
    #region Static Instance
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }

            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    #endregion

    #region Fields              
    static int N = 2;
    public int TrackSelector;
    public int TrackHistory;
    public int musicValue;
    public int masterValue;
    public int sfxValue;

    private AudioSource musicSource;
    private AudioClip[] musicClips;
    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        MenuLogic manager = new MenuLogic();
        //musicValue = (int)manager.MusicSound.value;
        TrackSelector = Random.Range(0, N);
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;

    }

    private void PlayMusicWithFade(AudioClip newClip, float transition = 1.0f)
    {
        StartCoroutine(UpdateMusicWithFade(musicSource, musicClips[TrackSelector + 1], transition));
    }

    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {

        if (!activeSource.isPlaying)
            activeSource.Play();

        float t = 0.0f;

        //Fade Out
        for ( t = 0; t < transitionTime; t+= Time.deltaTime)
        {
            activeSource.volume = (musicValue - ((t / transitionTime) * musicValue));
            yield return null;

        }

        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        //Fade in
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (t / transitionTime) * musicValue;
            yield return null;

        }
    }
    public void PlayMusic(AudioClip[] musicClip)
    {
        musicSource.clip = musicClip[TrackSelector];
        musicSource.volume = musicValue;
        musicSource.Play();
    }

}