using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class RGBAScript : MonoBehaviour
{
    public PinchSlider Red;
    public PinchSlider Green;
    public PinchSlider Blue;
    public PinchSlider Alpha;

    public Renderer TargetRenderer;


    // Start is called before the first frame update
    void Start()
    {
        if (Red == null || Blue == null || Green == null || Alpha == null || TargetRenderer == null)
        {
            Debug.LogError("Something wrong with the RGBA setup. - RGBAscript");
        }
        else
        {
            Alpha.SliderValue = 1;
        }

    }

    // Update is called once per frame
    public void OnEdit()
    {
        TargetRenderer.material.color = new Color(Red.SliderValue, Green.SliderValue, Blue.SliderValue, Alpha.SliderValue);
    }


    public void OnSliderUpdatedRed(SliderEventData eventData)
    {
        Debug.Log("OnSliderUpdatedRed");\
        // FIXME:
        // TargetRenderer = GetComponentInChildren<Renderer>();
        // TargetRenderer = GameObject.Find("TargetRenderCube").GetComponent<Renderer>();
        if ((TargetRenderer != null) && (TargetRenderer.material != null))
        {
            TargetRenderer.material.color = new Color(eventData.NewValue, TargetRenderer.sharedMaterial.color.g, TargetRenderer.sharedMaterial.color.b, TargetRenderer.sharedMaterial.color.a);
        }
    }

    public void OnSliderUpdatedGreen(SliderEventData eventData)
    {
        // TargetRenderer = GetComponentInChildren<Renderer>();
        if ((TargetRenderer != null) && (TargetRenderer.material != null))
        {
            TargetRenderer.material.color = new Color(TargetRenderer.sharedMaterial.color.r, eventData.NewValue, TargetRenderer.sharedMaterial.color.b, TargetRenderer.sharedMaterial.color.a);
        }
    }

    public void OnSliderUpdateBlue(SliderEventData eventData)
    {
        // TargetRenderer = GetComponentInChildren<Renderer>();
        if ((TargetRenderer != null) && (TargetRenderer.material != null))
        {
            TargetRenderer.material.color = new Color(TargetRenderer.sharedMaterial.color.r, TargetRenderer.sharedMaterial.color.g, eventData.NewValue, TargetRenderer.sharedMaterial.color.a);
        }
    }

    public void OnSliderUpdateAlpha(SliderEventData eventData)
    {
        // TargetRenderer = GetComponentInChildren<Renderer>();
        if ((TargetRenderer != null) && (TargetRenderer.material != null))
        {
            TargetRenderer.material.color = new Color(TargetRenderer.sharedMaterial.color.r, TargetRenderer.sharedMaterial.color.g, TargetRenderer.sharedMaterial.color.b, eventData.NewValue);
        }
    }
}
