using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BallController : MonoBehaviour 
{
    GameObject ballDetector;
    GameObject mouseDetector;

    Vector2 mouseDistance;
    private Vector2 lastPosition;
    private bool trackMouse = false;

    GameObject[] sliders;

    void Start()
    {
        ballDetector = GameObject.FindGameObjectWithTag("BallDetector");
        mouseDetector = GameObject.FindGameObjectWithTag("MouseDetector");
        sliders = GameObject.FindGameObjectsWithTag("Slider");
        foreach (GameObject slider in sliders)
            slider.SetActive(false);
    }

    public void Reset() 
	{
        GetComponent<ResetPosition>().Reset();
        GetComponent<Rigidbody>().isKinematic = true;
    }

	public void FollowMouse()
	{
        if (Input.GetMouseButtonDown(0))
        {
            trackMouse = true;
            lastPosition = Input.mousePosition;
            mouseDistance = new Vector2();
            foreach (GameObject slider in sliders)
            {
                slider.GetComponent<Slider>().value = 0;
                slider.SetActive(true);
                
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            trackMouse = false;
            foreach (GameObject slider in sliders)
                slider.SetActive(false);
        }
        if (trackMouse)
        {
            Vector2 newPosition = Input.mousePosition;
            mouseDistance.x = (newPosition.x - lastPosition.x) /Screen.width * 300;
            mouseDistance.y = (newPosition.y - lastPosition.y) /(Screen.height-newPosition.y) * 50;

            sliders[0].GetComponent<Slider>().value = mouseDistance.y;
            sliders[1].GetComponent<Slider>().value = mouseDistance.x;
        }
        else
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, Mathf.Infinity))
            {
                if (raycastHit.collider.bounds.Intersects(mouseDetector.GetComponent<Collider>().bounds))
                    transform.position = new Vector3(raycastHit.point.x, transform.position.y, transform.position.z);
            }
        }
    }

	public void Shoot(Vector3 direction, float strength, float torque)
	{
        strength += sliders[0].GetComponent<Slider>().value;
        torque -= sliders[1].GetComponent<Slider>().value;
        Rigidbody body = GetComponent<Rigidbody>();
        body.isKinematic = false;
        body.AddTorque(0, 0, torque);
        body.AddForce(strength * direction, ForceMode.Impulse);
    }

	public bool HasDone()
	{
        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        if ( !GetComponent<Collider>().bounds.Intersects(ballDetector.GetComponent<Collider>().bounds) && (velocity.x > 0.1 || velocity.y > 0.1 || velocity.z > 0.1) ) 
            return false;
        return true;
    }
}