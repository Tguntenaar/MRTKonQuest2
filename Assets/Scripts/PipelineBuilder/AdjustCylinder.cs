using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;


/*

NOTE: this script is not meant to me dynamic it only a prove of concept.

*/
public class AdjustCylinder : MonoBehaviour
{

    public PinchSlider heightSlider;
    public PinchSlider radiusSlider;

    public GameObject cylinder;


    float minScaleConstraint = 0.01f; // 10 keer zo klein
    float maxScaleConstraint = 0.5f; // 5 keer zo groot

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    float GetHeight()
    {
        float height = map(heightSlider.SliderValue, 0, 1, minScaleConstraint, maxScaleConstraint);
        return height;
    }

    float GetRadius()
    {
        float radius = map(radiusSlider.SliderValue, 0, 1, minScaleConstraint, maxScaleConstraint);
        return radius;
    }

    float map(float sliderValue, float sliderStart, float sliderEnd, float cylinderStart, float cylinderEnd)
    {
        return cylinderStart + (sliderValue - sliderStart) * (cylinderEnd - cylinderStart) / (sliderEnd - sliderStart);
    }

    public void AdjustHeight(SliderEventData data)
    {
        Debug.Log("Adjust Height");
        if (cylinder != null)
        {
            Vector3 v = cylinder.transform.localScale;
            cylinder.transform.localScale = new Vector3(v.x, GetHeight(), v.z);
        }
    }

    public void AdjustRadius(SliderEventData dat)
    {
        Debug.Log("Adjust Radius");
        if (cylinder != null)
        {
            Vector3 v = cylinder.transform.localScale;
            cylinder.transform.localScale = new Vector3(GetRadius(), v.y, GetRadius());
        }
    }
}
