using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour
{

    Collider collidedWithExitzone;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        collidedWithExitzone = other;
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");

        collidedWithExitzone = null;
    }

    public void DropBackInZone()
    {
        Debug.Log("DropBackInZone");

        if (collidedWithExitzone != null)
        {
            // The filter objec tis the cube parent  which we want to place on the exit zone position 
            collidedWithExitzone.transform.parent.transform.position = transform.position;
        }

        Debug.Log("Destroy exitZone");
        Destroy(gameObject);
    }
}
