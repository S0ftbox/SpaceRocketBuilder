using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(SceneManager.GetActiveScene().name == "FlightMode")
        {
            for (int i = 0; i < planets.Length; i++)
            {
                distance = Vector3.Distance(planets[i].transform.position, rocket.transform.position);
                if (distance <= planets[i].maxDistance)
                {
                    focusedPlanet = planets[i];
                }
                else
                {
                    focusedPlanet = planets[i - 1];
                    break;
                }
            }
            if (!rocket.GetComponent<Rocket>().isStageActive)
            {
                rocket.transform.SetParent(focusedPlanet.transform);
                if (focusedPlanet.name == "Planet")
                {
                    focusedPlanet.GetComponent<Rigidbody>().isKinematic = false;
                    focusedPlanet.GetComponent<Rigidbody>().mass = 500000;
                    GameObject.Find("Moon").GetComponent<Rigidbody>().isKinematic = true;
                    GameObject.Find("Moon").GetComponent<Rigidbody>().mass = 0.000001f;
                    GameObject.Find("Sun").GetComponent<Rigidbody>().isKinematic = true;
                    GameObject.Find("Sun").GetComponent<Rigidbody>().mass = 0.000001f;
                }
                if (focusedPlanet.name == "Moon")
                {
                    focusedPlanet.GetComponent<Rigidbody>().isKinematic = false;
                    focusedPlanet.GetComponent<Rigidbody>().mass = 5000;
                    GameObject.Find("Planet").GetComponent<Rigidbody>().isKinematic = true;
                    GameObject.Find("Planet").GetComponent<Rigidbody>().mass = 0.000001f;
                    GameObject.Find("Sun").GetComponent<Rigidbody>().isKinematic = true;
                    GameObject.Find("Sun").GetComponent<Rigidbody>().mass = 0.000001f;
                }
                if (focusedPlanet.name == "Sun")
                {
                    focusedPlanet.GetComponent<Rigidbody>().isKinematic = false;
                    focusedPlanet.GetComponent<Rigidbody>().mass = 500000000;
                    GameObject.Find("Planet").GetComponent<Rigidbody>().isKinematic = true;
                    GameObject.Find("Planet").GetComponent<Rigidbody>().mass = 0.000001f;
                    GameObject.Find("Moon").GetComponent<Rigidbody>().isKinematic = true;
                    GameObject.Find("Moon").GetComponent<Rigidbody>().mass = 0.000001f;
                }
            }
        }
    }
}
