using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;


public class ManipulationEventListener : MonoBehaviour
// Microsoft.MixedReality.Toolkit.UI.ObjectManipulator
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
        Dropzone dz = transform.parent.GetComponentInChildren<Dropzone>();

        dz.DropInZone();
    }

    // This function is set on
    public void DropInitiatedPrefabInZone(ManipulationEventData eventData)
    {
        // Search Dropzone
        Dropzone dz = transform.parent.GetComponentInChildren<Dropzone>();
        // Check for dropzone
        if (dz != null)
        {
            dz.DropInZone();
        }
    }

    public void DropInitiatedPrefabBackInExitZone(ManipulationEventData eventData)
    {
        // TODO: get the right exit zone
        // of spawn exit zone pas als je moved
        ExitZone[] ez = transform.parent.GetComponentsInChildren<ExitZone>();
        Debug.Log("ManipulationEventListener DropBackin Extit zone");

        // ez[0].DropBackInZone(); not set
        ez[1].DropBackInZone();
    }

    /* Function called on manipulation start. It checks wether the given filter is already in the pipeline. Than it Instantiates the exit zone on a specific position. It is Destroyed when dropped out of zone. */
    public void CheckIfAlreadyInPipeline()
    {
        // Get pipeline

        // Check if in pipeline
        // Instantiate exitzone on place

        GameObject go = Instantiate(exitZonePrefab, transform.position, transform.rotation, transform.parent);
        // make it interact with exitzone
        Collider c = go.GetComponent<BoxCollider>();

        // Debug.Log("Collider" + c.isTrigger);
        //
        // go.GetComponent<ExitZone>().SetCollidedWithExitZone(c);
        // else
        // nothing
    }
}
