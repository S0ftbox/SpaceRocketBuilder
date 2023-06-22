using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//hover over buttons with parts in Editor mode manager
public class ButtonActions : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string partName, description;
    float timeToWait = 0.5f;

    //called when cursor is moved above the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(Timer());
    }

    //called when cursor is moved away
    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        PartInfoManager.OnMouseLoseFocus();
    }

    //calls the OnMouseHover method in PartInfoManager
    private void ShowInfo()
    {
        PartInfoManager.OnMouseHover(partName, description, Input.mousePosition);
    }

    //simple Enumerator function with 0,5s timer
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeToWait);
        ShowInfo();
    }
}
