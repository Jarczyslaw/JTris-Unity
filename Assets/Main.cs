using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Main : Singleton<Main>
{
    public Game game;
    public MainCamera mainCamera;
    public Timer timer;

    public Statistics statistics;
    public Highscores highscores;

    private IEnumerator gameOverCoroutine;
    [NonSerialized]
    public bool delayedGameOverEnabled = false;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        highscores = new Highscores();
        statistics = new Statistics();
    }

    private void Start()
    {
        game.gameTick.intervalChangeAction += (level) =>
        {
            GraphicalUI.I.game.UpdateLevelText(level);
        };
        timer.updateEvent = (time) =>
        {
            GraphicalUI.I.game.UpdateTime(time);
        };
        mainCamera.SetMenuView();

        GraphicalUI.I.menu.UpdateHighscores();
        GraphicalUI.I.menu.UpdateStatistics();
    }

    public void AppQuit()
    {
        Debug.Log("App quit");
        Application.Quit();
    }

    public void GameStart()
    {
        game.Clear();
        mainCamera.SetGameView();
        timer.Run();

        GraphicalUI.I.game.UpdateTopScoreText(highscores.GetTopScore());
        GraphicalUI.I.game.UpdateOnStart(game.points, timer.value, game.lines, game.gameTick.level);
    }

    public void Pause()
    {
        game.Pause();
        timer.Stop();
    }

    public void Resume()
    {
        game.Resume();
        timer.Resume();
    }

    public void GameEnd()
    {
        GraphicalUI.I.game.SetPauseButtonState(false);
        game.blockInput = true;
        game.gameTick.Stop();
        timer.Stop();

        bool newRecord = highscores.AddScore(game.points);
        if (newRecord)
            GraphicalUI.I.menu.UpdateHighscores();

        statistics.AddGame(game.points, timer.value);
        GraphicalUI.I.menu.UpdateStatistics();

        GraphicalUI.I.gameOver.UpdateFinalText(game.points, timer.value);
        GraphicalUI.I.gameOver.SetNewRecordText(newRecord);
        GraphicalUI.I.menu.ShowLastGame(game.points, timer.value);
    }

    public void ShowDelayedGameOver()
    {
        delayedGameOverEnabled = true;
        if (gameOverCoroutine != null)
            StopCoroutine(gameOverCoroutine);
        gameOverCoroutine = GameEndCoroutine();
        StartCoroutine(gameOverCoroutine);
    }

    private IEnumerator GameEndCoroutine()
    {
        float timeAccu = 2f;
        while(true)
        {
            if (!delayedGameOverEnabled)
                break;
            timeAccu -= Time.deltaTime;
            if (timeAccu < 0f)
                break;
            yield return null;
        }
        GraphicalUI.I.Next(GraphicalUI.Transitions.GameToGameOver);
    }

    public void UpdatePointsAndLines(int points, int lines)
    {
        GraphicalUI.I.game.UpdatePoints(points);
        GraphicalUI.I.game.UpdateLinesText(lines);
    }
}
