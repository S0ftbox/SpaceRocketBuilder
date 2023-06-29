using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrottleValue : MonoBehaviour
{
    GameObject rocket;
    public Text throttleText;
    
    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket").transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        throttleText.text = rocket.GetComponent<Rocket>().throttle;
    }
}
