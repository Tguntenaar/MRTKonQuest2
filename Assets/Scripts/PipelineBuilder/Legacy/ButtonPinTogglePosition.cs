using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPinTogglePosition : MonoBehaviour
{
    public Vector3 orignalposition;
    public Quaternion orignalrotation;
    private bool followmode;
    // Start is called before the first frame update
    void Start()
    {
        followmode = false;
        if (orignalposition == null)
        {
            Debug.Log("Original position of near menu not set on filter");
        }
    }


    public void PlaceAboveFilterBox()
    {
        if (followmode)
        {
            // position on original position
            Debug.Log("followmode: " + transform.parent.name);
            transform.parent.transform.localPosition = orignalposition;
            // transform.parent.transform. = orignalscale;
            transform.parent.transform.localRotation = orignalrotation;
        }
        else
        {
            // do nothing
            followmode = true;
        }

    }
}
