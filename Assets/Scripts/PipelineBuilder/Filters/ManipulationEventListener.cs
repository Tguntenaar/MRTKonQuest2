using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;


public class ManipulationEventListener : MonoBehaviour
{

    public GameObject exitZonePrefab;

    // Start is called before the first frame update
    void Start()
    {
        // OnManipulationEnded.AddListener(HandleOnManipulationEnded); Listener doesnt always work
    }

    /* This function is not used anymore */
    void HandleOnManipulationEnded(ManipulationEventData eventData)
    {
        // Search Dropzone
        PipelineController dz = transform.parent.GetComponentInChildren<PipelineController>();

        dz.DropInZone(eventData.ManipulationSource);
    }

    // This function is set on
    public void DropInitiatedPrefabInZone(ManipulationEventData eventData)
    {
        // Search Dropzone
        PipelineController dz = transform.parent.GetComponentInChildren<PipelineController>();
        // Check for dropzone
        if (dz != null)
        {
            dz.DropInZone(eventData.ManipulationSource);
        }
    }

    public void DropInitiatedPrefabBackInExitZone(ManipulationEventData eventData)
    {
        // There is always just one exitZone
        ExitZone ez = transform.parent.GetComponentInChildren<ExitZone>();
        // ez is null when not in the pipeline yet.
        if (ez != null)
        {
            ez.DropBackInZone();
        }
    }

    /* Function called on manipulation start. It checks wether the given filter is already in the pipeline. Than it Instantiates the exit zone on a specific position. It is Destroyed when dropped out of zone. */
    public void CheckIfAlreadyInPipeline()
    {
        // Get pipeline
        PipelineController pc = transform.parent.GetComponentInChildren<PipelineController>();
        List<GameObject> pipeline = pc.GetPipeline();
        // Check if in pipeline
        if (pipeline.Contains(gameObject))
        {
            // Debug.Log("Already in pipe");
            // Instantiate exitzone on place
            GameObject go = Instantiate(exitZonePrefab, transform.position, transform.rotation, transform.parent);
        }
    }
}
