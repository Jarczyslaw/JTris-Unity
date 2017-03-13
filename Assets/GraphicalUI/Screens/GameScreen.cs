using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScreen : GenericScreen
{
    public TouchKeypad touchKeypad;

    public Text pointsText;
    public Text timeText;
    public Button pauseButton;
    public GameObject nextText;
    public Text topScoreText;
    public Text linesText;
    public Text levelText;

    public override void Show()
    {
        SetPauseButtonState(true);
        nextText.SetActive(true);
        touchKeypad.Init();
        base.Show();
    }

    public override void Hide()
    {
        nextText.SetActive(false);
        base.Hide();
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

    public void UpdateTopScoreText(int topScore)
    {
        topScoreText.text = topScore.ToString().PadLeft(6, '0');
    } 

    public void UpdateLinesText(int lines)
    {
        linesText.text = lines.ToString().PadLeft(6, '0');
    }

    public void UpdateLevelText(int level)
    {
        levelText.text = level.ToString().PadLeft(6, '0');
    }

    public void UpdateOnStart(int points, float time, int lines, int level)
    {
        UpdatePoints(points);
        UpdateTime(time);
        UpdateLinesText(lines);
        UpdateLevelText(level);
    }
}


