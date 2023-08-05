using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaledCameraController : MonoBehaviour
{
    public GameObject mainCam;
    Vector3 oldPostition;


    // Update is called once per frame
    void Update()
    {
        Vector3 relativePosition = mainCam.transform.InverseTransformPoint(transform.position);
        Vector3 newScale = mainCam.transform.localScale / 30;
        transform.position = mainCam.transform.TransformPoint(relativePosition);

        transform.rotation = mainCam.transform.rotation;
        transform.localScale = newScale;
    }
}
