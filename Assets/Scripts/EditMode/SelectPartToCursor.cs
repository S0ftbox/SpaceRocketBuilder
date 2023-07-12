using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPartToCursor : MonoBehaviour
{
    public GameObject cursor;
    public RocketPartActions partActions;

    
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Core" || hit.collider.tag == "Engine" || hit.collider.tag == "Fuel" || hit.collider.tag == "Separator")
            {
                if (hit.transform.parent == null && Input.GetMouseButtonDown(1))
                {
                    Instantiate(hit.collider.gameObject, cursor.transform);
                    cursor.transform.GetChild(0).transform.position = cursor.transform.position;
                    DestroyImmediate(hit.collider.gameObject);
                }
            }
        }
    }
}
