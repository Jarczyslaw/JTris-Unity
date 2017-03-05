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
        string time = Statics.SecondsToTime(seconds);
        timeText.text = string.Format("Time: {0}", time);
    }

    public void UpdatePoints(int points)
    {
        pointsText.text = string.Format("Points: {0:000000}", points);
    }

    public void SetPauseButtonState(bool enabled)
    {
        pauseButton.interactable = enabled;
    }
}


