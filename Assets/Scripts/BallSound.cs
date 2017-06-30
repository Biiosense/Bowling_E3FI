using UnityEngine;
using System.Collections;

public class BallSound : MonoBehaviour 
{
	/*
	 * Play the sound when the ball hits the floor
	 */
	void OnCollisionEnter(Collision collision) 
	{
		AudioSource sound = GetComponent<AudioSource>();

		if(!sound.isPlaying && collision.collider.gameObject.tag == "Floor")
		{
			sound.Play();
		} 
	}
}
