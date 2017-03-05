using UnityEngine;
using System.Collections;
using System;

public class Game : MonoBehaviour
{
    public Main main;

    public Board board;
    public Grid grid;
    public FiguresPool figuresPool;
    [NonSerialized]
    public Figure currentFigure;
    public GameObject nextFigureSpawnPoint;
    public GameTick gameTick;

    private IEnumerator dropCoroutine;

    [NonSerialized]
    public bool blockInput = true;

    [NonSerialized]
    public int points = 0;
    private int maxPoints = 999999;

    private bool paused = false;

    void Awake()
    {
        figuresPool.Initialize();
        board.Initialize();
        grid.Initialize(board);

        gameTick.tickAction = () =>
        {
            MoveUp();
        };
    }

    public void Clear()
    {
        if (dropCoroutine != null)
            StopCoroutine(dropCoroutine);
        board.Clear();
        figuresPool.Clear();
        SpawnFigure();
        blockInput = false;
        paused = false;
        points = 0;
        gameTick.Run();
    }

    public void Pause()
    {
        blockInput = true;
        paused = true;
        board.Pause();
        gameTick.Stop();
    }

    public void Resume()
    {
        blockInput = false;
        paused = false;
        board.Resume();
        gameTick.Resume();
    }

    private void SpawnFigure()
    {
        figuresPool.GetFigures();
        currentFigure = figuresPool.currentFigure;
        Figure nextFigure = figuresPool.nextFigure;

        nextFigure.transform.position = nextFigureSpawnPoint.transform.position;
        currentFigure.transform.position = board.spawnPoint;
        currentFigure.UpdateVectors();
        if (!board.CheckBoard(currentFigure.vects))
        {
            main.GameEnd();
            main.ShowDelayedGameOver();
        }    
    }

    private void AddPoints(int linesRemoved)
    {
        if (linesRemoved != 0)
        {
            points += linesRemoved;
            if (linesRemoved == 3)
                points += 1;
            else if (linesRemoved == 4)
                points += 3;

            if (points > maxPoints)
                points = maxPoints;
            main.UpdatePoints(points);
        }
    }

    public void RotateLeft()
    {
        if (!blockInput)
        {
            currentFigure.RotateLeft();
            if (!board.CheckSideBorders(currentFigure.vects)
                || !board.CheckTopBorder(currentFigure.vects)
                || !board.CheckBoard(currentFigure.vects))
            {
                currentFigure.RotateRight();
            }
        }
    }

    public void RotateRight()
    {
        if (!blockInput)
        {
            currentFigure.RotateRight();
            if (!board.CheckSideBorders(currentFigure.vects)
                || !board.CheckTopBorder(currentFigure.vects)
                || !board.CheckBoard(currentFigure.vects))
                currentFigure.RotateLeft();
        }
    }

    public bool MoveUp()
    {
        bool moveToBoard = false;
        if (!blockInput)
        {
            currentFigure.MoveUp();
            if (!board.CheckTopBorder(currentFigure.vects)
                || !board.CheckBoard(currentFigure.vects))
            {
                gameTick.Stop();
                blockInput = true;
                currentFigure.MoveDown();
                board.MoveToStack(currentFigure.vects);
                int removedLines = board.TryToRemoveFullRows();
                AddPoints(removedLines);
                figuresPool.ReturnToPool(currentFigure);
                SpawnFigure();
                moveToBoard = true;
            }
            else
                gameTick.RestoreTickTime();
        }
        return moveToBoard;
    }

    public void FinishMoveUp()
    {
        blockInput = false;
        gameTick.RestoreTickTime();
        gameTick.Resume();
    }

    public void MoveLeft()
    {
        if (!blockInput)
        {
            currentFigure.MoveLeft();
            if (!board.CheckSideBorders(currentFigure.vects)
                || !board.CheckBoard(currentFigure.vects))
                currentFigure.MoveRight();
        }
    }

    public void MoveRight()
    {
        if (!blockInput)
        {
            currentFigure.MoveRight();
            if (!board.CheckSideBorders(currentFigure.vects)
                || !board.CheckBoard(currentFigure.vects))
                currentFigure.MoveLeft();
        }
    }

    public void Drop()
    {
        if (!blockInput)
        {
            gameTick.Stop();

            if (dropCoroutine != null)
                StopCoroutine(dropCoroutine);
            dropCoroutine = DropCoroutine();
            StartCoroutine(dropCoroutine);
        }
    }

    private IEnumerator DropCoroutine()
    {
        while (true)
        {
            if (!paused)
            {
                if (MoveUp())
                    break;
            }
            yield return null;
        }
    }
}
