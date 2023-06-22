using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


//universal structure to store data about the saved rocket components
[System.Serializable]
public class RocketData
{
    public string name;
    public string prefabPath;
    public string tag;
    public float positionX;
    public float positionY;
    public float positionZ;
    public float mass;
    public float thrustPower;
    public float burnRate;
    public float massWithFuel;
    public float solidEngineMass;
    public bool solidFuel;
    public bool isManned;

    public List<RocketData> children;
}

//save and load manager
public class SaveLoad : MonoBehaviour
{
    public Button save, load, openList, deleteFile, cancel;
    public GameObject savesList;
    public InputField rocketInputName;
    public GameObject empty, node, stage, prefabButton;
    GameObject rocket;
    public PartsList partsList;
    public StageManager stages;
    public RocketPartActions actions;
    int stageCount;
    string rocketName;
    string selectedFilePath;
    string[] existingFiles;

    void Start()
    {
        //the root of the tree of rocket components
        rocket = GameObject.Find("Rocket").transform.GetChild(0).gameObject;
        //panel with saved rocket models
        savesList.SetActive(false);
    }

    void Update()
    {
        if(rocket != null)
        {
            //"save model" button listener activated when rocket game object isn't empty
            rocketName = rocketInputName.text;
            if (rocketInputName.text == "")
                rocketName = "UntitledVessel";
            save.onClick.AddListener(() => SaveRocketToFile(rocket, Application.persistentDataPath + "/SavedRockets/" + rocketName + ".bin"));
        }

        //"load model" button listener
        openList.onClick.AddListener(() => OpenFilesList());

        //"cancel" button listener
        cancel.onClick.AddListener(() => CloseFilesList());

        //"load" button listener
        load.onClick.AddListener(() => LoadRocketFromFile(selectedFilePath));

        //"delete" button listener
        deleteFile.onClick.AddListener(() => DeleteFile(selectedFilePath));
    }

    //method to save rocket components to a binary file
    //rocketParent - root of the rocket components
    //filePath - path to the location of saved file
    public void SaveRocketToFile(GameObject rocketParent, string filePath)
    {
        //create new RocketData object, store the name and position of root
        RocketData savedRocket = new RocketData();
        rocketParent.name = rocketName;
        savedRocket.name = rocketParent.name;
        savedRocket.positionX = rocketParent.transform.position.x;
        savedRocket.positionY = rocketParent.transform.position.y;
        savedRocket.positionZ = rocketParent.transform.position.z;

        //create new list of RocketData objects, each containing the children of root
        savedRocket.children = new List<RocketData>();
        foreach (Transform child in rocketParent.transform)
        {
            SaveRocketChild(child.gameObject, savedRocket.children);
        }

        //save the root RocketData variable into the file using serialization
        BinaryFormatter bf = new BinaryFormatter();
        FileStream rocketFile = File.Create(filePath);
        bf.Serialize(rocketFile, savedRocket);
        rocketFile.Close();
    }

    //method to save children rocket components (store them inside the root RocketData object to be exact)
    //rocketChild - current child game object
    //parentChildren - list of RocketData objects stored in youngest parent
    void SaveRocketChild(GameObject rocketChild, List<RocketData> parentChildren)
    {
        bool ignoreChildren = false;
        //create new RocketData object, store the name, tag and position of rocketChild object
        RocketData childData = new RocketData();
        childData.name = rocketChild.name;
        childData.tag = rocketChild.tag;
        childData.positionX = rocketChild.transform.position.x;
        childData.positionY = rocketChild.transform.position.y;
        childData.positionZ = rocketChild.transform.position.z;
        //following if statements are used to determine which variables of RocketData structure have to be modified by checking the object's tag
        //Core - set mass and isManned values
        if (rocketChild.tag == "Core")
        {
            childData.mass = rocketChild.GetComponent<CoreData>().mass;
            childData.isManned = rocketChild.GetComponent<CoreData>().isManned;
        }
        //Fuel - set dry mass and mass with fuel values
        if(rocketChild.tag == "Fuel")
        {
            childData.mass = rocketChild.GetComponent<TankData>().dryMass;
            childData.massWithFuel = rocketChild.GetComponent<TankData>().massWithFuel;
        }
        //Engine - set mass, burn rate, thrust power and solidFuel values
        //check is engine has solidFuel value set to True; if yes, then set dry mass, mass with fuel and engine mass values
        if(rocketChild.tag == "Engine")
        {
            childData.mass = rocketChild.GetComponent<EngineData>().mass;
            childData.burnRate = rocketChild.GetComponent<EngineData>().burnRate;
            childData.thrustPower = rocketChild.GetComponent<EngineData>().thrustPower;
            childData.solidFuel = rocketChild.GetComponent<EngineData>().solidFuel;
            if (childData.solidFuel)
            {
                childData.mass = rocketChild.GetComponent<TankData>().dryMass;
                childData.massWithFuel = rocketChild.GetComponent<TankData>().massWithFuel;
                childData.solidEngineMass = rocketChild.GetComponent<EngineData>().mass;
            }
        }
        if (rocketChild.tag == "Separator")
        {
            ignoreChildren = true;
        }

        //repeat the process of saving game objects using resursive calling the SaveRocketChild method with new lists of RocketData objects
        childData.children = new List<RocketData>();
        if (!ignoreChildren)
        {
            foreach (Transform child in rocketChild.transform)
            {
                SaveRocketChild(child.gameObject, childData.children);
            }
        }
        parentChildren.Add(childData);
    }

    //method to load rocket with its components from file
    //filePath - path of the file to load the rocket from
    //returns the savedRocket game object with the rocket components as it's children in a correct hierarchy
    public void LoadRocketFromFile(string filePath)
    {
        //read the data from the file into the RocketData object using deserialization method
        BinaryFormatter bf = new BinaryFormatter();
        FileStream rocketFile = File.Open(filePath, FileMode.Open);
        RocketData rocketData = (RocketData)bf.Deserialize(rocketFile);
        rocketFile.Close();

        //check if the construction area is empty and destroy the existing objects to prevent further errors
        if(GameObject.Find("Rocket").transform.childCount != 0)
        {
            DestroyImmediate(GameObject.Find("Rocket").transform.GetChild(0).gameObject);
        }

        //create empty game object which will be the root of the rocket, set the parent, name and the position
        GameObject savedRocket = Instantiate(empty);
        savedRocket.transform.SetParent(GameObject.Find("Rocket").transform);
        savedRocket.name = rocketData.name;
        rocketInputName.text = rocketData.name;
        savedRocket.transform.position = new Vector3(rocketData.positionX, rocketData.positionY, rocketData.positionZ);

        //using recursion load children components
        foreach (RocketData childData in rocketData.children)
        {
            LoadRocketChild(childData, savedRocket.transform);
        }
        //hide the panel with saved models
        savesList.SetActive(false);
        //remove created buttons with saved models
        while (savesList.transform.GetChild(0).GetChild(0).childCount > 0)
        {
            DestroyImmediate(savesList.transform.GetChild(0).GetChild(0).GetChild(0).gameObject);
        }
    }

    //method to load chilrden game objects
    //childData - RocketData object which is the child of youngest parent in hierarchy
    //parentTransform - Transform variable of the youngest parent
    void LoadRocketChild(RocketData childData, Transform parentTransform)
    {
        bool ignoreChildren = false;
        //create new gameobject
        //if statements are used to determine which action should be performed using the tag stored in one of childData's variable
        GameObject rocketChild;
        //Node - instantiate the game object using 'node' prefab
        if (childData.tag == "Node")
        {
            rocketChild = Instantiate(node);
        }
        //Stage - add new list of GameObjects to StageManager stages (this is needed to modify the loaded rocket later), instantiate the game object using 'stage' prefab and increment the stageCont value
        else if (childData.tag == "Stage")
        {
            stages.stages.Add(new List<GameObject>());
            rocketChild = Instantiate(stage);
            stageCount++;
        }
        //Other tags - create temporary game object by searching the PartsList structure using getPrefab method, then instantiate this game object and add it to a StageManager stages list
        else
        {
            
            GameObject partToInstantiate = partsList.getPrefab(childData.name);
            rocketChild = Instantiate(partToInstantiate);
            stages.stages[stageCount-1].Add(partToInstantiate);
            if (childData.tag == "Separator")
            {
                ignoreChildren = true;
                string lastPart = stages.stages[stageCount - 2].Last().name;
                if (lastPart == "LiquidEngineBig_1.25m")
                {
                    rocketChild.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (lastPart == "LiquidEngineSmall_1.25m")
                {
                    rocketChild.transform.GetChild(4).gameObject.SetActive(true);
                }
            }
            else
                ignoreChildren = false;
        }
        //set the created game object's name, parent, position and tag
        rocketChild.name = childData.name;
        rocketChild.transform.SetParent(parentTransform);
        rocketChild.transform.position = new Vector3(childData.positionX, childData.positionY, childData.positionZ);
        rocketChild.tag = childData.tag;

        //using recursion load children components
        if (!ignoreChildren)
        {
            foreach (RocketData grandchild in childData.children)
            {
                LoadRocketChild(grandchild, rocketChild.transform);
            }
        }
    }

    //method to activate the panel with existing saved models
    void OpenFilesList()
    {
        existingFiles = Directory.GetFiles(Application.persistentDataPath + "/SavedRockets/", "*.bin");
        if (savesList.transform.GetChild(0).GetChild(0).childCount == 0)
        {
            for (int i = 0; i < existingFiles.Length; i++)
            {
                GameObject btn = Instantiate(prefabButton);
                btn.transform.SetParent(savesList.transform.GetChild(0).GetChild(0));
                btn.transform.GetChild(0).GetComponent<Text>().text = existingFiles[i];
                string pathTmp = Application.persistentDataPath + "/SavedRockets/";
                int pathLen = pathTmp.Length;
                btn.transform.GetChild(0).GetComponent<Text>().text = btn.transform.GetChild(0).GetComponent<Text>().text.Remove(0, pathLen);
                btn.transform.GetChild(0).GetComponent<Text>().text = btn.transform.GetChild(0).GetComponent<Text>().text.Remove(btn.transform.GetChild(0).GetComponent<Text>().text.Length - 4);
                btn.GetComponent<Button>().onClick.AddListener(() => SetActivePath(btn.transform.GetChild(0).GetComponent<Text>().text));
            }
        }
        savesList.SetActive(true);
    }

    void SetActivePath(string filePath)
    {
        selectedFilePath = Application.persistentDataPath + "/SavedRockets/" + filePath + ".bin";
    }

    //method to deactivate the panel with existing saved models
    void CloseFilesList()
    {
        while(savesList.transform.GetChild(0).GetChild(0).childCount > 0)
        {
            DestroyImmediate(savesList.transform.GetChild(0).GetChild(0).GetChild(0).gameObject);
        }
        savesList.SetActive(false);
    }

    //method to delete existing file
    //filePath - path of the file to delete
    void DeleteFile(string filePath)
    {
        File.Delete(filePath);
        while (savesList.transform.GetChild(0).GetChild(0).childCount > 0)
        {
            DestroyImmediate(savesList.transform.GetChild(0).GetChild(0).GetChild(0).gameObject);
        }
        OpenFilesList();
    }
}
