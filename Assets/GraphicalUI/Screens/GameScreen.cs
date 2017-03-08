using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScreen : GenericScreen
{
    public TouchKeypad touchKeypad;

    public Text pointsText;
    public Text timeText;
    public Button pauseButton;

    public override void Show()
    {
        SetPauseButtonState(true);
        touchKeypad.Init();
        base.Show();
    }

    public void PauseButtonClick()
    {
        gui.Next(GraphicalUI.Transitions.GameToPause);
        Main.I.Pause();
    }

    public void UpdateTime(float seconds)
    {
        timeText.text = Statics.SecondsToTime(seconds);
    }

    public void UpdatePoints(int points)
    {
        string p = points.ToString().PadLeft(6, '0');
        pointsText.text = p;
    }

    public void SetPauseButtonState(bool enabled)
    {
        pauseButton.interactable = enabled;
    }
}


