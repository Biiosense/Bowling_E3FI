using UnityEngine;
using System.Collections;

public class PinSound : MonoBehaviour 
{
	private AudioSource sound;
	
	void Start () 
	{
		sound = GetComponent<AudioSource>();

		// BEND PITCH FROM 0.7 TO 1.1 TO VARY THE SOUNDS
		sound.pitch = 0.7f + (float) Random.Range(1,4) / 10;
	}

	void OnCollisionEnter (Collision collision) 
	{
		if ( !sound.isPlaying )
		{
			sound.Play();
		}
	}
}