using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

/**
Zwarte pC
*/
public class PipelineController : MonoBehaviour
{
    public Material m1;
    public Material m2;

    private Collider collidedWithDropzone;

    public List<GameObject> GetPipeline() => pipeline;

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
        GameObject filter = collidedWithDropzone.transform.parent.gameObject;

        Debug.Log("Drop in zone called");
        if (collidedWithDropzone != null)
        {
            // CHECK IF ALREADY IN PIPELINE
            if (!pipeline.Contains(filter))
            {
                pipeline.Add(filter);
                SnapObjectIntoPosition(collidedWithDropzone);
                MoveDropZoneToRight(1);
            }
            else
            {
                // move filter to the end
                pipeline.Remove(filter);
                pipeline.Add(filter);
                // FIXME: re arrange pipeline
            }
        }
        else
        {
            // remove filter from dropzone
            if (pipeline.Contains(filter))
            {
                pipeline.Remove(filter);
                // FIXME: rearrange everything
            }
        }
    }

    void ShiftFiltersToLeft()
    {

    }

    void SnapObjectIntoPosition(Collider collider)
    {
        // Debug.Log(collider.name);
        collider.transform.parent.transform.position = transform.position;
        collider.transform.parent.transform.rotation = transform.rotation;
        // scale disabled on pipeline and filterblocks
        // collider.transform.parent.transform.localScale 
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
}
