using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navball : MonoBehaviour
{
    GameObject rocket;
    GameObject planet;
    public PlanetTargetSwitch planetSwitch;

    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket").transform.GetChild(0).gameObject;
    }

    void Update()
    {
        planet = planetSwitch.focusedPlanet.gameObject;
        Vector3 direction = rocket.transform.position - planet.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        Quaternion localRotation = Quaternion.Inverse(rocket.transform.rotation) * targetRotation;
        transform.localRotation = localRotation;


        //new Quaternion(rocket.transform.localRotation.x, rocket.transform.localRotation.y, -rocket.transform.localRotation.z)
    }

    private void FixedUpdate()
    {
        if (!Mathf.Approximately(rocket.GetComponent<Rocket>().pitch, 0f))
        {
            transform.Rotate(Vector3.right * rocket.GetComponent<Rocket>().pitch * rocket.GetComponent<Rocket>().rotationSpeed * Time.deltaTime);
        }
        if (!Mathf.Approximately(rocket.GetComponent<Rocket>().yaw, 0f))
        {
            transform.Rotate(Vector3.forward * -rocket.GetComponent<Rocket>().yaw * rocket.GetComponent<Rocket>().rotationSpeed * Time.deltaTime);
        }
        if (!Mathf.Approximately(rocket.GetComponent<Rocket>().roll, 0f))
        {
            transform.Rotate(Vector3.up * rocket.GetComponent<Rocket>().roll * rocket.GetComponent<Rocket>().rotationSpeed * Time.deltaTime);
        }
    }
}
