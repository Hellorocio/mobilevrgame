using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScript : MonoBehaviour
{
    public GameObject particles;
    AudioSource audioSource;

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
}
