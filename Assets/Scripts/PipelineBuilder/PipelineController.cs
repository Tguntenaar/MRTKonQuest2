using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

/**
TODO: Add VTKWorkbench files from project OriginalCode to VTKWORKBench Folder in this project
*/
public class PipelineController : MonoBehaviour
{
    public Material m1;
    public Material m2;

    private Collider collidedWithDropzone;

    public List<GameObject> GetPipeline() => pipeline;

    private float pipelineScale = 0.1f;
    private List<GameObject> pipeline = new List<GameObject>();

    [HideInInspector]
    public GameObject targetData;

    [HideInInspector]
    public List<GameObject> targetObjects = new List<GameObject>();

    // To keep track of dropzone
    Vector3 lastPositionDropZone;
    // Start is called before the first frame update
    void Start()
    {
        lastPositionDropZone = transform.localPosition;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEnter");
        if (other.CompareTag("SnapObject"))
        {
            GetComponent<Renderer>().material = m1;
            collidedWithDropzone = other;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SnapObject"))
        {
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
            if (!pipeline.Contains(filter))
            {
                pipeline.Add(filter);
                SnapObjectIntoPosition(collidedWithDropzone);
                MoveDropZoneToRight(1);
                HideAllMenusInPipeline();

                ///////////////////////////////////////////////////
                // TEMP for cylinder input v2
                if (filter.GetComponent<ExtendedProperties>() != null)
                {
                    // Dataobject block put into pipeline
                    filter.GetComponent<PipelineController>().AddObjectToRegistry(filter);
                }
                else
                {
                    // Single Object in input
                    filter.GetComponent<PipeLineBlock>().RegisterTargetData(targetData);
                }
                ///////////////////////////////////////////////////


                // DropZone Position After Adding to Dropzone
                MoveFilterToPosition(pipeline.Count, transform);
            }
            else
            {
                // move filter to the end
                pipeline.Remove(filter);
                pipeline.Add(filter);
                // rearrange pipeline in UI
                PipelineToUI();
            }
        }
        else
        {
            // get exitzone
            ExitZone ez = transform.parent.GetComponentInChildren<ExitZone>();
            // check if left exitzone
            if (pipeline.Contains(filter) && ez.GetCollidedWithExitzone() == null)
            {
                // remove filter from pipeline
                pipeline.Remove(filter);
                // rearrange pipeline in UI
                PipelineToUI();
            }
        }

        // HideAllMenusInPipeline();
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
            MoveFilterToPosition(i, pipeline[i].transform);
        }

        // DropZone Position
        MoveFilterToPosition(pipeline.Count, transform);
    }

    void SnapObjectIntoPosition(Collider collider)
    {
        // PipeLineBlock pb = collider.transform.parent.GetComponentInChildren<PipeLineBlock>();
        // pb.SnapIntoPosition(transform.position, transform.rotation);
        InputScript iscript = transform.parent.GetComponentInChildren<InputScript>();
        // From Cube To FilterBlock Note: Every Filter / Object Block needs a child with boxcollider with Tag SnapObject
        collider.transform.parent.transform.position = transform.position;
        collider.transform.parent.transform.rotation = transform.rotation;
        collider.transform.parent.transform.localScale = new Vector3(1, 1, 1);
    }

    public void MoveDropZoneToRight(int amount)
    {
        // Dropzone                                     
        transform.position = transform.position + transform.right * pipelineScale * amount;
    }

    public void MoveFilterToPosition(int amount, Transform t)
    {
        t.localPosition = lastPositionDropZone + Vector3.right * pipelineScale * amount;
        t.rotation = transform.rotation;
    }

    // translate / animate
    void ShiftFiltersToLeft(int amount, Transform t)
    {
        t.localPosition = lastPositionDropZone;
        t.Translate(lastPositionDropZone + t.right * pipelineScale * (amount - 1));
    }


    public void HideAllMenusInPipeline()
    {
        for (int i = 0; i < pipeline.Count; i++)
        {
            pipeline[i].GetComponent<PipeLineBlock>().HideMenu();

            // HideShowMenu menu = pipeline[i].GetComponentInChildren<HideShowMenu>();
            // menu.HideMenu();
        }

    }

    public void RegisterObject(GameObject g)
    {
        targetData = g;
        // Of
        // Dictionary<GameObject> voeg hierin toe
        // Of
        // List<GameObject> Pipeline
    }

    public void AddObjectToRegistry(GameObject g)
    {
        targetObjects.Add(g);
    }
}