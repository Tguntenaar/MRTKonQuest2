using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinner : MonoBehaviour
{
    public float RotationRateScale = 1.0f;
    public Vector3 RotationXYZ = new Vector3(1.0f, 2.0f, 3.0f);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float totalScale = Time.deltaTime * RotationRateScale;
        Vector3 v3 = new Vector3(RotationXYZ.x * totalScale, RotationXYZ.y * totalScale, RotationXYZ.z * totalScale);
        transform.Rotate(v3);
    }
}
