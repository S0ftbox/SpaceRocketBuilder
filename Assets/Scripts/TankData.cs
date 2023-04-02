using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    public float dryMass;
    public float massWithFuel;
    public float currentMass;

    private void Awake()
    {
        currentMass = massWithFuel;
    }
}
