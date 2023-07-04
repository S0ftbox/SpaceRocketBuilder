using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelect : MonoBehaviour
{
    Renderer objectRenderer;
    Color originalColor;
    public Color emissionColor;
    bool isHovering;

    private void Start()
    {
        // Get the Renderer component of the object
        objectRenderer = GetComponent<Renderer>();

        // Store the original material color
        originalColor = objectRenderer.material.GetColor("_EmissionColor");
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
                    SceneManager.LoadScene("EditMode");
                }
                if(gameObject.name == "LaunchPad")
                {
                    Debug.LogError("WIP");
                }
            }
        }
        else
        {
            // Reset the emission color to the original color when not hovering
            objectRenderer.material.SetColor("_EmissionColor", originalColor);
        }
    }
}
