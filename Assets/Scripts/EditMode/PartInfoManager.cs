using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartInfoManager : MonoBehaviour
{
    public GameObject prefabInfo;

    public static Action<string, string, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;

    private void OnEnable()
    {
        OnMouseHover += ButtonHovered;
        OnMouseLoseFocus += ButtonUnhovered;
    }

    private void OnDisable()
    {
        OnMouseHover -= ButtonHovered;
        OnMouseLoseFocus -= ButtonUnhovered;
    }

    private void Start()
    {
        ButtonUnhovered();
    }

    public void ButtonHovered(string partName, string description, Vector2 mousePosition)
    {
        prefabInfo.SetActive(true);
        prefabInfo.transform.position = new Vector2(mousePosition.x+105, mousePosition.y-155);
        Text nameText = prefabInfo.gameObject.transform.GetChild(0).GetComponent<Text>();
        Text descriptionText = prefabInfo.gameObject.transform.GetChild(1).GetComponent<Text>();
        nameText.text = partName;
        descriptionText.text = description;
    }

    void ButtonUnhovered()
    {
        prefabInfo.SetActive(false);
    }
}
