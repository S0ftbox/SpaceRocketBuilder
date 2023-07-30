using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
    public float dryMass, totalMass;
    public float thrustPower, burnRate;
    public bool hasCoreModule, isDataUpdated, isStageActive;
    public int crewCount;
    public Vector3 startVelocity, startAngularVelocity;

    void Update()
    {
        if(transform.childCount > 0 && !isDataUpdated)
        {
            dryMass = 0;
            totalMass = 0;
            thrustPower = 0;
            burnRate = 0;
            hasCoreModule = false;
            for(int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).tag == "Core")
                {
                    hasCoreModule = true;
                    dryMass += transform.GetChild(i).GetComponent<CoreData>().mass;
                    totalMass += transform.GetChild(i).GetComponent<CoreData>().mass;
                    if (transform.GetChild(i).GetComponent<CoreData>().isManned)
                    {
                        crewCount += transform.GetChild(i).GetComponent<CoreData>().crewCount;
                    }
                }
                if (transform.GetChild(i).tag == "Fuel")
                {
                    dryMass += transform.GetChild(i).GetComponent<TankData>().dryMass;
                    totalMass += transform.GetChild(i).GetComponent<TankData>().massWithFuel;
                }
                if (transform.GetChild(i).tag == "Engine")
                {
                    thrustPower = transform.GetChild(i).GetComponent<EngineData>().thrustPower;
                    burnRate = transform.GetChild(i).GetComponent<EngineData>().burnRate;
                    dryMass += transform.GetChild(i).GetComponent<EngineData>().mass;
                    totalMass += transform.GetChild(i).GetComponent<EngineData>().mass;
                    if (transform.GetChild(i).GetComponent<EngineData>().solidFuel)
                    {
                        dryMass += transform.GetChild(i).GetComponent<TankData>().dryMass;
                        totalMass += transform.GetChild(i).GetComponent<TankData>().massWithFuel;
                    }
                }
                if(transform.GetChild(i).tag == "Separator")
                {
                    dryMass += transform.GetChild(i).GetComponent<OtherData>().mass;
                    totalMass += transform.GetChild(i).GetComponent<OtherData>().mass;
                }
            }
            isDataUpdated = true;
            transform.parent.GetComponent<Rocket>().isInitialDataSet = false;
        }
        if(transform.parent == null && gameObject.GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
            gameObject.GetComponent<Rigidbody>().mass = dryMass;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
