using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/**
Zwarte pC
*/
public class HideShowMenu : MonoBehaviour
{

    public GameObject menu;

    //new 
    Vector3 originalPosition;
    Quaternion originalRotation;
    Vector3 originalScale;

    private bool followmode;


    // Start is called before the first frame update
    void Start()
    {
        if (menu == null)
        {
            Debug.Log("No Menu setup for script HideShowMenu");
        }

        followmode = false;
        // originalTransform = menu.transform;
        originalPosition = menu.transform.localPosition;
        originalRotation = menu.transform.localRotation;
        originalScale = menu.transform.localScale;
    }

    public void ToggleHideShowMenu() => menu.SetActive(!menu.activeSelf);
    public void HideMenu() => menu.SetActive(false);

    public void PlaceAboveFilterBox()
    {
        if (followmode)
        {
            // position on original position
            transform.parent.transform.localPosition = originalPosition;
            transform.parent.transform.localRotation = originalRotation;
            transform.parent.transform.localScale = originalScale;
        }
        else
        {
            // do nothing
            followmode = true;
        }

    }
}
