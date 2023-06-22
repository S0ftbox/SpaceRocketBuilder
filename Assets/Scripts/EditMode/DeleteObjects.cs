using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//deleting objects from construction area in Editor mode
public class DeleteObjects : MonoBehaviour
{
    public GameObject parent, rootPrefab;
    public InputField rocketName;
    public Button removeButton;

    //listener for the button to removing the game objects
    void Update()
    {
        removeButton.onClick.AddListener(() => Remove());
    }

    //method for removing the game objects - uses DestroyImmediate method with given root to delete
    void Remove()
    {
        while (parent.transform.childCount > 0)
        {
            DestroyImmediate(parent.transform.GetChild(0).gameObject);
        }
        GameObject root = Instantiate(rootPrefab);
        root.transform.SetParent(parent.transform);
        rocketName.text = "";
    }
}
