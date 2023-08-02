using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIStartTouch : MonoBehaviour,ISelectHandler,IDeselectHandler
{
    public GameObject ui;
    //private bool isHovering = false; // 标记是否正在悬浮
    void Start()
    {
        ui.SetActive(false);
    }
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("selected");
        ui.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("deselected");
        ui.SetActive(false);
    }
}
