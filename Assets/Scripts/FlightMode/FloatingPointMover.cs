using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloatingPointMover : MonoBehaviour
{
    Vector3 distanceFromMapCenter;
    GameObject rocket;
    GameObject[] movableGameObjects;

    void Awake()
    {
        movableGameObjects = GameObject.FindGameObjectsWithTag("FloatingPointMovable");
    }

    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket").transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "FlightMode")
        {
            distanceFromMapCenter = rocket.transform.position - new Vector3(0, 0, 0);
            if (distanceFromMapCenter.magnitude > 100)
            {

                for (int i = 0; i < movableGameObjects.Length; i++)
                {
                    movableGameObjects[i].transform.position -= rocket.transform.position;
                }
                rocket.transform.position = new Vector3(0, 0, 0);
            }
        }
    }
}
