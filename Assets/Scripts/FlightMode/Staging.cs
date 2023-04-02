using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Staging : MonoBehaviour
{
    public Rocket rocket;
    public Slider fuel;
    float initialFuel;

    void Start()
    {
        initialFuel = rocket.tankMass - rocket.emptyTankMass;
        fuel.maxValue = initialFuel;
        fuel.value = initialFuel;
    }

    void FixedUpdate()
    {
        while (fuel.value > 0)
        {
            fuel.value -= rocket.massBurnRate * Time.fixedDeltaTime * rocket.currentThrust / 100;
        }
    }
}
