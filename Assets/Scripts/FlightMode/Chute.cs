using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chute : MonoBehaviour
{
    Rocket rocket;
    public float coefficient = 1.75f;
    public bool isActivated = false;
    public GameObject chuteCloth;

    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket").transform.GetChild(0).gameObject.GetComponent<Rocket>();
    }

    void Update()
    {
        if (rocket.activateChute)
        {
            isActivated = true;
        }

        if (isActivated && rocket.isInAtmosphere)
        {
            rocket.dragCoefficient = coefficient;
            chuteCloth.SetActive(true);
        }
    }
}
