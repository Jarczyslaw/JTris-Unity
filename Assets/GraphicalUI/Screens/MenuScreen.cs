using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuScreen : GenericScreen, IPointerDownHandler
{
    public GameObject lastInfo;
    public Text lastScoreText;
    public Text lastTimeText;
    public Text versionText;
    public Toast toast;

    public Table highscores;
    public Table statistics;

    private bool exitConfirmed;
    private int clearCounter;
    private readonly int clearStartValue = 3;

    public override void Init()
    {
        lastInfo.SetActive(false);
        highscores.Init();
        statistics.Init();
   
        versionText.text = Application.version;
    }

    public override void Show()
    {
        toast.Hide();
        exitConfirmed = false;
        clearCounter = clearStartValue;
        base.Show();
    }

    public void Exit()
    {
        if (!exitConfirmed)
        {
            toast.Show("Press again to exit...");
            exitConfirmed = true;
        }
        else
            Main.I.AppQuit();
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

    public void ClearButtonClick()
    {
        if(clearCounter <= 0)
        {
            Main.I.highscores.Clear();
            Main.I.statistics.Clear();
            clearCounter = clearStartValue;
            toast.Show("Data cleared!");
        } 
        else
        {
            toast.Show(string.Format("Press {0} times to clear game data...", clearCounter));
            clearCounter--;
        }
    }

    public void CloseButton()
    {
        Exit();
    }

    public void ShowLastGame(int score, float time)
    {
        lastInfo.SetActive(true);
        lastScoreText.text = score.ToString();
        lastTimeText.text = Statics.SecondsToTime(time);
    }

    public void UpdateHighscores()
    {
        var records = Main.I.highscores.ToArray();
        highscores.FillTable(records);
    }

    public void UpdateStatistics()
    {
        var stats = Main.I.statistics.ToArray();
        statistics.FillTable(stats);
    }
}


