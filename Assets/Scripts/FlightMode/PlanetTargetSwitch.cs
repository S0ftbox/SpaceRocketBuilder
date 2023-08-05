using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetTargetSwitch : MonoBehaviour
{
    public Planet[] planets;
    public Planet focusedPlanet;
    GameObject rocket;
    float distance;

    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket").transform.GetChild(0).gameObject;
    }

    void Update()
    {
        for(int i = 0; i < 3; i++)
        {
            distance = Vector3.Distance(planets[i].transform.position, rocket.transform.position);
            if(distance <= planets[i].maxDistance)
            {
                focusedPlanet = planets[i];
            }
            else
            {
                focusedPlanet = planets[i-1];
                break;
            }
        }
        if (!rocket.GetComponent<Rocket>().isStageActive)
        {
            rocket.transform.SetParent(focusedPlanet.transform);
            if (focusedPlanet.name == "Planet")
            {
                //focusedPlanet.GetComponent<PlanetRotation>().enabled = false;
                //focusedPlanet.GetComponent<PlanetRotationInverted>().enabled = false;
                focusedPlanet.GetComponent<Rigidbody>().isKinematic = false;
                focusedPlanet.GetComponent<Rigidbody>().mass = 500000;
                //GameObject.Find("PlanetScaled").GetComponent<PlanetRotation>().enabled = false;
                //GameObject.Find("PlanetScaled").GetComponent<PlanetRotationInverted>().enabled = false;
                //GameObject.Find("Moon").GetComponent<PlanetRotation>().enabled = true;
                GameObject.Find("Moon").GetComponent<Rigidbody>().isKinematic = true;
                GameObject.Find("Moon").GetComponent<Rigidbody>().mass = 0.00001f;
                //GameObject.Find("MoonScaled").GetComponent<PlanetRotation>().enabled = true;
                //GameObject.Find("Sun").GetComponent<PlanetRotationInverted>().enabled = true;
                GameObject.Find("Sun").GetComponent<Rigidbody>().isKinematic = true;
                GameObject.Find("Sun").GetComponent<Rigidbody>().mass = 0.00001f;
                //GameObject.Find("SunScaled").GetComponent<PlanetRotationInverted>().enabled = true;
            }
            if (focusedPlanet.name == "Moon")
            {
                //focusedPlanet.GetComponent<PlanetRotation>().enabled = false;
                focusedPlanet.GetComponent<Rigidbody>().isKinematic = false;
                focusedPlanet.GetComponent<Rigidbody>().mass = 5000;
                //GameObject.Find("MoonScaled").GetComponent<PlanetRotation>().enabled = false;
               // GameObject.Find("Planet").GetComponent<PlanetRotation>().enabled = false;
                //GameObject.Find("Planet").GetComponent<PlanetRotationInverted>().enabled = true;
                GameObject.Find("Planet").GetComponent<Rigidbody>().isKinematic = true;
                GameObject.Find("Planet").GetComponent<Rigidbody>().mass = 0.00001f;
                //GameObject.Find("PlanetScaled").GetComponent<PlanetRotation>().enabled = false;
                //GameObject.Find("PlanetScaled").GetComponent<PlanetRotationInverted>().enabled = true;
                //GameObject.Find("Sun").GetComponent<PlanetRotationInverted>().enabled = true;
                GameObject.Find("Sun").GetComponent<Rigidbody>().isKinematic = true;
                GameObject.Find("Sun").GetComponent<Rigidbody>().mass = 0.00001f;
                //GameObject.Find("SunScaled").GetComponent<PlanetRotationInverted>().enabled = true;
            }
            if (focusedPlanet.name == "Sun")
            {
               // focusedPlanet.GetComponent<PlanetRotationInverted>().enabled = false;
                focusedPlanet.GetComponent<Rigidbody>().isKinematic = false;
                focusedPlanet.GetComponent<Rigidbody>().mass = 500000000;
                //GameObject.Find("SunScaled").GetComponent<PlanetRotationInverted>().enabled = false;
               // GameObject.Find("Planet").GetComponent<PlanetRotation>().enabled = true;
               // GameObject.Find("Planet").GetComponent<PlanetRotationInverted>().enabled = false;
                GameObject.Find("Planet").GetComponent<Rigidbody>().isKinematic = true;
                GameObject.Find("Planet").GetComponent<Rigidbody>().mass = 0.00001f;
                //GameObject.Find("PlanetScaled").GetComponent<PlanetRotation>().enabled = true;
                //GameObject.Find("PlanetScaled").GetComponent<PlanetRotationInverted>().enabled = false;
               // GameObject.Find("Moon").GetComponent<PlanetRotation>().enabled = true;
                GameObject.Find("Moon").GetComponent<Rigidbody>().isKinematic = true;
                GameObject.Find("Moon").GetComponent<Rigidbody>().mass = 0.00001f;
                //GameObject.Find("MoonScaled").GetComponent<PlanetRotation>().enabled = true;
            }
        }
    }
}
