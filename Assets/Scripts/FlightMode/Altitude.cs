using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Altitude : MonoBehaviour
{
    public Text altitude;
    GameObject rocket;
    public GameObject planet;
    float altitudeValue;

    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket").transform.GetChild(0).gameObject;
    }

    void Update()
    {
        altitudeValue = (Vector3.Distance(planet.transform.position, rocket.transform.position) - 600) * 100;
        altitude.text = altitudeValue.ToString("n0") + " m";
    }
}
