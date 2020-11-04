using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    [Header("Menu AudioSources")]
    public AudioSource myListen;
    public AudioClip switchAudio;
    public AudioClip selectAudio;

    public void HoverPlay()
    {
        myListen.PlayOneShot(switchAudio);
    }
    public void SelectPlay()
    {
        myListen.PlayOneShot(selectAudio);
    }
}
