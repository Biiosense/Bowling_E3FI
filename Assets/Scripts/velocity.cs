using System.Collections;
using UnityEngine;

public class velocity : MonoBehaviour
{

    private Rigidbody _rigidbody;
    private Vector3 speed;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        speed = _rigidbody.velocity;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, 30, 100, 90), "Measurements");

        GUI.Label(new Rect(10, 50, 100, 20), speed.ToString() + "m/s");
    }
}
