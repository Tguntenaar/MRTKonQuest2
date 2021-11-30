using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class SnapZoneScript : MonoBehaviour
{

    public Material m1;
    public Material m2;

    private Collider c1;

    bool inDropZone = false;
    Vector3 lastPositionDropZone;

    // Start is called before the first frame update
    void Start()
    {
        lastPositionDropZone = transform.position;
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLISON");
        GetComponent<Renderer>().material = m1;
        c1 = other;
    }

    void OnTriggerExit(Collider Other)
    {
        Debug.Log("COLLISON EXT");
        GetComponent<Renderer>().material = m2;
        c1 = null;
    }

    /*
        Called when manipulation ended
    */
    public void PlaceInside()
    {
        Debug.Log("PlaceInside");
        Transform t;
        // Filters hebben meerder colliders. We willen dat de collider een snap object is 
        if (!c1.CompareTag("SnapObject"))
        {
            // get parent
            t = c1.transform.parent.transform;
        }
        else
        {
            t = c1.transform;
        }

        // Move cube to the center
        if (c1 != null)
        {

            t.position = transform.position;
            // Move DropZone
            transform.position = transform.position + transform.forward * -0.1f;
            inDropZone = true;
        }
    }

    public void CheckIfAlreadyInPipeline()
    {
        // if in pipeline
        if (inDropZone)
        {
            // move dropzone here
            inDropZone = false;

        }
        // else
        // nothing
    }


}