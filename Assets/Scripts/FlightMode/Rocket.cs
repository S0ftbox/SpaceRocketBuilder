using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    public Rigidbody rocketRigidbody;
    public float totalMass, emptyTotalMass, currentFuelRatio;
    public float currentThrust, engineThrust, engineBurnTime;
    public float massBurnRate;
    public float rotationSpeed = 20f;
    public float pitch, roll, yaw;
    public bool isStageActive = false;
    public bool isStageSolidFuel = false;
    public string throttle;
    float initialFuel, currentFuel;
    public bool isInitialDataSet = false;
    public bool hasCoreModule = false;
    public int totalStageCount, currentStage, totalCrewCount;
    
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

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "FlightMode")
        {
            WaitSecond();
        }
    }

    private void Update()
    {
        if(this.transform.childCount > 0 && !isInitialDataSet)
        {
            for(int i = 0; i < transform.childCount-1; i++)
            {
                totalMass += transform.GetChild(i).GetComponent<StageData>().totalMass;
                emptyTotalMass += transform.GetChild(i).GetComponent<StageData>().totalMass;
                if (transform.GetChild(i).GetComponent<StageData>().hasCoreModule)
                {
                    hasCoreModule = true;
                    totalCrewCount += transform.GetChild(i).GetComponent<StageData>().crewCount;
                }
            }
            totalMass += transform.GetChild(transform.childCount - 1).GetComponent<StageData>().totalMass;
            emptyTotalMass += transform.GetChild(transform.childCount - 1).GetComponent<StageData>().dryMass;
            engineThrust = transform.GetChild(transform.childCount - 1).GetComponent<StageData>().thrustPower;
            engineBurnTime = transform.GetChild(transform.childCount - 1).GetComponent<StageData>().burnRate;
            if (transform.GetChild(transform.childCount - 1).GetComponent<StageData>().hasCoreModule)
            {
                hasCoreModule = true;
                totalCrewCount += transform.GetChild(transform.childCount - 1).GetComponent<StageData>().crewCount;
            }

            massBurnRate = (totalMass - emptyTotalMass) / engineBurnTime;
            rocketRigidbody.mass = totalMass;
            initialFuel = totalMass - emptyTotalMass;
            totalStageCount = transform.childCount;
            currentStage = totalStageCount;
            isInitialDataSet = true;
        }

        if (SceneManager.GetActiveScene().name == "FlightMode" && hasCoreModule)
        {
            if (Input.GetKeyDown(KeyCode.Space) && totalStageCount > 0)
            {
                if(currentStage != totalStageCount && currentStage != 0)
                {
                    //stage separation
                    transform.GetChild(currentStage).GetComponent<StageData>().startVelocity = GetComponent<Rigidbody>().velocity;
                    transform.GetChild(currentStage).GetComponent<StageData>().startAngularVelocity = GetComponent<Rigidbody>().angularVelocity;
                    transform.GetChild(currentStage).parent = null;
                }
                transform.GetChild(currentStage - 1).GetComponent<StageData>().isStageActive = true;
                isStageActive = transform.GetChild(currentStage - 1).GetComponent<StageData>().isStageActive;
                emptyTotalMass += transform.GetChild(currentStage - 1).GetComponent<StageData>().dryMass;
                engineThrust = transform.GetChild(currentStage - 1).GetComponent<StageData>().thrustPower;
                currentStage -= 1;
            }
                
            if(engineThrust != 0)
            {
                if (Input.GetKeyDown(KeyCode.Z) && !isStageSolidFuel)
                    currentThrust = engineThrust;
                if (Input.GetKeyDown(KeyCode.X) && !isStageSolidFuel)
                    currentThrust = 0;
                if (Input.GetKey(KeyCode.LeftShift) && currentThrust < engineThrust && !isStageSolidFuel)
                    currentThrust += engineThrust * 0.025f;
                if (Input.GetKey(KeyCode.LeftControl) && currentThrust > 0 && !isStageSolidFuel)
                    currentThrust -= engineThrust * 0.025f;
                if (currentThrust < 0)
                    currentThrust = 0;
                if (currentThrust > engineThrust)
                    currentThrust = engineThrust;

                float throttleVal = currentThrust / engineThrust * 100;
                throttle = throttleVal.ToString("n0") + " %";
            }
            else
            {
                throttle = "0%";
                currentThrust = 0;
            }
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

            yaw = Input.GetAxis("Horizontal") * -1;
            pitch = Input.GetAxis("Vertical") * -1;
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

    IEnumerator WaitSecond()
    {
        rocketRigidbody.isKinematic = true;
        yield return new WaitForSeconds(1);
        rocketRigidbody.isKinematic = false;
    }
}
