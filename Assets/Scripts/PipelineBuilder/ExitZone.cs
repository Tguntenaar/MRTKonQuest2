using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class gets called in Manipulation Event Listener
There is only one exit zone at the time while dragging. On release it gets destroyed
*/
public class ExitZone : MonoBehaviour
{

    public Collider GetCollidedWithExitzone() => collidedWithExitzone;
    private Collider collidedWithExitzone;

    void OnTriggerEnter(Collider other)
    {
        collidedWithExitzone = other;
    }

    void OnTriggerExit(Collider other)
    {
        collidedWithExitzone = null;
    }

    public void DropBackInZone()
    {
        Debug.Log("DropBackInZone");

        if (collidedWithExitzone != null)
        {
            // The filter object tis the cube parent  which we want to place on the exit zone position 
            collidedWithExitzone.transform.parent.transform.position = transform.position;
        }
        // Remove exit zone
        Debug.Log("Destroy exitZone");
        Destroy(gameObject);

        // Remove from pipeline

    }
}
