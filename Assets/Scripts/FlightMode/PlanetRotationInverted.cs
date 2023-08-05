using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotationInverted : MonoBehaviour
{
    public float rotationSpeed;
    public Transform parentPlanet;

    void Update()
    {
        gameObject.transform.RotateAround(parentPlanet.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
