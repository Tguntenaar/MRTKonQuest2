using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class HandMenuSwitchProperty : MonoBehaviour
{

    public GameObject[] propertyPrefabs;

    public enum ManipulatorTypes
    {
        IntegerFloat = 0,
        Boolean = 1,
        Range = 2,
        ContourFilter = 3,
        FileBrowser = 4,
        RGBA = 5
    }

    // Start is called before the first frame update
    void Start()
    {
        HideAll();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPressed()
    {
        Debug.Log("Pressed");
    }

    public void HideShowManipulator(int index)
    {
        var active = propertyPrefabs[(int)index].activeSelf;
        HideAll();
        propertyPrefabs[(int)index].SetActive(!active);
    }

    public void HideAll()
    {
        for (int i = 0; i < propertyPrefabs.Length; i += 1)
        {
            propertyPrefabs[i].SetActive(false);
        }
    }

  
    void SwitchButtonHandler(int index)
    {
        // Here I want to know which button
        //.GetComponent<Interactable>().onClick.AddListener(delegate { SwitchButtonHandler(0); });
        Debug.Log(index);
    }
}
