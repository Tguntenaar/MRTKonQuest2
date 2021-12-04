using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;

public class ContourMenu : MonoBehaviour
{

    public MinMaxSlider minMaxSlider;
    public GridObjectCollection gridObjectCollection;
    public GameObject EntryPrefab;
    public GameObject SelectedEntry;
    public List<GameObject> collection = new List<GameObject>();
    public TextMeshPro tMPro;

    // Start is called before the first frame update
    void Start()
    {
        if (minMaxSlider == null)
        {
            minMaxSlider = transform.GetComponentInChildren<MinMaxSlider>();
        }

        if (gridObjectCollection == null)
        {
            gridObjectCollection = transform.GetComponentInChildren<GridObjectCollection>();
        }

        if (collection.Count == 0)
        {
            collection.Add(new GameObject("test"));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }



    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    float CalculateSteps()
    {
        float delta = minMaxSlider.Max - minMaxSlider.Min;
        // float delta2 = 2500 - 100;
        delta = Remap(delta, 0, 1, 100, 2500);
        Debug.Log(delta);
        var oneStep = delta / collection.Count;
        return oneStep;
    }

    public void TestShowStep()
    {
        tMPro.text = CalculateSteps().ToString("F0");
    }

    // On Button Click
    /*
     FIELDS
        Colliders
        Name
        Offset
        Radius
        Transform
    */
    void AddEntry()
    {
        // Instatiate
        GameObject newEntry = Instantiate(EntryPrefab, gridObjectCollection.transform);

        // Create new node
        // ObjectCollectionNode ocn = new ObjectCollectionNode();
        // ocn.Name = newEntry.name;
        // ocn.Transform = newEntry.transform;

        // Add to NodeList
        // gridObjectCollection.NodeListReadOnly

        collection.Add(newEntry);

        gridObjectCollection.UpdateCollection();

    }

    // OnButton Click
    void RemoveEntry()
    {
        if (SelectedEntry != null)
        {
            Destroy(SelectedEntry);
            collection.Remove(SelectedEntry);
        }
    }
}
