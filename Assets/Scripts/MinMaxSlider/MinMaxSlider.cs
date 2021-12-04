using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;
using System;

public class MinMaxSlider : MonoBehaviour
{

    [SerializeField]
    private TextMeshPro textMeshMin = null;

    [SerializeField]
    private TextMeshPro textMeshMax = null;

    [SerializeField]
    private TextMeshPro textMeshMagnitude = null;

    public PinchSlider pinch1;
    public PinchSlider pinch2;

    [HideInInspector]
    public float Max;
    [HideInInspector]
    public float Min;

    public PinchSlider magnitudeSlider;

    private int magnifier = 100;

    void Awake()
    {
        // PinchSlider[] sliders = transform.GetComponentsInChildren<PinchSlider>();
        // if (pinch1 == null || pinch2 == null)
        // {
        //     pinch1 = sliders[0];
        //     pinch2 = sliders[1];
        // }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (magnitudeSlider)
        {
            magnitudeSlider.SliderValue = 0.1f;
        }
        // PinchSlider[] sliders = transform.GetComponentsInChildren<PinchSlider>();
        // if (pinch1 == null || pinch2 == null)
        // {
        //     pinch1 = sliders[0];
        //     pinch2 = sliders[1];
        // }
    }

    public void UpdateUI(float pinch1, float pinch2)
    {
        if (textMeshMin == null)
        {
            textMeshMin = GetComponent<TextMeshPro>();
        }

        if (textMeshMax == null)
        {
            textMeshMax = GetComponent<TextMeshPro>();
        }


        if (textMeshMin != null && textMeshMax != null)
        {
            int magnitude = GetMagnitude();
            textMeshMin.text = $"{pinch1 * magnitude:F2}";
            textMeshMax.text = $"{pinch2 * magnitude:F2}";

        }
    }

    public void OnSliderUpdated(SliderEventData eventData)
    {
        // Debug.Log("OnSliderUpdated");
        if (pinch1.SliderValue < pinch2.SliderValue)
        {
            Min = pinch1.SliderValue;
            Max = pinch2.SliderValue;
            UpdateUI(pinch1.SliderValue, pinch2.SliderValue);
        }
        else
        {
            Min = pinch2.SliderValue;
            Max = pinch1.SliderValue;
            UpdateUI(pinch2.SliderValue, pinch1.SliderValue);
        }
    }

    public void UpdateMagnitudeUI(SliderEventData eventData)
    {
        textMeshMagnitude.text = $"Magnitude: {GetMagnitude()}";
        UpdateUI(pinch1.SliderValue, pinch2.SliderValue);
    }

    int GetMagnitude()
    {
        if (magnitudeSlider != null && textMeshMagnitude != null)
        {
            return (int)Math.Round(magnitudeSlider.SliderValue * magnifier);
        }
        return 1;
    }

}
