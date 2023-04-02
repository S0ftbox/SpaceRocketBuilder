using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Velocity : MonoBehaviour
{
    public Text velocity;
    public GameObject rocket;
    float velocityVal;
    Vector3 previousLocation;

    void Start()
    {
        previousLocation = rocket.transform.position;
    }

    void FixedUpdate()
    {
        velocityVal = (((rocket.transform.position - previousLocation).magnitude) / Time.fixedDeltaTime) * 100;
        velocity.text = velocityVal.ToString("n1") + " m/s";
        previousLocation = rocket.transform.position;
    }
}
