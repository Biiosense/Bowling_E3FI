using UnityEngine;
using System.Collections;

public class ResetPosition : MonoBehaviour 
{
	private Vector3 position;
	private Quaternion rotation;

	void Awake () {
		position = transform.position;
		rotation = transform.rotation;
	}

	public void Reset () 
	{
		transform.position = position;
		transform.rotation = rotation;

		if(GetComponent<Rigidbody>() != null && !GetComponent<Rigidbody>().isKinematic) 
		{ 
			GetComponent<Rigidbody>().velocity = Vector3.zero; 
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero; 
		}
	}
}