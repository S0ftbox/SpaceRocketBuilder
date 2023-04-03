using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int scrollScale = 5;
    public int rotateScale = 10;

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            transform.position = transform.position + new Vector3(0, Input.GetAxis("Mouse ScrollWheel") * scrollScale, 0);
        }
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(Input.GetAxis("Mouse Y") * -rotateScale, Input.GetAxis("Mouse X") * rotateScale, 0);
        }

    }
}
