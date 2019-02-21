using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioComponent : MonoBehaviour {

    public AudioClip engineIdle;
    public AudioClip engineRun;
    public AudioClip shotFire;


    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        if(gameObject.layer == 11)
        {
            audioSource.clip = engineIdle;
            audioSource.Play();
        }
    }
	
    public void PlayEngineRun()
    {
        if(audioSource.clip == engineIdle)
        {
            audioSource.clip = engineRun;
            audioSource.Play();
        }
    }

    public void PlayEngineIdle()
    {
        if (audioSource.clip == engineRun)
        {
            audioSource.clip = engineIdle;
            audioSource.Play();
        }
    }

    public void PlayShot()
    {
        audioSource.PlayOneShot(shotFire);
    }
}
