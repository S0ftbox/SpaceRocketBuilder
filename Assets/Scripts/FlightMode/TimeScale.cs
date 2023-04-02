using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScale : MonoBehaviour
{
    public Text timeScale;
    public int timeScaleMultiplier;
    public int currentTimeScale = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period) && currentTimeScale < 3)
            currentTimeScale++;
        if (Input.GetKeyDown(KeyCode.Comma) && currentTimeScale > 0)
            currentTimeScale--;
        if (Input.GetKeyDown(KeyCode.Slash) && currentTimeScale > 0)
            currentTimeScale = 0;

        timeScale.text = timeScaleMultiplier + "x";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeScaleMultiplier = (int)Mathf.Pow(2, currentTimeScale);
        Time.timeScale = timeScaleMultiplier;
    }
}
