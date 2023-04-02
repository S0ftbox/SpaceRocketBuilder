using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPointMover : MonoBehaviour
{
    Vector3 distanceFromMapCenter;
    GameObject[] movableGameObjects;

    void Start()
    {
        movableGameObjects = GameObject.FindGameObjectsWithTag("FloatingPointMovable");
    }

    void Update()
    {
        distanceFromMapCenter = transform.position - new Vector3(0, 0, 0);
        if(distanceFromMapCenter.magnitude > 100)
        {
            
            for(int i = 0; i < movableGameObjects.Length; i++)
            {
                movableGameObjects[i].transform.position -= transform.position;
            }
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
