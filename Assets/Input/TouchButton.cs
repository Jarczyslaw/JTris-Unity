using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image buttonSprite;

    public Color unpressedColor;
    public Color pressedColor;

    private bool pressed = false;
    private bool down = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
        down = true;
        if (buttonSprite != null)
            buttonSprite.color = pressedColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
        if (buttonSprite != null)
            buttonSprite.color = unpressedColor;
    }

    public void Init()
    {
        pressed = false;
        down = false;
        if (buttonSprite != null)
            buttonSprite.color = unpressedColor;
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
