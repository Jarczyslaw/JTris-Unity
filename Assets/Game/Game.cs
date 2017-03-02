using UnityEngine;
using System.Collections;
using System;

public class Game : MonoBehaviour
{
    public Board board;
    public Grid grid;
    public FiguresPool figuresPool;
    [NonSerialized]
    public Figure currentFigure;
    public GameObject nextFigureSpawnPoint;

    private IEnumerator dropCoroutine;

    [NonSerialized]
    public bool blockInput = false;

    void Awake()
    {
        figuresPool.Initialize();
        board.Initialize();
        grid.Initialize(board);
        SpawnFigure();
    }

    public void Clear()
    {
        if (dropCoroutine != null)
            StopCoroutine(dropCoroutine);
        board.Clear();
        figuresPool.Clear();
        SpawnFigure();
        blockInput = false;
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
            Debug.Log("END");
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

    public void MoveUp(ref bool moveToBoard)
    {
        if (!blockInput)
        {
            currentFigure.MoveUp();
            if (!board.CheckTopBorder(currentFigure.vects)
                || !board.CheckBoard(currentFigure.vects))
            {
                currentFigure.MoveDown();
                board.MoveToStack(currentFigure.vects);
                board.TryToRemoveFullRows();
                figuresPool.ReturnToPool(currentFigure);
                SpawnFigure();
                moveToBoard = true;
            }
            else
                moveToBoard = false;
        }
    }

    public void MoveDown()
    {
        if (!blockInput)
            currentFigure.MoveDown();
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
            bool moveToBoard = false;
            MoveUp(ref moveToBoard);
            if (moveToBoard)
                break;
            yield return null;
        }
    }
}
