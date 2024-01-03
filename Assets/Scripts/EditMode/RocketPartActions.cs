using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RocketPartActions : MonoBehaviour
{
    public float distanceToSnap = 0.5f;
    GameObject rocketParent;
    public Vector3 rocketUpNode;
    public Vector3 rocketDownNode;
    public GameObject cursor;
    public GameObject stageEmptyPrefab;
    public StageManager stages;
    int currentStage = 0;
    public bool canPlaceAbove = true;
    GameObject currentPart = null;

    void Start()
    {
        stages.stages = new List<List<GameObject>>();
    }

    public void AddPart(GameObject part)
    {
        currentPart = part;

        if(rocketParent.transform.childCount == 0)
        {
            Instantiate(stageEmptyPrefab, rocketParent.transform);
            Instantiate(currentPart, rocketParent.transform.GetChild(0).transform);
            rocketUpNode = currentPart.transform.GetChild(0).transform.position;
            rocketDownNode = currentPart.transform.GetChild(1).transform.position;
            stages.stages.Add(new List<GameObject>());
            stages.stages[currentStage].Add(currentPart);
        }
        else
        {
            Instantiate(currentPart, cursor.transform);
        }
    }

    void Update()
    {
        rocketParent = GameObject.Find("Rocket").transform.GetChild(0).gameObject;
        if(rocketParent.transform.childCount > 0)
        {
            rocketUpNode = rocketParent.transform.GetChild(0).GetChild(0).GetChild(0).transform.position;
            rocketDownNode = rocketParent.transform.GetChild(rocketParent.transform.childCount - 1).GetChild(rocketParent.transform.GetChild(rocketParent.transform.childCount - 1).childCount - 1).GetChild(1).transform.position;
        }
        cursor.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        if (Input.GetMouseButtonDown(0) && cursor.transform.childCount > 0)
        {
            GameObject tempObject = cursor.transform.GetChild(0).gameObject;
            Vector3 tempObjectUpNode = tempObject.transform.GetChild(0).transform.position;
            Vector3 tempObjectDownNode = tempObject.transform.GetChild(1).transform.position;
            cursor.transform.DetachChildren();
            if (Vector3.Distance(tempObjectUpNode, rocketDownNode) <= distanceToSnap)
            {
                currentStage = stages.stages.Count - 1;
                Vector3 tempDistanceUp = tempObjectUpNode - tempObject.transform.position;
                tempObject.transform.position = new Vector3(rocketParent.transform.position.x, rocketDownNode.y - tempDistanceUp.y, rocketParent.transform.position.z);
                if (tempObject.tag == "Separator")
                {
                    string lastPart = stages.stages[currentStage].Last().name;
                    if(lastPart == "LiquidEngineBig_1.25m" || lastPart == "LiquidEngineBig_1.25m(Clone)")
                        tempObject.transform.GetChild(3).gameObject.SetActive(true);
                    else if (lastPart == "LiquidEngineSmall_1.25m" || lastPart == "LiquidEngineSmall_1.25m(Clone)")
                        tempObject.transform.GetChild(4).gameObject.SetActive(true);
                    Instantiate(stageEmptyPrefab, rocketParent.transform);
                    stages.stages.Add(new List<GameObject>());
                    currentStage++;
                }
                tempObject.transform.SetParent(rocketParent.transform.GetChild(currentStage).transform);
                rocketDownNode = tempObjectDownNode;
                stages.stages[currentStage].Add(tempObject);
            }
            if (Vector3.Distance(tempObjectDownNode, rocketUpNode) <= distanceToSnap && canPlaceAbove)
            {
                currentStage = 0;
                Vector3 tempDistanceDown = tempObjectDownNode - tempObject.transform.position;
                tempObject.transform.position = new Vector3(rocketParent.transform.position.x, rocketUpNode.y - tempDistanceDown.y, rocketParent.transform.position.z);
                if (tempObject.tag == "Separator")
                {
                    Instantiate(stageEmptyPrefab, rocketParent.transform);
                    stages.stages.Insert(0, new List<GameObject>());
                    currentStage = 1;
                }
                if (tempObject.name == "FuelTankCone_1.25m(Clone)" || tempObject.name == "Chute_1.25m(Clone)")
                {
                    canPlaceAbove = false;
                }
                tempObject.transform.SetParent(rocketParent.transform.GetChild(currentStage).transform);
                rocketUpNode = tempObjectUpNode;
                stages.stages[currentStage].Insert(0, currentPart);
            }
            rocketParent.GetComponent<Rocket>().isInitialDataSet = false;
            rocketParent.transform.GetChild(currentStage).GetComponent<StageData>().isDataUpdated = false;
        }
    }
}
