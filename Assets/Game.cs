using UnityEngine;
using System.Collections;
using System;

public class Game : MonoBehaviour
{
    public Board board;
    public FiguresPool figuresPool;
    [NonSerialized]
    public Figure currentFigure;

    private IEnumerator dropCoroutine;

    void Awake()
    {
        figuresPool.Initialize();
        board.Initialize();
        SpawnFigure();
    }

    private void SpawnFigure()
    {
        currentFigure = figuresPool.GetRandomFigure();
        currentFigure.transform.position = board.spawnPoint;
        currentFigure.UpdateVectors();
        if (!board.CheckBoard(currentFigure.vects))
            Debug.Log("END");
    }

    public void RotateLeft()
    {
        currentFigure.RotateLeft();
        if (!board.CheckSideBorders(currentFigure.vects) 
            || !board.CheckTopBorder(currentFigure.vects) 
            || !board.CheckBoard(currentFigure.vects))
        {
            currentFigure.RotateRight();
        }
    }

    public void RotateRight()
    {
        currentFigure.RotateRight();
        if (!board.CheckSideBorders(currentFigure.vects) 
            || !board.CheckTopBorder(currentFigure.vects) 
            || !board.CheckBoard(currentFigure.vects))
            currentFigure.RotateLeft();
    }

    public bool MoveUp()
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
            return true;
        }
        else
            return false;
    }

    public void MoveDown()
    {
        currentFigure.MoveDown();
    }

    public void MoveLeft()
    {
        currentFigure.MoveLeft();
        if (!board.CheckSideBorders(currentFigure.vects) 
            || !board.CheckBoard(currentFigure.vects))
            currentFigure.MoveRight();
    }

    public void MoveRight()
    {
        currentFigure.MoveRight();
        if (!board.CheckSideBorders(currentFigure.vects) 
            || !board.CheckBoard(currentFigure.vects))
            currentFigure.MoveLeft();
    }

    public void Drop()
    {
        if (dropCoroutine != null)
            StopCoroutine(dropCoroutine);
        dropCoroutine = DropCoroutine();
        StartCoroutine(dropCoroutine);
    }

    private IEnumerator DropCoroutine()
    {
        while (true)
        {
            if (MoveUp())
                break;
            yield return null;
        }
    }
}
