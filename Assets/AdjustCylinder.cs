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
        return heightSlider.SliderValue * 10 + 0.1f;
    }

    float GetRadius()
    {
        return radiusSlider.SliderValue * 10 + 0.1f;
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
