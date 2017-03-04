﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverScreen : GenericScreen, IPointerDownHandler
{
    public Text finalText;

    public void ReturnToMenu()
    {
        gui.Next(GraphicalUI.Transitions.GameOverToMenu);
        Main.I.mainCamera.SetMenuView();
    }

    public void UpdateFinalText(int points, float seconds)
    {
        string time = Statics.SecondsToTime(seconds);
        finalText.text = string.Format("Total points: {0:000000}\nTotal time: {1}", points, time);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ReturnToMenu();
    }
}

