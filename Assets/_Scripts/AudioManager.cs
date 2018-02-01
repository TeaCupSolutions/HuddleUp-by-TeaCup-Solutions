using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioSource ES;

	public string[] audioName;
	public AudioClip[] audioClip;
	public bool clipFound;

	public void Play(string clipName)
	{
		for (int i = 0; i < audioName.Length ; i++) 
		{
			if (clipName == audioName [i]) {
				AudioSource audio = GetComponent<AudioSource>();
				audio.clip = audioClip [i];
				audio.Play ();
				clipFound = true;
				break;
			} 
			else 
			{
				clipFound = false;
			}
		}
		if (!clipFound)
		{
			Debug.Log ("Clip not found");
		}
	}
}
