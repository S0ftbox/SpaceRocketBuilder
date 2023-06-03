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

    public GameObject getPrefab(string prefab)
    {
        int cloneWordFinder = prefab.IndexOf("(Clone)");
        if(cloneWordFinder != -1)
            prefab = prefab.Remove(prefab.Length - 7);
        foreach(var c in commandModules)
        {
            if (c.prefab.name == prefab)
                return c.prefab;
        }
        foreach(var f in fuelTanks)
        {
            if (f.prefab.name == prefab)
                return f.prefab;
        }
        foreach (var l in liquidFuelEngines)
        {
            if (l.prefab.name == prefab)
                return l.prefab;
        }
        foreach (var s in solidFuelEngines)
        {
            if (s.prefab.name == prefab)
                return s.prefab;
        }
        foreach (var s in separators)
        {
            if (s.prefab.name == prefab)
                return s.prefab;
        }
        foreach (var c in chutes)
        {
            if (c.prefab.name == prefab)
                return c.prefab;
        }
        return null;
    }
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
    public float dryMass;
    public float initialFuelMass;
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