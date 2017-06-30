using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revolution : MonoBehaviour {

    public float speedRevolution;
    public GameObject center;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(center.transform.position, Vector3.up, Time.deltaTime* speedRevolution);
    }
}
