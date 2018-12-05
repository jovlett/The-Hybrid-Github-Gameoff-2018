using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour {
    public AudioClip[] clips;
    private AudioSource aud;
    private bool playing;

	void Start () {
        aud = GetComponent<AudioSource>();	
	}
	
	void Update () {
		if(!aud.isPlaying)
        {
            aud.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }
	}
}
