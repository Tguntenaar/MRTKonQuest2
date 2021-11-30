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

    public PinchSlider s1;
    public PinchSlider s2;

    public PinchSlider magnitudeSlider;

    private int magnifier = 100;



    // Start is called before the first frame update
    void Start()
    {
        if (magnitudeSlider)
        {
            magnitudeSlider.SliderValue = 0.1f;
        }
    }

    public void UpdateUI(float s1, float s2)
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
            textMeshMin.text = $"{s1 * magnitude:F2}";
            textMeshMax.text = $"{s2 * magnitude:F2}";

        }
    }

    public void OnSliderUpdated(SliderEventData eventData)
    {
        // Debug.Log("OnSliderUpdated");
        if (s1.SliderValue < s2.SliderValue)
        {
            UpdateUI(s1.SliderValue, s2.SliderValue);
        }
        else
        {
            UpdateUI(s2.SliderValue, s1.SliderValue);
        }
    }

    public void UpdateMagnitudeUI(SliderEventData eventData)
    {
        textMeshMagnitude.text = $"Magnitude: {GetMagnitude()}";
        UpdateUI(s1.SliderValue, s2.SliderValue);
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
