using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class InputScript : MonoBehaviour
{

    public GameObject dataObject;
    public GameObject[] dataObjects;

    private PipelineController pipeline;

    // Start is called before the first frame update
    void Start()
    {
        pipeline = transform.parent.GetComponentInChildren<PipelineController>();
        if (dataObject != null)
        {
            GameObject cylinder = Instantiate(dataObject);
            pipeline.RegisterObject(cylinder);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
