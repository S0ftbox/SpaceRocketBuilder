using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

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

public class SaveLoad : MonoBehaviour
{
    public Button save, load;
    public GameObject empty, node, stage;
    GameObject rocket;
    public PartsList partsList;

    void Start()
    {
        rocket = GameObject.Find("Rocket").transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if(rocket != null)
        {
            save.onClick.AddListener(() => SaveRocketToFile(rocket, "Assets/SavedRockets/untitled.bin"));
        }
        //save.onClick.RemoveAllListeners();

        load.onClick.AddListener(() => LoadRocketFromFile("Assets/SavedRockets/untitled.bin"));
        //load.onClick.RemoveAllListeners();
    }

    void SaveRocketToFile(GameObject rocketParent, string filePath)
    {
        RocketData savedRocket = new RocketData();
        savedRocket.name = rocketParent.name;
        savedRocket.positionX = rocketParent.transform.position.x;
        savedRocket.positionY = rocketParent.transform.position.y;
        savedRocket.positionZ = rocketParent.transform.position.z;
        savedRocket.prefabPath = PrefabStageUtility.GetCurrentPrefabStage()?.assetPath;

        savedRocket.children = new List<RocketData>();
        foreach (Transform child in rocketParent.transform)
        {
            SaveRocketChild(child.gameObject, savedRocket.children);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream rocketFile = File.Create(filePath);
        bf.Serialize(rocketFile, savedRocket);
        rocketFile.Close();
    }

    void SaveRocketChild(GameObject rocketChild, List<RocketData> parentChildren)
    {
        RocketData childData = new RocketData();
        childData.name = rocketChild.name;
        Debug.Log(childData.name.Length);
        //childData.name = childData.name.Remove(childData.name.Length - 7);
        childData.tag = rocketChild.tag;
        childData.prefabPath = PrefabStageUtility.GetCurrentPrefabStage()?.assetPath;
        childData.positionX = rocketChild.transform.position.x;
        childData.positionY = rocketChild.transform.position.y;
        childData.positionZ = rocketChild.transform.position.z;
        if (rocketChild.tag == "Core")
        {
            childData.mass = rocketChild.GetComponent<CoreData>().mass;
            childData.isManned = rocketChild.GetComponent<CoreData>().isManned;
        }
        if(rocketChild.tag == "Fuel")
        {
            childData.mass = rocketChild.GetComponent<TankData>().dryMass;
            childData.massWithFuel = rocketChild.GetComponent<TankData>().massWithFuel;
        }
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

        childData.children = new List<RocketData>();
        foreach(Transform child in rocketChild.transform)
        {
            SaveRocketChild(child.gameObject, childData.children);
        }
        parentChildren.Add(childData);
    }

    GameObject LoadRocketFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return null;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream rocketFile = File.Open(filePath, FileMode.Open);
        RocketData rocketData = (RocketData)bf.Deserialize(rocketFile);
        rocketFile.Close();
        GameObject savedRocket = Instantiate(empty);
        savedRocket.transform.SetParent(GameObject.Find("Rocket").transform);
        savedRocket.name = rocketData.name;
        savedRocket.transform.position = new Vector3(rocketData.positionX, rocketData.positionY, rocketData.positionZ);
        
        foreach(RocketData childData in rocketData.children)
        {
            LoadRocketChild(childData, savedRocket.transform);
        }

        return savedRocket;
    }

    void LoadRocketChild(RocketData childData, Transform parentTransform)
    {
        GameObject rocketChild;
        if (childData.tag == "Node")
        {
            rocketChild = Instantiate(node);
        }
        else if (childData.tag == "Stage")
        {
            rocketChild = Instantiate(stage);
        }
        else
        {
            Debug.Log(childData.name);
            GameObject partToInstantiate = partsList.getPrefab(childData.name);
            rocketChild = Instantiate(partToInstantiate);
        }
        rocketChild.name = childData.name;
        rocketChild.transform.SetParent(parentTransform);
        rocketChild.transform.position = new Vector3(childData.positionX, childData.positionY, childData.positionZ);
        rocketChild.tag = childData.tag;
        /*if(rocketChild.tag == "Core")
        {
            //rocketChild.AddComponent
        }*/

        foreach (RocketData grandchild in childData.children)
        {
            LoadRocketChild(grandchild, rocketChild.transform);
        }
    }

    /*public void SaveObject(GameObject objectToSave, string savePath)
    {
        GameObject savedPrefab = PrefabUtility.SaveAsPrefabAsset(objectToSave, savePath);
    }

    public void LoadObject(string loadPath)
    {
        GameObject loadedPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(loadPath);
        GameObject innstantiatedPrefab = Instantiate(loadedPrefab);
    }*/
}
