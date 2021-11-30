using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class SpawnFilterBlocks : MonoBehaviour
{
    // https://stackoverflow.com/questions/49003811/unity-ui-onclick-inspector-with-enumeration
    public GameObject filterPrefab;
    public GameObject pipeline;
    private bool instantiateInWorldSpace = false;


    // Start is called before the first frame update
    void Start()
    {
        if (filterPrefab == null)
        {
            Debug.Log("filterPrefab not set");
        }
    }

    public void SpawnObject()
    {
        GameObject spawned = Instantiate(filterPrefab, pipeline.transform, instantiateInWorldSpace);
        ObjectManipulator om = spawned.GetComponent<ObjectManipulator>();

        Debug.Log("Spawned");
    }

    private void HandleOnManipulationEnded(ManipulationEventData eventdata)
    {
        Debug.Log("This works");
    }




    // private enum Filter
    // {
    //     ContourFilter,
    //     RangeFilter,
    //     Integer
    // }
    // [SerializeReference] private Filter state;
    // public void SpawnObjectEnum(SpawnFilterBlocks currState)
    // {

    //     switch (currState.state)
    //     {
    //         case Filter.ContourFilter:
    //             Instantiate(contourfilterPrefab); // TODO: next to the hand menu
    //             break;
    //         case Filter.RangeFilter:
    //             break;
    //         case Filter.Integer:
    //             break;
    //         default:
    //             Debug.Log("Add Filter to Spawn FilterBlock Script");
    //             break;
    //     }
    // }
}
