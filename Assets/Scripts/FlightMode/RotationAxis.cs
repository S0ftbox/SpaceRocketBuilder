using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//pitch, roll and yaw indicators manager
public class RotationAxis : MonoBehaviour
{
    public Rocket rocket;
    public Slider pitch, roll, yaw;
    float pitchVal, rollVal, yawVal;

    //method which updates sliders regarding the current rotation forces of the rocket
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
