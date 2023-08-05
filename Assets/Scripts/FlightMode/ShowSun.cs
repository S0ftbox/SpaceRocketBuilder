using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSun : MonoBehaviour
{
    public Camera regularCamera;
    public GameObject sun, sunModel;

    // Update is called once per frame
    void Update()
    {
        CheckObjectVisibility();
    }

    private void CheckObjectVisibility()
    {
        // Get the direction from the observer to the target object.
        Vector3 directionToTarget = sun.transform.position - regularCamera.transform.position;

        // Perform a linecast from the observer to the target object.
        // If the linecast hits any object (except the target object itself), the target object is hidden.
        RaycastHit hitInfo;
        if (Physics.Linecast(regularCamera.transform.position, sun.transform.position, out hitInfo))
        {
            // Check if the hit object is not the target object itself.
            if (hitInfo.collider.gameObject != sun)
            {
                // The target object is hidden behind another object.
                sunModel.SetActive(false);
            }
            else
            {
                // The target object is not hidden.
                sunModel.SetActive(true);
            }
        }
        else
        {
            // The linecast didn't hit anything, so the target object is not hidden.
            sunModel.SetActive(true);
        }
    }
}
