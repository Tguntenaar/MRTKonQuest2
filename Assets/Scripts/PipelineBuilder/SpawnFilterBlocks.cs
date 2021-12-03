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
        // public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
        // GameObject spawned = Instantiate(filterPrefab, pipeline.transform, instantiateInWorldSpace);
        // position is to 10cm to the right of the handmenu / rotation is the same
        Transform parentTransform = transform.parent.transform.parent.transform.parent.transform;
        parentTransform = pipeline.transform;
        Debug.Log(parentTransform.name);
        Vector3 position = parentTransform.position + new Vector3(0.1f, 0.15f, 0);
        Quaternion rotation = parentTransform.rotation;

        GameObject spawned = Instantiate(filterPrefab, position, rotation, pipeline.transform);
        spawned.transform.parent = parentTransform.parent;

        // ObjectManipulator om = spawned.GetComponent<ObjectManipulator>();

        Debug.Log("Spawned");
    }

    private void HandleOnManipulationEnded(ManipulationEventData eventdata)
    {
        Debug.Log("This works");
    }
}
