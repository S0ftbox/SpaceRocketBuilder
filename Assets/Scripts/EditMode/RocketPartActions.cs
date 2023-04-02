using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPartActions : MonoBehaviour
{
    public float distanceToSnap = 0.5f;
    public GameObject rocketParent;
    Vector3 rocketUpNode;
    Vector3 rocketDownNode;
    public GameObject cursor;
    public GameObject stageEmptyPrefab;
    public StageManager stages;
    int currentStage;
    GameObject currentPart = null;

    public void AddPart(GameObject part)
    {
        currentPart = part;

        if(rocketParent.transform.childCount == 0)
        {
            Instantiate(stageEmptyPrefab, rocketParent.transform);
            Instantiate(currentPart, rocketParent.transform.GetChild(0).transform);
            rocketUpNode = currentPart.transform.GetChild(0).transform.position;
            rocketDownNode = currentPart.transform.GetChild(1).transform.position;
            //stages.stages.
            currentStage = 0;
            //stages.stages[currentStage] = stageEmptyPrefab;
        }
        else
        {
            Instantiate(currentPart, cursor.transform);
        }
    }

    void Update()
    {
        cursor.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        if (Input.GetMouseButtonDown(0) && cursor.transform.childCount > 0)
        {
            GameObject tempObject = cursor.transform.GetChild(0).gameObject;
            Vector3 tempObjectUpNode = tempObject.transform.GetChild(0).transform.position;
            Vector3 tempObjectDownNode = tempObject.transform.GetChild(1).transform.position;
            cursor.transform.DetachChildren();
            if (Vector3.Distance(tempObjectUpNode, rocketDownNode) <= distanceToSnap)
            {
                Vector3 tempDistanceUp = tempObjectUpNode - tempObject.transform.position;
                tempObject.transform.position = new Vector3(rocketParent.transform.position.x, rocketDownNode.y - tempDistanceUp.y, rocketParent.transform.position.z);
                if(tempObject.tag == "Engine")
                {
                    Instantiate(stageEmptyPrefab, rocketParent.transform);
                    currentStage++;
                }
                tempObject.transform.SetParent(rocketParent.transform.GetChild(currentStage).transform);
                rocketDownNode = tempObjectDownNode;
            }
            if (Vector3.Distance(tempObjectDownNode, rocketUpNode) <= distanceToSnap)
            {
                Vector3 tempDistanceDown = tempObjectDownNode - tempObject.transform.position;
                tempObject.transform.position = new Vector3(rocketParent.transform.position.x, rocketUpNode.y - tempDistanceDown.y, rocketParent.transform.position.z);
                if (tempObject.tag == "Engine")
                {
                    Instantiate(stageEmptyPrefab, rocketParent.transform);
                    currentStage++;
                }
                tempObject.transform.SetParent(rocketParent.transform.GetChild(currentStage).transform);
                rocketUpNode = tempObjectUpNode;
            }
        }
        
    }
}
