using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class floatParameterFilter : MonoBehaviour
{

    public TextMeshPro tmp;
    string value;
    float valueFloat;
    // TODO: edit font size when number gets very big

    public void Submit()
    {
        valueFloat = float.Parse(value);
    }

    public void Clear()
    {

        value = "";
        UpdateUI();
    }

    public void AddNumber(int number)
    {
        value += number.ToString();
        UpdateUI();
    }

    public void AddPoint()
    {
        if (!value.Contains(".") && value != "")
            value += ".";
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (value.Length < 15)
        {
            tmp.text = "type: " + value;
        }
        else
        {
            tmp.text = value;
        }
    }
}
