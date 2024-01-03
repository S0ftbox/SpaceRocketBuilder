using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunLightRotation : MonoBehaviour
{
    public Transform sun, planet;
    Vector3 direction;
    Quaternion rotation;

    // Update is called once per frame
    void Update()
    {
        direction = (planet.position - sun.position).normalized;
        rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        /*float distanceToSun = Vector3.Distance(rocket.position, sun.position);
        if(distanceToSun <= lightDistance)
        {
            transform.position = sun.position;
        }
        else
        {
            Vector3 directionToRocket = (rocket.position - transform.position).normalized;
            transform.position = rocket.position + directionToRocket * lightDistance;
        }*/
    }
}
