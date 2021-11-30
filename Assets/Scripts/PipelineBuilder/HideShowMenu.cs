using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/**
Zwarte pC
*/
public class HideShowMenu : MonoBehaviour
{

    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        if (menu == null)
        {
            Debug.Log("No Menu setup for script HideShowMenu");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleHideShowMenu() => menu.SetActive(!menu.activeSelf);
}
