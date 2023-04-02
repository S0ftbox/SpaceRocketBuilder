using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationAxis : MonoBehaviour
{
    public Rocket rocket;
    public Slider pitch, roll, yaw;
    float pitchVal, rollVal, yawVal;

    void Update()
    {
        pitchVal = rocket.pitch;
        rollVal = rocket.roll;
        yawVal = rocket.yaw;

        pitch.value = -pitchVal;
        roll.value = -rollVal;
        yaw.value = -yawVal;
    }
}
