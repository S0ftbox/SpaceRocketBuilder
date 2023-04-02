using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbitParameters : MonoBehaviour
{
    float semiMajorAxis;
    float u;
    float eccentricity;
    float inclination;
    float ascendingNodeLongitude;
    float periapsis;
    float apoapsis;
    float periapsisArgument;
    float trueAnomaly;
    public GameObject rocket;
    public GameObject planet;
    public OrbitPathDraw pathDraw;
    //[Range(1, 100)]
    //public int pathSegments = 50;
    Rigidbody rocketRb;
    Rigidbody planetRb;
    Vector3 velocity;
    public Text semiMajorText, eccentricityText, apoapsisText, periapsisText, inclinationText, ascNodeLongText;
    //public LineRenderer lineRenderer;

    void Start()
    {
        rocketRb = rocket.GetComponent<Rigidbody>();
        planetRb = planet.GetComponent<Rigidbody>();
        u = 0.1f * (rocketRb.mass + planetRb.mass);
        //lineRenderer.GetComponent<LineRenderer>();
        //lineRenderer.positionCount = pathSegments + 1;
        //lineRenderer.loop = true;
    }

    void FixedUpdate()
    {
        Vector3 rocketPosition = rocketRb.position - planetRb.position;
        Vector3 rocketVelocity = rocketRb.velocity;
        float rocketSpeed = rocketVelocity.magnitude;
        float gForce = u / rocketPosition.sqrMagnitude;
        float spEnergy = 0.5f * rocketSpeed * rocketSpeed - 0.1f * planetRb.mass / rocketPosition.magnitude;
        semiMajorAxis = -0.1f * planetRb.mass / (2 * spEnergy);
        Vector3 rocketMomentum = Vector3.Cross(rocketPosition, rocketVelocity);
        Vector3 planetMomentum = -rocketMomentum * rocketRb.mass / planetRb.mass;
        Vector3 angularMomentumVector = rocketMomentum + planetMomentum;
        float angularMomentum = angularMomentumVector.magnitude;
        Vector3 eccentricityVector = Vector3.Cross(rocketVelocity, angularMomentumVector) / (0.1f * planetRb.mass) - rocketPosition / rocketPosition.magnitude;
        eccentricity = eccentricityVector.magnitude;
        Vector3 ascendingNodeVector = Vector3.Cross(Vector3.up, angularMomentumVector);
        inclination = Vector3.Angle(angularMomentumVector, ascendingNodeVector);
        ascendingNodeLongitude = Vector3.Angle(Vector3.right, ascendingNodeVector);
        if(ascendingNodeVector.y < 0)
        {
            ascendingNodeLongitude = 360 - ascendingNodeLongitude;
        }
        periapsis = (semiMajorAxis * (1 - eccentricity) - 600) * 100;
        apoapsis = (semiMajorAxis * (1 + eccentricity) - 600) * 100;
        periapsisArgument = Vector3.Angle(ascendingNodeVector, eccentricityVector);
        if (eccentricityVector.z < 0)
        {
            periapsisArgument = 360 - periapsisArgument;
        }
        trueAnomaly = Vector3.Angle(eccentricityVector, rocketPosition);
        if(Vector3.Dot(rocketPosition, rocketVelocity) < 0)
        {
            trueAnomaly = 360 - trueAnomaly;
        }

        //pathDraw.DrawEllipse(planetRb.transform, semiMajorAxis, eccentricity, inclination, ascendingNodeLongitude, pathSegments, lineRenderer);

        semiMajorText.text = "Semi-major axis: " + semiMajorAxis.ToString("n2");
        eccentricityText.text = "Eccentricity: " + eccentricity.ToString("n2");
        apoapsisText.text = "Apoapsis: " + apoapsis.ToString("n0") + " m";
        if(periapsis < 0)
            periapsisText.text = "Periapsis: 0 m";
        else
            periapsisText.text = "Periapsis: " + periapsis.ToString("n0") + " m";
        inclinationText.text = "Inclination: " + inclination.ToString("n0") + "°";
        ascNodeLongText.text = "Ascending Node: " + ascendingNodeLongitude.ToString("n0") + "°";
    }

    public float getInclination()
    {
        return inclination;
    }

    public float getAscendingNode()
    {
        return ascendingNodeLongitude;
    }
}
