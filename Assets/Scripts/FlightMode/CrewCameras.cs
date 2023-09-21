using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewCameras : MonoBehaviour
{
    GameObject rocket;
    public GameObject crewCamera;
    public GameObject crewCamContainer;
    public int crewCount;

    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket").transform.GetChild(0).gameObject;
    }

    void Update()
    {
        crewCount = rocket.GetComponent<Rocket>().totalCrewCount;
        if(crewCamContainer.transform.childCount == 0)
        {
            for (int i = 0; i < rocket.GetComponent<Rocket>().totalCrewCount; i++)
            {
                GameObject crewCamInstance = Instantiate(crewCamera);
                crewCamInstance.transform.SetParent(crewCamContainer.transform);
            }
        }
    }
}
