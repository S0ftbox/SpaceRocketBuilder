using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwitchTabs : MonoBehaviour
{
    Button cmdModule, fuelTank, liquidEngine, solidEngine, separator, chute;
    public PartsList partsList;
    public GameObject prefabButton;
    public RectTransform parentTab;
    public RocketPartActions partActions;
    float width;

    void Awake()
    {
        cmdModule = GameObject.Find("CommandModulesButton").GetComponent<Button>();
        fuelTank = GameObject.Find("FuelTanksButton").GetComponent<Button>();
        liquidEngine = GameObject.Find("LiquidFuelEnginesButton").GetComponent<Button>();
        solidEngine = GameObject.Find("SolidFuelEnginesButton").GetComponent<Button>();
        separator = GameObject.Find("SeparatorsButton").GetComponent<Button>();
        chute = GameObject.Find("ChutesButton").GetComponent<Button>();
        width = parentTab.rect.width;
        parentTab.GetComponent<GridLayoutGroup>().cellSize = new Vector2(width / 3, width / 3);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (commandModule c in partsList.commandModules)
        {
            GameObject btn = (GameObject)Instantiate(prefabButton);
            btn.transform.SetParent(parentTab);
            Image img = btn.gameObject.transform.GetChild(0).GetComponent<Image>();
            img.sprite = c.icon;
            btn.GetComponent<Button>().onClick.AddListener(() => ButtonPressed(c.prefab));
            btn.AddComponent<ButtonActions>();
            btn.GetComponent<ButtonActions>().partName = c.name;
            btn.GetComponent<ButtonActions>().description = c.description;
        }
    }

    void Update()
    {
        cmdModule.onClick.AddListener(() => ShowButtons(cmdModule));
        fuelTank.onClick.AddListener(() => ShowButtons(fuelTank));
        liquidEngine.onClick.AddListener(() => ShowButtons(liquidEngine));
        solidEngine.onClick.AddListener(() => ShowButtons(solidEngine));
        separator.onClick.AddListener(() => ShowButtons(separator));
        chute.onClick.AddListener(() => ShowButtons(chute));
    }

    void ShowButtons(Button pressedBtn)
    {
        while (parentTab.transform.childCount > 0)
        {
            DestroyImmediate(parentTab.transform.GetChild(0).gameObject);
        }
        if (pressedBtn == cmdModule)
        {
            foreach (commandModule c in partsList.commandModules)
            {
                GameObject btn = (GameObject)Instantiate(prefabButton);
                btn.transform.SetParent(parentTab);
                Image img = btn.gameObject.transform.GetChild(0).GetComponent<Image>();
                img.sprite = c.icon;
                btn.GetComponent<Button>().onClick.AddListener(() => ButtonPressed(c.prefab));
                btn.AddComponent<ButtonActions>();
                btn.GetComponent<ButtonActions>().partName = c.name;
                btn.GetComponent<ButtonActions>().description = c.description;
            }
        }
        if (pressedBtn == fuelTank)
        {
            foreach (fuelTank f in partsList.fuelTanks)
            {
                GameObject btn = (GameObject)Instantiate(prefabButton);
                btn.transform.SetParent(parentTab);
                Image img = btn.gameObject.transform.GetChild(0).GetComponent<Image>();
                img.sprite = f.icon;
                btn.GetComponent<Button>().onClick.AddListener(() => ButtonPressed(f.prefab));
                btn.AddComponent<ButtonActions>();
                btn.GetComponent<ButtonActions>().partName = f.name;
                btn.GetComponent<ButtonActions>().description = f.description;
            }
        }
        if (pressedBtn == liquidEngine)
        {
            foreach (liquidFuelEngine l in partsList.liquidFuelEngines)
            {
                GameObject btn = (GameObject)Instantiate(prefabButton);
                btn.transform.SetParent(parentTab);
                Image img = btn.gameObject.transform.GetChild(0).GetComponent<Image>();
                img.sprite = l.icon;
                btn.GetComponent<Button>().onClick.AddListener(() => ButtonPressed(l.prefab));
                btn.AddComponent<ButtonActions>();
                btn.GetComponent<ButtonActions>().partName = l.name;
                btn.GetComponent<ButtonActions>().description = l.description;
            }
        }
        if (pressedBtn == solidEngine)
        {
            foreach (solidFuelEngine s in partsList.solidFuelEngines)
            {
                GameObject btn = (GameObject)Instantiate(prefabButton);
                btn.transform.SetParent(parentTab);
                Image img = btn.gameObject.transform.GetChild(0).GetComponent<Image>();
                img.sprite = s.icon;
                btn.GetComponent<Button>().onClick.AddListener(() => ButtonPressed(s.prefab));
                btn.AddComponent<ButtonActions>();
                btn.GetComponent<ButtonActions>().partName = s.name;
                btn.GetComponent<ButtonActions>().description = s.description;
            }
        }
        if (pressedBtn == separator)
        {
            foreach (separator s in partsList.separators)
            {
                GameObject btn = (GameObject)Instantiate(prefabButton);
                btn.transform.SetParent(parentTab);
                Image img = btn.gameObject.transform.GetChild(0).GetComponent<Image>();
                img.sprite = s.icon;
                btn.GetComponent<Button>().onClick.AddListener(() => ButtonPressed(s.prefab));
                btn.AddComponent<ButtonActions>();
                btn.GetComponent<ButtonActions>().partName = s.name;
                btn.GetComponent<ButtonActions>().description = s.description;
            }
        }
        if (pressedBtn == chute)
        {
            foreach (chute c in partsList.chutes)
            {
                GameObject btn = (GameObject)Instantiate(prefabButton);
                btn.transform.SetParent(parentTab);
                Image img = btn.gameObject.transform.GetChild(0).GetComponent<Image>();
                img.sprite = c.icon;
                btn.GetComponent<Button>().onClick.AddListener(() => ButtonPressed(c.prefab));
                btn.AddComponent<ButtonActions>();
                btn.GetComponent<ButtonActions>().partName = c.name;
                btn.GetComponent<ButtonActions>().description = c.description;
            }
        }
    }

    void ButtonPressed(GameObject part)
    {
        partActions.AddPart(part);
    }
}
