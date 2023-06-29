using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    public Rigidbody rocketRigidbody;
    public float coreMass, tankMass, emptyTankMass, engineMass;
    public float totalMass, emptyTotalMass, currentFuelRatio;
    public float currentThrust, engineThrust, engineBurnTime;
    public float massBurnRate;
    public float rotationSpeed = 20f;
    public float pitch, roll, yaw;
    public GameObject[] cores;
    public GameObject[] tanks;
    public GameObject[] engines;
    public bool isStageActive = false;
    public string throttle;
    float initialFuel, currentFuel;
    bool isInitialDataSet = false;
    
    void UpdateMass(float dt)
    {
        if (totalMass <= emptyTotalMass) return;
        totalMass -= massBurnRate * dt * currentThrust / 100;
        currentFuel = totalMass - emptyTotalMass;
        currentFuelRatio = currentFuel / initialFuel;
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
        if(this.transform.childCount > 0 && !isInitialDataSet)
        {
            cores = GameObject.FindGameObjectsWithTag("Core");
            tanks = GameObject.FindGameObjectsWithTag("Fuel");
            engines = GameObject.FindGameObjectsWithTag("Engine");

            foreach (GameObject core in cores)
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
                engineThrust = engine.GetComponent<EngineData>().thrustPower;
                engineBurnTime = engine.GetComponent<EngineData>().burnRate;
            }

            totalMass = coreMass + tankMass + engineMass;
            emptyTotalMass = coreMass + emptyTankMass + engineMass;
            massBurnRate = (totalMass - emptyTotalMass) / engineBurnTime;
            rocketRigidbody.mass = totalMass;
            initialFuel = totalMass - emptyTotalMass;
            isInitialDataSet = true;
        }

        if (SceneManager.GetActiveScene().name == "FlightMode")
        {
            if (Input.GetKeyDown(KeyCode.Space))
                isStageActive = true;

            if (Input.GetKeyDown(KeyCode.Z))
                currentThrust = engineThrust;
            if (Input.GetKeyDown(KeyCode.X))
                currentThrust = 0;
            if (Input.GetKey(KeyCode.LeftShift) && currentThrust < engineThrust)
                currentThrust += engineThrust * 0.025f;
            if (Input.GetKey(KeyCode.LeftControl) && currentThrust > 0)
                currentThrust -= engineThrust * 0.025f;
            if (currentThrust < 0)
                currentThrust = 0;
            if (currentThrust > engineThrust)
                currentThrust = engineThrust;

            float throttleVal = currentThrust / engineThrust * 100;
            throttle = throttleVal.ToString("n0") + " %";
        }
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "FlightMode")
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
            }
            if (!Mathf.Approximately(yaw, 0f))
            {
                rocketRigidbody.freezeRotation = true;
                transform.Rotate(Vector3.forward * yaw * rotationSpeed * Time.deltaTime);
            }
            if (!Mathf.Approximately(roll, 0f))
            {
                rocketRigidbody.freezeRotation = true;
                transform.Rotate(Vector3.up * roll * rotationSpeed * Time.deltaTime);
            }
            rocketRigidbody.freezeRotation = false;
        }
    }
}
