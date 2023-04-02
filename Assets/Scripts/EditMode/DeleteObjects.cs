using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteObjects : MonoBehaviour
{
    public GameObject parent;
    public Button removeButton;

    void Update()
    {
        removeButton.onClick.AddListener(() => Remove());
    }

    void Remove()
    {
        while (parent.transform.childCount > 0)
        {
            DestroyImmediate(parent.transform.GetChild(0).gameObject);
        }
    }
}
