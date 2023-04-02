using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navball : MonoBehaviour
{
    public GameObject rocket;
    public GameObject planet;

    void Update()
    {
        Vector3 direction = rocket.transform.position - planet.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        Quaternion localRotation = Quaternion.Inverse(rocket.transform.rotation) * targetRotation;
        transform.localRotation = localRotation;


        //new Quaternion(rocket.transform.localRotation.x, rocket.transform.localRotation.y, -rocket.transform.localRotation.z)
    }
}
