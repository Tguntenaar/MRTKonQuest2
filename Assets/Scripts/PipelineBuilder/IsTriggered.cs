using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

/**
Zwarte pC
*/
public class IsTriggered : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //cubeManipulator.ForceEndManipulation();
        if (other.gameObject.CompareTag("FilterBox"))
        {
            ObjectManipulator cubeManipulator = other.gameObject.GetComponent<ObjectManipulator>();
            cubeManipulator.enabled = !cubeManipulator.enabled;
        }

        Debug.Log("istriggered");
        //public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta);

        //Vector3.MoveTowards(transform.position, Vector3.right * Time.deltaTime, 100);
        //other.gameObject.transform.position = transform.position + Vector3.right;
        //other.gameObject.transform.position = new Vector3(0.160300002f, -0.60799998f, 0f);
        //TransformVector(transform.position + Vector3.right * 2);
    }
}
