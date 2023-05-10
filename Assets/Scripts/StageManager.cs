using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] public StageComponents[] stages;
}

[System.Serializable]
public class StageComponents
{
    public GameObject[] components;
}
