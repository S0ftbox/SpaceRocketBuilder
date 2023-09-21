using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Staging : MonoBehaviour
{
    Rocket rocket;
    Transform rocketObject;
    RectTransform fuel;
    public float deltaV;
    float currentFuel;
    float initialRectX, initialRectY;
    Planet planet;
    public PlanetTargetSwitch planetSwitch;
    public Transform stageUI;
    public GameObject stageData;
    public Texture chute, engine, core;

    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket").transform.GetChild(0).gameObject.GetComponent<Rocket>();
        rocketObject = GameObject.FindGameObjectWithTag("Rocket").transform.GetChild(0);
        for (int i = 0; i < rocketObject.childCount; i++)
        {
            GameObject stage = (GameObject)Instantiate(stageData);
            stage.transform.SetParent(stageUI);
            stage.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
            for (int j = 0; j < rocketObject.GetChild(i).childCount; j++)
            {
                
                if (rocketObject.GetChild(i).GetChild(j).gameObject.tag == "Chute")
                {
                    stage.transform.GetChild(1).GetComponent<RawImage>().texture = chute;
                    stage.transform.GetChild(2).gameObject.SetActive(false);
                    Debug.Log("Chute in stage " + i);
                }
                if (rocketObject.GetChild(i).GetChild(j).gameObject.tag == "Engine")
                {
                    stage.transform.GetChild(1).GetComponent<RawImage>().texture = engine;
                    Debug.Log("Engine in stage " + i);
                }
                if (rocketObject.GetChild(i).GetChild(j).gameObject.tag == "Core")
                {
                    stage.transform.GetChild(1).GetComponent<RawImage>().texture = core;
                    stage.transform.GetChild(2).gameObject.SetActive(false);
                    Debug.Log("Core in stage " + i);
                }
            }
        }
        fuel = stageUI.GetChild(stageUI.childCount - 1).GetChild(2).GetChild(0).GetComponent<RectTransform>();
        initialRectX = fuel.parent.GetComponent<RectTransform>().sizeDelta.x;
        initialRectY = fuel.sizeDelta.y;
        fuel.sizeDelta = new Vector2(initialRectX, initialRectY);
    }

    void Update()
    {
        if(rocketObject.childCount != stageUI.childCount)
        {
            GameObject.Destroy(stageUI.GetChild(stageUI.childCount - 1).gameObject);
            if (stageUI.GetChild(stageUI.childCount - 1).GetChild(1).GetComponent<RawImage>().texture == engine)
            {
                Debug.Log(stageUI.childCount - 2);
                fuel = stageUI.GetChild(stageUI.childCount - 2).GetChild(2).GetChild(0).GetComponent<RectTransform>();
                initialRectX = fuel.parent.GetComponent<RectTransform>().sizeDelta.x;
                initialRectY = fuel.sizeDelta.y;
                fuel.sizeDelta = new Vector2(initialRectX, initialRectY);
            }
        }
        planet = planetSwitch.focusedPlanet;
        deltaV = (rocket.engineThrust / (rocket.engineBurnTime * planet.gravity) * planet.gravity) * (Mathf.Log(rocket.totalMass / rocket.emptyTotalMass)) * 1000;
        currentFuel = rocket.currentFuelRatio;
        fuel.sizeDelta = new Vector2(initialRectX * currentFuel, initialRectY);
    }
}
