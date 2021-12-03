using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;

public class ContourFilterButtonEvents : MonoBehaviour
{
    public GameObject[] controls;
    public GameObject[] buttons;

    public PinchSlider p1;
    public PinchSlider p2;

    public TextMeshPro[] meshes;


    // https://docs.microsoft.com/en-us/windows/mixed-reality/mrtk-unity/features/ux-building-blocks/interactable?view=mrtkunity-2021-05#events
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started contour filterbutton events");
        // Debug.Log(controls.Length);
        for (int i = 0; i < controls.Length; i++)
        {
            //controls[i].SetActive(false);
        }
        // array.onClick.AddListener(delegate { SwitchButtonHandler(0)});
    }

    void SwitchButtonHandler(int idx_)
    {
        // Here I want to know which button 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonOne()
    {
        Debug.Log("Button1");
        Debug.Log(controls[0].activeSelf);
        controls[0].SetActive(!controls[0].activeSelf);
    }

    public void ButtonTwo()
    {
        Debug.Log("Button2");
        controls[1].SetActive(!controls[1].activeSelf);
    }

    public void Test(Event e)
    {
        Debug.Log(e.ToString());
        Debug.Log(e);
    }

    public void ChangedSliders()
    {
        float min = 0;
        float max = 0;
        float amplifier = 600;

        if (p1.SliderValue < p2.SliderValue)
        {
            min = p1.SliderValue;
            max = p2.SliderValue;
        }
        else
        {
            min = p2.SliderValue;
            max = p1.SliderValue;
        }

        float range = max - min;
        float steps = range / meshes.Length;

        for (int i = 0; i < meshes.Length; i++)
        {
            float answer = min + i * steps;
            meshes[i].text = $"{ answer * amplifier:F2}";
        }

    }
}
