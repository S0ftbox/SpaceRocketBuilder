using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeSelect : MonoBehaviour
{
    Renderer objectRenderer;
    Color originalColor;
    public Color emissionColor;
    public GameObject savesList;
    public GameObject empty, node, stage, prefabButton;
    public Button cancel, load, delete;
    public PartsList partsList;
    public StageManager stages;
    public GameObject loadingScreen, loadingIndicator, loadingImage;
    public Texture[] textures;
    bool isHovering;
    int stageCount, percent;
    string selectedFilePath;
    string[] existingFiles;
    
    private void Start()
    {
        // Get the Renderer component of the object
        objectRenderer = GetComponent<Renderer>();

        // Store the original material color
        originalColor = objectRenderer.material.GetColor("_EmissionColor");

        //panel with saved rocket models
        savesList.SetActive(false);
    }

    private void OnMouseEnter()
    {
        // Enable emission when mouse enters the object
        objectRenderer.material.EnableKeyword("_EMISSION");
        isHovering = true;
    }

    private void OnMouseExit()
    {
        // Disable emission when mouse exits the object
        objectRenderer.material.DisableKeyword("_EMISSION");
        isHovering = false;
    }

    void Update()
    {
        // Update the emission intensity/color when hovering
        if (isHovering)
        {
            // Apply the new emission color to the material
            objectRenderer.material.SetColor("_EmissionColor", emissionColor);
            if (Input.GetMouseButtonDown(0))
            {
                if (gameObject.name == "VAB")
                {
                    AsyncLoadScene("EditMode");
                }
                if(gameObject.name == "LaunchPad")
                {
                    OpenFilesList();
                    cancel.onClick.AddListener(() => CloseFilesList());
                    load.onClick.AddListener(() => LoadFlightMode());
                    delete.onClick.AddListener(() => DeleteFile(selectedFilePath));
                }
            }
        }
        else
        {
            // Reset the emission color to the original color when not hovering
            objectRenderer.material.SetColor("_EmissionColor", originalColor);
        }
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
        if (GameObject.Find("Rocket").transform.childCount != 0)
        {
            DestroyImmediate(GameObject.Find("Rocket").transform.GetChild(0).gameObject);
        }

        //create empty game object which will be the root of the rocket, set the parent, name and the position
        GameObject savedRocket = Instantiate(empty);
        savedRocket.transform.SetParent(GameObject.Find("Rocket").transform);
        savedRocket.name = rocketData.name;
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
            rocketChild = Instantiate(stage);
            stageCount++;
        }
        //Other tags - create temporary game object by searching the PartsList structure using getPrefab method, then instantiate this game object and add it to a StageManager stages list
        else
        {

            GameObject partToInstantiate = partsList.getPrefab(childData.name);
            rocketChild = Instantiate(partToInstantiate);
            string lastPart = partToInstantiate.name;
            //stages.stages[stageCount - 1].Add(partToInstantiate);
            if (childData.tag == "Separator")
            {
                ignoreChildren = true;
                //string lastPart = stages.stages[stageCount - 2].Last().name;
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
        while (savesList.transform.GetChild(0).GetChild(0).childCount > 0)
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

    void LoadFlightMode()
    {
        LoadRocketFromFile(selectedFilePath);
        AsyncLoadScene("FlightMode");
    }

    public async void AsyncLoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        loadingImage.GetComponent<RawImage>().texture = textures[UnityEngine.Random.Range(0, 2)];
        loadingScreen.SetActive(true);

        do
        {
            await Task.Delay(1);
            if (loadingIndicator != null)
            {
                percent = (int)(scene.progress * 100);
                loadingIndicator.GetComponent<Text>().text = percent.ToString() + "%";
            }
        } while (scene.progress < 0.9f);

        loadingScreen.SetActive(false);
        scene.allowSceneActivation = true;
    }
}
