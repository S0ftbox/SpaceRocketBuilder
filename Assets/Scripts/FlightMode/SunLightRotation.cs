using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunLightRotation : MonoBehaviour
{
    Transform rocket;
    public Transform sun;
    Vector3 direction;
    Quaternion rotation;
    public float lightDistance = 5000;

    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket").transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        direction = (rocket.position - sun.position).normalized;
        rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        float distanceToSun = Vector3.Distance(rocket.position, sun.position);
        if(distanceToSun <= lightDistance)
        {
            transform.position = sun.position;
        }
        else
        {
            Vector3 directionToRocket = (rocket.position - transform.position).normalized;
            transform.position = rocket.position + directionToRocket * lightDistance;
        }
    }
}
