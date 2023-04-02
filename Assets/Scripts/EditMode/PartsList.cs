using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartsList : MonoBehaviour
{
    [SerializeField] public commandModule[] commandModules;
    [SerializeField] public fuelTank[] fuelTanks;
    [SerializeField] public liquidFuelEngine[] liquidFuelEngines;
    [SerializeField] public solidFuelEngine[] solidFuelEngines;
    [SerializeField] public separator[] separators;
    [SerializeField] public chute[] chutes;
}

[System.Serializable]
public class commandModule
{
    public bool isManned;
    public float mass;
    public Sprite icon;
    public GameObject prefab;
    public string name;
    public string description;
}

[System.Serializable]
public class fuelTank
{
    public float dryMass;
    public float initialFuelMass;
    public Sprite icon;
    public GameObject prefab;
    public string name;
    public string description;
}

[System.Serializable]
public class liquidFuelEngine
{
    public float thrustPower;
    public float mass;
    public float burnRate;
    public Sprite icon;
    public GameObject prefab;
    public string name;
    public string description;
}

[System.Serializable]
public class solidFuelEngine
{
    public float thrustPower;
    public float mass;
    public float burnRate;
    public Sprite icon;
    public GameObject prefab;
    public string name;
    public string description;
}

[System.Serializable]
public class separator
{
    public float mass;
    public Sprite icon;
    public GameObject prefab;
    public string name;
    public string description;
}

[System.Serializable]
public class chute
{
    public float mass;
    public Sprite icon;
    public GameObject prefab;
    public string name;
    public string description;
}