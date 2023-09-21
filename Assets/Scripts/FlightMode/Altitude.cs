using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Altitude : MonoBehaviour
{
    public Text altitude;
    GameObject rocket;
    GameObject planet;
    public float altitudeValue;
    int planetSeaLevel;
    public PlanetTargetSwitch planetSwitch;

    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket").transform.GetChild(0).gameObject;
    }

    void Update()
    {
        planet = planetSwitch.focusedPlanet.gameObject;
        planetSeaLevel = planetSwitch.focusedPlanet.radius;
        altitudeValue = (Vector3.Distance(planet.transform.position, rocket.transform.position) - planetSeaLevel) * 100;
        altitude.text = altitudeValue.ToString("n0") + " m";
    }
}
