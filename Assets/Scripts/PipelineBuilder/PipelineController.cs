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

    private float pipelineScale = 0.1f;
    private List<GameObject> pipeline = new List<GameObject>();

    // To keep track of dropzone
    Vector3 lastPositionDropZone;
    // Start is called before the first frame update
    void Start()
    {
        lastPositionDropZone = transform.localPosition;
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

    public void DropInZone(GameObject filter)
    {
        Debug.Log("Drop in zone called");
        Debug.Log(filter.name);
        if (collidedWithDropzone != null)
        {
            // TODO: 
            // GameObject filter = collidedWithDropzone.transform.parent.gameObject;
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
                Debug.Log(" REMOVE AND ADD ");
                Debug.Log(pipeline.Count);
                // rearrange pipeline in UI
                PipelineToUI();
            }
        }
        else
        {
            // get exitzone
            ExitZone ez = transform.parent.GetComponentInChildren<ExitZone>();
            // remove filter from dropzone
            if (pipeline.Contains(filter) && ez.GetCollidedWithExitzone() == null) // TODO: check if left exitzone
            {
                pipeline.Remove(filter);
                // rearrange pipeline in UI
                PipelineToUI();
            }
        }

        /*
            Else doe niks hier en doe het in exit zone waar je dit dropzone object zoekt
            Of 
            haal hier de exit zone op maar kan je er twee spawnen met twee handen?
        */
    }

    // IEnumerator
    void PipelineToUI()
    {
        // Get location of Input
        Transform inputposition = transform.parent.GetChild(0);
        Transform dropZonePosition = transform.parent.GetChild(1);

        for (int i = 0; i < pipeline.Count; i++)
        {
            Debug.Log(pipeline[i].name);
            // yield return new WaitForSeconds(.5f);
            MoveFilterToPosition(i, pipeline[i].transform);
        }

        // DropZone Position
        MoveFilterToPosition(pipeline.Count, transform);
    }

    void ShiftFiltersToLeft(int amount, Transform t)
    {
        // TODO: translate / animate
        t.localPosition = lastPositionDropZone;
        t.Translate(lastPositionDropZone + t.right * pipelineScale * (amount - 1));
    }

    void SnapObjectIntoPosition(Collider collider)
    {
        collider.transform.parent.transform.position = transform.position;
        collider.transform.parent.transform.rotation = transform.rotation;
    }

    public void MoveDropZoneToRight(int amount)
    {
        // Dropzone                                     
        transform.position = transform.position + transform.right * pipelineScale * amount;
    }

    public void MoveFilterToPosition(int amount, Transform t)
    {
        // werkend wanneer de pipeline niet omdraait.
        // t.localPosition = lastPositionDropZone + t.right * pipelineScale * amount;

        t.localPosition = lastPositionDropZone + Vector3.right * pipelineScale * amount;
        t.rotation = transform.rotation;
    }

    public void HideAllMenusInPipeline()
    {
        for (int i = 0; i < pipeline.Count; i++)
        {
            HideMenu(pipeline[i]);
        }

    }

    void HideMenu(GameObject filter)
    {
        HideShowMenu menu = filter.GetComponentInChildren<HideShowMenu>();
        menu.HideMenu();
    }
}