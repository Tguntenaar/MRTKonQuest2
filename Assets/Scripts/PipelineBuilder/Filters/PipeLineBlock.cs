using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;


public class PipeLineBlock : MonoBehaviour
{

    public GameObject exitZonePrefab;

    HideShowMenu hsm;

    void Start()
    {
        hsm = transform.GetComponentInChildren<HideShowMenu>();
    }

    /* This function is not used anymore */
    void HandleOnManipulationEnded(ManipulationEventData eventData)
    {
        // Search Dropzone
        PipelineController pc = transform.parent.GetComponentInChildren<PipelineController>();

        pc.DropInZone(eventData.ManipulationSource);
    }

    /*

    // TODO:
    // In plaats van RGBAScript + switch case wil
    // je alleen een GetComponent<FilterScript>
    // Dit filter script heeft elk filter (ManipulationEvent? -> FilterScript)
    // Dit filter script bevat variable TargetData object voor elk Filter
    // Andere Specifieke Filters Script kunne dit script Extenden

    // Als je meer dan een filter werkend wilt hebben moet je die aanpassen.

    */
    public void RegisterTargetData(GameObject targetData)
    {
        Debug.Log("RegisterTargetData");
        // If menu disabled can't find its components
        ShowMenu();

        switch (transform.name)
        {
            case "RGBAFilter":
                RGBAFilter rgba = transform.GetComponentInChildren<RGBAFilter>();
                Debug.Log(rgba.name);
                Debug.Log(transform.name);
                Debug.Log(rgba.TargetRenderer.transform.name);
                // null reference when Menu is disabled
                rgba.TargetRenderer.transform.gameObject.SetActive(false);
                // set to cylinder
                rgba.TargetRenderer = targetData.GetComponent<Renderer>();
                break;
            case "CylinderPipelineObject":
                Debug.Log(transform.name);
                AdjustCylinder ac = transform.GetComponentInChildren<AdjustCylinder>();
                Debug.Log("cylinder assigned");
                ac.cylinder = targetData;
                break;
            default:
                Debug.Log("RegisteredDefault");

                // FIXME:
                // BaseFilter filter = transform.GetComponentInChildren<BaseFilter>();
                // filter.TargetObject = targetData;
                break;
        }
    }

    public void SnapIntoPosition(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        // transform.localScale = 
    }

    public void HideMenu()
    {
        Debug.Log("HideMenu " + transform.name);
        hsm.HideMenu();
    }

    public void ShowMenu()
    {
        hsm.ShowMenu();
    }

    // Gets called when data object is placed in Filterbox;
    public void GenerateMenu()
    {
        ExtendedProperties ep = transform.GetComponent<ExtendedProperties>();
        for (int i = 0; i < ep.properties.Length; i++)
        {
            // Instantiate(PinchSliderPrefab); op de juiste positie etc
        }
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
