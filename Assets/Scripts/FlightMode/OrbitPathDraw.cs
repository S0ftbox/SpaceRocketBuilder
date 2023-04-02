using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitPathDraw : MonoBehaviour
{
    public void DrawEllipse(Transform focusPoint, float semiMajorAxis, float eccentricity, float inclination, float ascendingNode, int segments, LineRenderer renderer)
    {
        Vector3[] positions = new Vector3[segments + 1];

        Vector3 majorAxis = semiMajorAxis * new Vector3(Mathf.Cos(Mathf.Deg2Rad * ascendingNode), Mathf.Sin(Mathf.Deg2Rad * ascendingNode), 0f);
        Vector3 minorAxis = semiMajorAxis * Mathf.Sqrt(1f - Mathf.Pow(eccentricity, 2f)) * new Vector3(-Mathf.Sin(Mathf.Deg2Rad * ascendingNode), Mathf.Cos(Mathf.Deg2Rad * ascendingNode), 0f);

        Quaternion inclinationRotation = Quaternion.Euler(inclination, 0f, 0f);
        Quaternion nodeRotation = Quaternion.Euler(0f, 0f, ascendingNode);

        for(int i = 0; i <= segments; i++)
        {
            float angle = (float)i / segments * 360f;

            Vector3 position = focusPoint.position + majorAxis * Mathf.Cos(Mathf.Deg2Rad * angle) + minorAxis * Mathf.Sin(Mathf.Deg2Rad * angle);
            position = nodeRotation * position;
            position = inclinationRotation * position;

            positions[i] = position;
        }
        renderer.SetPositions(positions);
    }
}
