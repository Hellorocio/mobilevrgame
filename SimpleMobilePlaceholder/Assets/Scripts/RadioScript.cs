using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScript : MonoBehaviour
{
    public GameObject particles;
    AudioSource audioSource;

    public AudioClip[] bgMusic; //[0] = untimed, [1] = timed

    public enum MusicType {Untimed, Timed};

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ClickRadio ()
    {
        if (audioSource != null && particles != null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                particles.SetActive(false);
            }
            else
            {
                audioSource.Play();
                particles.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Called by buttons [0] = set to untimed, [1] = set to timed
    /// </summary>
    /// <param name="musicNum"></param>
    public void SetMusic (MusicType musicType)
    {
        audioSource.clip = bgMusic[(int)musicType];
    }
    
}
