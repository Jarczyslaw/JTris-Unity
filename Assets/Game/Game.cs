using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

    private Routine dropRoutine;
    private Routine removeRoutine;

    [NonSerialized]
    public bool blockInput = true;

    [NonSerialized]
    public int points = 0;
    private int maxPoints = 999999;
    [NonSerialized]
    public int lines = 0;

    private bool paused = false;

    void Awake()
    {
        dropRoutine = new Routine(this);
        removeRoutine = new Routine(this);

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
        dropRoutine.Stop();
        removeRoutine.Stop();
        board.Clear();
        figuresPool.Clear();
        SpawnFigure();
        blockInput = false;
        paused = false;
        points = 0;
        lines = 0;
        gameTick.Run();
    }

    public void Pause()
    {
        blockInput = true;
        paused = true;
        gameTick.Stop();
    }

    public void Resume()
    {
        blockInput = false;
        paused = false;
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
            lines += linesRemoved;
            points += linesRemoved;
            if (linesRemoved == 2)
                points += 1;
            else if (linesRemoved == 3)
                points += 2;
            else if (linesRemoved == 4)
                points += 4;

            if (points > maxPoints)
                points = maxPoints;
            main.UpdatePointsAndLines(points, lines);
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

    public List<int> TryToMoveUp()
    {
        currentFigure.MoveUp();
        if (!board.CheckTopBorder(currentFigure.vects) || !board.CheckBoard(currentFigure.vects))
        {
            currentFigure.MoveDown();
            board.MoveToStack(currentFigure.vects);
            List<int> rowsToRemove = board.GetFullRows();
            figuresPool.ReturnToPool(currentFigure);
            SpawnFigure();
            return rowsToRemove;
        }
        return null;
    }

    public void MoveUp()
    {
        if (!blockInput)
        {
            List<int> rowsToRemove = TryToMoveUp();
            if (rowsToRemove != null && rowsToRemove.Count != 0)
                Remove(rowsToRemove);
            else
                gameTick.RestoreTickTime();
        }
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
            dropRoutine.Run(DropCoroutine());
        }
    }

    private IEnumerator DropCoroutine()
    {  
        while (true)
        {
            if (!paused)
            {
                List<int> rowsToRemove = TryToMoveUp();
                if (rowsToRemove != null)
                {
                    if (rowsToRemove.Count != 0)
                        Remove(rowsToRemove);
                    else
                    {
                        gameTick.RestoreTickTime();
                        gameTick.Resume();
                    }
                    break;
                }    
            }
            yield return null;
        }
    }

    public void Remove(List<int> rowsToRemove)
    {
        gameTick.Stop();
        blockInput = true;
        AddPoints(rowsToRemove.Count);
        removeRoutine.Run(RemoveCoroutine(rowsToRemove));
    }

    private IEnumerator RemoveCoroutine(List<int> rowsToRemove)
    {
        bool nextState = false;
        int counter = 20;
        for (;;)
        {
            if (!paused)
            {
                board.SetRows(rowsToRemove, nextState);
                nextState = !nextState;

                counter--;
                if (counter < 0)
                    break;
            }
            yield return null;
        }
        board.SetRows(rowsToRemove, true);
        board.RemoveFullRows(rowsToRemove);

        gameTick.RestoreTickTime();
        gameTick.Resume();
        blockInput = false;
    }
}
