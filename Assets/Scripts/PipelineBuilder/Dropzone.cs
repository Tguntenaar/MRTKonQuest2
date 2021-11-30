using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

/**
Zwarte pC
*/
public class Dropzone : MonoBehaviour
{
    public Material m1;
    public Material m2;

    private Collider collidedWithDropzone;

    private List<GameObject> pipeline = new List<GameObject>();

    // To keep track of dropzone
    Vector3 lastPositionDropZone;
    // Start is called before the first frame update
    void Start()
    {
        lastPositionDropZone = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SnapObject"))
        {
            Debug.Log("Triggered");

            GetComponent<Renderer>().material = m1;
            collidedWithDropzone = other;
            // Add dropinZone here.
        }


    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SnapObject"))
        {
            Debug.Log("Exit");
            GetComponent<Renderer>().material = m2;
            collidedWithDropzone = null;
        }
    }

    public void DropInZone()
    {
        Debug.Log("Drop in zone called");
        if (collidedWithDropzone != null)
        {
            AddToPipeline(collidedWithDropzone);
            /*
            TODO:
                Create ExitZone
                On Manipulation Ended Check ook voor ExitZone stuff.
            */
            // create exit zone
            SnapObjectIntoPosition(collidedWithDropzone);
            MoveDropZoneToRight(1);
        }
    }

    public void AddToPipeline(Collider other)
    {
        // Collider == Cube
        // other.transform.parent.name == ...Filter
        pipeline.Add(other.gameObject);
    }

    void SnapObjectIntoPosition(Collider collider)
    {
        // Debug.Log(collider.name);
        collider.transform.parent.transform.position = transform.position;
        collider.transform.parent.transform.rotation = transform.rotation;
        // collider.transform.parent.transform.localScale = transform.lossyScale; scale disabled on pipeline and filterblocks
    }

    public void MoveDropZoneToRight(int amount)
    {
        transform.position = transform.position + transform.right * 0.1f * amount;
    }

    public void MoveDropZoneToIndex(int index)
    {
        Vector3 originalPos = new Vector3(0.1f, 0, 0);
        transform.position = (index + 1) * originalPos;
    }

    public void OnManipulationStarted(ManipulationEventData eventdata)
    {
        Debug.Log(" Manipulation started ");
        // if already in pipeline get index

    }

    public void OnManipulationEnded(ManipulationEventData eventdata)
    {
        Debug.Log(" Manipulation ended ");
    }
    // als eentje word gemanipuleerd die al in de pipeline zit gebruik exitzone
}
