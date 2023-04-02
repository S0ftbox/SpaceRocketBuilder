using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    public Rigidbody rocketRigidbody;
    public float coreMass, tankMass, emptyTankMass, engineMass;
    public float totalMass, emptyTotalMass;
    public float currentThrust, engineThrust, engineBurnTime;
    public float massBurnRate;
    public float rotationSpeed = 5f;
    public float pitch, roll, yaw;
    public GameObject[] cores;
    public GameObject[] tanks;
    public GameObject[] engines;
    public bool isStageActive = false;
    public Text throttle;
    public GameObject navball;

    private void Awake()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
        foreach(GameObject engine in engines)
        {
            engineThrust = engine.GetComponent<EngineData>().thrustPower;
            engineBurnTime = engine.GetComponent<EngineData>().burnRate;
        }
    }

    private void Start()
    {
        foreach(GameObject core in cores)
        {
            coreMass += core.GetComponent<CoreData>().mass;
        }
        foreach (GameObject tank in tanks)
        {
            tankMass += tank.GetComponent<TankData>().currentMass;
            emptyTankMass += tank.GetComponent<TankData>().dryMass;
        }
        foreach (GameObject engine in engines)
        {
            engineMass += engine.GetComponent<EngineData>().mass;
        }
        totalMass = coreMass + tankMass + engineMass;
        emptyTotalMass = coreMass + emptyTankMass + engineMass;
        massBurnRate = (totalMass - emptyTotalMass) / engineBurnTime;
        rocketRigidbody.mass = totalMass;
    }

    void UpdateMass(float dt)
    {
        if (totalMass <= emptyTotalMass) return;
        totalMass -= massBurnRate * dt * currentThrust / 100;
        rocketRigidbody.mass = Mathf.Max(totalMass, emptyTotalMass);
    }

    void ApplyThrust()
    {
        rocketRigidbody.AddForce(currentThrust * transform.up);
    }

    void UpdateThrust()
    {
        if(totalMass <= emptyTotalMass)
        {
            currentThrust = 0f;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            isStageActive = true;

        if (Input.GetKeyDown(KeyCode.Z))
            currentThrust = engineThrust;
        if (Input.GetKeyDown(KeyCode.X))
            currentThrust = 0;
        if (Input.GetKey(KeyCode.LeftShift) && currentThrust < engineThrust)
            currentThrust += engineThrust * 0.0025f;
        if (Input.GetKey(KeyCode.LeftControl) && currentThrust > 0)
            currentThrust -= engineThrust * 0.0025f;
        if (currentThrust < 0)
            currentThrust = 0;
        if (currentThrust > engineThrust)
            currentThrust = engineThrust;

        float throttleVal = currentThrust / engineThrust * 100;
        throttle.text = throttleVal.ToString("n0") + " %";
    }

    private void FixedUpdate()
    {


        if (isStageActive)
        {
            UpdateMass(Time.fixedDeltaTime);
            ApplyThrust();
            UpdateThrust();
        }

        pitch = Input.GetAxis("Horizontal") * -1;
        yaw = Input.GetAxis("Vertical") * -1;
        roll = Input.GetAxis("Roll") * -1;
        if (!Mathf.Approximately(pitch, 0f))
        {
            rocketRigidbody.freezeRotation = true;
            transform.Rotate(Vector3.right * pitch * rotationSpeed * Time.deltaTime);
            navball.transform.Rotate(Vector3.right * pitch * rotationSpeed * Time.deltaTime);
        }
        if (!Mathf.Approximately(yaw, 0f))
        {
            rocketRigidbody.freezeRotation = true;
            transform.Rotate(Vector3.forward * yaw * rotationSpeed * Time.deltaTime);
            navball.transform.Rotate(Vector3.forward * -yaw * rotationSpeed * Time.deltaTime);
        }
        if(!Mathf.Approximately(roll, 0f))
        {
            rocketRigidbody.freezeRotation = true;
            transform.Rotate(Vector3.up * roll * rotationSpeed * Time.deltaTime);
            navball.transform.Rotate(Vector3.up * roll * rotationSpeed * Time.deltaTime);
        }
        rocketRigidbody.freezeRotation = false;
    }
}
