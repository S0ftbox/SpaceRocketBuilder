using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonActions : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string partName, description;
    float timeToWait = 0.5f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(Timer());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        PartInfoManager.OnMouseLoseFocus();
    }

    private void ShowInfo()
    {
        PartInfoManager.OnMouseHover(partName, description, Input.mousePosition);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeToWait);
        ShowInfo();
    }
}
