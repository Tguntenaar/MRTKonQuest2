using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEntry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Select()
    {
        Debug.Log("Selected!");
        ContourMenu contourMenu = transform.GetComponentInParent<ContourMenu>();
        contourMenu.SelectedEntry = transform.gameObject;
    }
}
