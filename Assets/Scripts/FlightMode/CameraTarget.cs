using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    Transform rocket;

    void Start()
    {
        rocket = GameObject.Find("Rocket").transform.GetChild(0);
    }

    void Update()
    {
        transform.position = rocket.position;
    }
}
