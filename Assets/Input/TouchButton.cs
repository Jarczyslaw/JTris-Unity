using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pressed = false;
    private bool down = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
        down = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }

    public void Init()
    {
        pressed = false;
        down = false;
    }

    public bool GetButton()
    {
        return pressed;
    }

    public bool GetButtonDown()
    {
        bool temp = down;
        down = false;
        return temp;
    }
}
