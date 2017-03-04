using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : Singleton<Main>
{
    public Game game;
    public MainCamera mainCamera;
    public Timer timer;

    private IEnumerator gameEndCoroutine;
    private bool gameEndDelayCancel = false;

    private void Start()
    {
        timer.updateEvent += UpdateTime;
        mainCamera.SetMenuView();
    }

    public void GameStart()
    {
        Debug.Log("START");
        game.Clear();
        UpdatePoints(game.points);
        UpdateTime(0f);
        mainCamera.SetGameView();
        timer.Run();
    }

    public void Pause()
    {
        Debug.Log("PAUSE");
        game.Pause();
        timer.Stop();
    }

    public void Resume()
    {
        Debug.Log("RESUME");
        game.Resume();
        timer.Resume();
    }

    public void GameEnd()
    {
        Debug.Log("END");
        GraphicalUI.I.game.SetPauseButtonState(false);
        game.blockInput = true;
        game.gameTick.Stop();
        timer.Stop();

        GraphicalUI.I.gameOver.UpdateFinalText(game.points, timer.value);
    }

    public void ShowDelayedGameOver()
    {
        gameEndDelayCancel = false;
        if (gameEndCoroutine != null)
            StopCoroutine(gameEndCoroutine);
        gameEndCoroutine = GameEndCoroutine();
        StartCoroutine(gameEndCoroutine);
    }

    private IEnumerator GameEndCoroutine()
    {
        float timeAccu = 1f;
        while(true)
        {
            if (gameEndDelayCancel)
                break;
            timeAccu -= Time.deltaTime;
            if (timeAccu < 0f)
                break;
            yield return null;
        }
        GraphicalUI.I.Next(GraphicalUI.Transitions.GameToGameOver);
    }

    public void UpdateTime(float seconds)
    {
        GraphicalUI.I.game.UpdateTime(seconds);
    }

    public void UpdatePoints(int points)
    {
        GraphicalUI.I.game.UpdatePoints(points);
    }
}
