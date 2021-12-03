using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class PipelineBlockCollision : MonoBehaviour
{
    private int pipelineLength = 1;

    private GameObject[] pipeline;

    public GameObject InputBox;

    public Vector3 startPosition = new Vector3(1f, -0.5f, -0.7f);
    public Vector3 firstFilterPosition = new Vector3(1f, -0.5f, -0.87f);

    public GameObject startBlock;

    public GameObject filterPrefab;

    void Start()
    {
        // TODO add input box to pipeline
        // Then spawn filter after filter with push on first button
    }

    public void SpawnNewFilterAtEnd()
    {
        Instantiate(filterPrefab, FindPosition(), startBlock.transform.rotation);
        pipelineLength += 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided!");
        if (collision.gameObject.CompareTag("SnapObject"))
        {
            // Remove MRTK components that make it grabbable
            //CursorContextObjectManipulator a = collision.gameObject.GetComponent<CursorContextObjectManipulator>();
            //a.enabled = !a.enabled;
            ObjectManipulator a = collision.gameObject.GetComponent("ObjectManiplutor") as ObjectManipulator;
            //Debug.Log(collision.gameObject.name);
            //Debug.Log(a);
            //a.enabled = !a.enabled;

            // Move to position
            collision.rigidbody.MovePosition(FindPosition());
            pipelineLength += 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Other option
        Debug.Log("OnTriggerEnter!");
        if (other.gameObject.CompareTag("SnapObject"))
        {
            // Remove MRTK components that make it grabbable
            //CursorContextObjectManipulator a = collision.gameObject.GetComponent<CursorContextObjectManipulator>();
            //a.enabled = !a.enabled;

            //ObjectManipulator a = other.gameObject.GetComponent("ObjectManiplutor") as ObjectManipulator;
            //a.enabled = !a.enabled;

            // Move to position
            // other.rigidbody.MovePosition(FindPosition());
            pipelineLength += 1;
        }
    }


    void Snap()
    {
        // Do Something
        pipelineLength += 1;
    }

    Vector3 FindPosition()
    {
        Vector3 increment = startPosition - firstFilterPosition;
        // fix direction Vector3.back equals Vector3(0,0,-1)
        Vector3 thePosition = transform.TransformPoint(Vector3.back * pipelineLength * 2);
        return thePosition;
    }
}

