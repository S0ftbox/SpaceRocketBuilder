using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float scrollScale = 5;
    public int rotateScale = 10;
    public float minVertAngle = -80f;
    public float maxVertAngle = 80f;

    public Vector3 offset;
    float targetPos;
    public float minOffset = -12f;
    public float maxOffset = -2f;
    float xRotation = 0f;
    float yRotation = 0f;
    bool isRotating = false;
    Vector3 lastMousePos;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if(SceneManager.GetActiveScene().name != "FlightMode")
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                offset.z += Input.GetAxis("Mouse ScrollWheel") * scrollScale;
                offset.z = Mathf.Clamp(offset.z, minOffset, maxOffset);
            }
            else
            {
                target.position = target.position + new Vector3(0, Input.GetAxis("Mouse ScrollWheel") * scrollScale, 0);
            }
        }
        else
        {
            offset.z += Input.GetAxis("Mouse ScrollWheel") * scrollScale;
            offset.z = Mathf.Clamp(offset.z, minOffset, maxOffset);
        }

        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
            lastMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            Vector3 mouseDelta = Input.mousePosition - lastMousePos;
            lastMousePos = Input.mousePosition;

            xRotation -= mouseDelta.y * rotateScale * Time.deltaTime;
            yRotation += mouseDelta.x * rotateScale * Time.deltaTime;

            xRotation = Mathf.Clamp(xRotation, minVertAngle, maxVertAngle);
        }
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        transform.position = target.position + rotation * offset;
        transform.rotation = rotation;
    }
}
