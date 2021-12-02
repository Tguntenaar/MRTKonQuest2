using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class InputScript : MonoBehaviour
{

    public GameObject dataObject;

    // Start is called before the first frame update
    void Start()
    {
        if (dataObject != null)
        {
            GameObject cylinder = Instantiate(dataObject);

            PipelineController p = transform.parent.GetComponentInChildren<PipelineController>();
            p.RegisterObject(cylinder);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
