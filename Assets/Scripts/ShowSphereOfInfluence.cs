using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSphereOfInfluence : MonoBehaviour
{
    public GameObject planet;
    float influenceDistance;

    private void Start()
    {
        influenceDistance = planet.GetComponent<Planet>().maxDistance;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(planet.transform.position, influenceDistance);
    }
}
