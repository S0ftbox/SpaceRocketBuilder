using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Staging : MonoBehaviour
{
    public Rocket rocket;
    public RectTransform fuel;
    public float deltaV;
    float currentFuel;
    float initialRectX, initialRectY;
    public Planet planet;

    void Start()
    {
        initialRectX = fuel.parent.GetComponent<RectTransform>().sizeDelta.x;
        initialRectY = fuel.sizeDelta.y;
        fuel.sizeDelta = new Vector2(initialRectX, initialRectY);
    }

    void Update()
    {
        deltaV = (rocket.engineThrust / (rocket.engineBurnTime * planet.gravity) * planet.gravity) * (Mathf.Log(rocket.totalMass / rocket.emptyTotalMass)) * 1000;
        currentFuel = rocket.currentFuelRatio;
        fuel.sizeDelta = new Vector2(initialRectX * currentFuel, initialRectY);
    }
}
