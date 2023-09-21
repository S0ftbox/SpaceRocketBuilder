using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePanel : MonoBehaviour
{
    public Button closeButton;

    void Update()
    {
        closeButton.onClick.AddListener(() => ClosePanelButton());
    }

    void ClosePanelButton()
    {
        gameObject.SetActive(false);
    }
}
