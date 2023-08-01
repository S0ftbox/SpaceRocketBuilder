using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    Transform rocket;
    public GameObject planet;
    public float rotationSpeed;
    Vector3 direction;
    Quaternion rotation;

    void Start()
    {
        rocket = GameObject.Find("Rocket").transform.GetChild(0);
    }

    void Update()
    {
        transform.position = rocket.position;
        direction = (planet.transform.position - transform.position).normalized;
        rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
}
