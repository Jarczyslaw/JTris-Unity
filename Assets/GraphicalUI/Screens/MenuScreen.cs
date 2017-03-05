using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuScreen : GenericScreen, IPointerDownHandler
{
    public Text lastScoreText;

    public override void Init()
    {
        lastScoreText.enabled = false;
    }

    public void StartGame()
    {
        gui.Next(GraphicalUI.Transitions.MenuToGame);
        Main.I.GameStart();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartGame();
    }

    public void ShowLastScore(int score)
    {
        lastScoreText.enabled = true;
        lastScoreText.text = string.Format("Last score: {0}", score);
    }
}


