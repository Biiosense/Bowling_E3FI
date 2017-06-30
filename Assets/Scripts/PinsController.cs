using UnityEngine;
using System.Collections;
using System;

public class PinsController : MonoBehaviour
{
    GameObject[] pins;
    GameObject detector;


    void Start()
    {
        detector = GameObject.FindGameObjectWithTag("PinDetector");
        pins = GameObject.FindGameObjectsWithTag("Pin");
        foreach (GameObject pin in pins)
            pin.gameObject.SetActive(false);
    }

    public void Reset()
    {
        foreach (GameObject pin in pins)
        {
            pin.gameObject.SetActive(true);
            pin.GetComponent<ResetPosition>().Reset();
        }
        GetComponent<Animator>().Play("quilleAnimation");

    }

    public bool AllDown()
    {
        if (pins.Length - KnockedOut() > 0)
            return false;
        return true;
    }

    public void RemoveKnockedOut()
    {
        foreach (GameObject pin in pins)
            if (pin.gameObject.activeSelf && !pin.GetComponent<Collider>().bounds.Intersects(detector.GetComponent<Collider>().bounds))
                pin.gameObject.SetActive(false);
    }

    public int KnockedOut()
    {
        int cnt = 0;
        foreach (GameObject pin in pins)
            if (pin.gameObject.activeSelf && !pin.GetComponent<Collider>().bounds.Intersects(detector.GetComponent<Collider>().bounds))
                cnt++;
        return cnt;
    }

    public bool HasDone()
    {
        foreach (GameObject pin in pins)
        {
            Vector3 velocity = pin.GetComponent<Rigidbody>().velocity;
            // Si une quille est presque arrêtée
            if ((Math.Abs(velocity.x) <= 0.1 && Math.Abs(velocity.y) <= 0.1 && Math.Abs(velocity.z) <= 0.1))
            {
                velocity = Vector3.zero;              // On l'arrête
                //  Et si en plus elle n'est pas encore tombée
                if (pin.GetComponent<Collider>().bounds.Intersects(detector.GetComponent<Collider>().bounds))  
                    pin.GetComponent<Rigidbody>().rotation = Quaternion.Euler(-90, 0, 0);     // on l'a remet droite

            }
            if (pin.GetComponent<Renderer>().isVisible && (Math.Abs(velocity.x) > 0.01 && Math.Abs(velocity.y) > 0.01 && Math.Abs(velocity.z) > 0.01) && pin.transform.position.y > -10)
                return false;

        }
        return true;
    }
}
