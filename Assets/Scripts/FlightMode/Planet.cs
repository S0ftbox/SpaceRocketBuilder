using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float gravity = 10f; // Gravitational acceleration value
    public float maxDistance = 1000f; // Maximum distance for gravitational effect
    public float force;
    public int radius;
    public bool hasAtmosphere;
    public int atmosphereLimit;
    public float airDensityAtSeaLevel;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Disable normal gravity for the object
    }

    void FixedUpdate()
    {
        // Get all objects with a rigidbody in the scene
        Rigidbody[] objects = FindObjectsOfType<Rigidbody>();

        foreach (Rigidbody obj in objects)
        {
            if (obj == rb) continue; // Skip the current object
            if (obj.gameObject.name == "Moon") continue;
            if (obj.gameObject.name == "Planet") continue;

            // Calculate the distance and direction between the object and the planet
            Vector3 direction = transform.position - obj.transform.position;
            float distance = direction.magnitude;

            force = 0.1f * rb.mass * obj.mass / Mathf.Pow(distance, 2);
            obj.AddForce(direction.normalized * force, ForceMode.Force);
        }
    }
}