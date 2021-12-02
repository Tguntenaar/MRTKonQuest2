using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using System;

public class IntegerManipulator : MonoBehaviour
{
    PinchSlider pinchSlider;
    TextMeshPro t;

    public TextMeshPro min;
    public TextMeshPro max;

    public int minValue = 0;
    public int maxValue = 10000;
    // TODO: default values or get setter en set on initiate.

    TextMeshPro[] texts;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<TextMeshPro>();
        pinchSlider = transform.parent.GetComponentInChildren<PinchSlider>();
        texts = transform.parent.GetComponentsInChildren<TextMeshPro>();

        min.text = $"Min: {minValue}";
        max.text = $"Max: {maxValue}";
    }

    // Update is called once per frame
    void Update()
    {

    }

    /* This gets called on slider change. */
    public void UpdateUI()
    {
        if (pinchSlider != null) // gets called once at wake probably because give a null ref exception only 1 time
        {
            int baseValue = minValue;
            int delta = maxValue - minValue;
            int total = baseValue + (int)Math.Round(delta * pinchSlider.SliderValue);
            t.text = $"Value: {total}";
        }
    }
}
