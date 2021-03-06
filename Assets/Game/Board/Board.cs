﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Board : MonoBehaviour
{
    public GameObject boardBlockPrefab;

    public Game game;

    [NonSerialized]
    public int cols = 10;
    [NonSerialized]
    public int rows = 20;

    private SpriteRenderer[,] blocks;

    [NonSerialized]
    public Vector3 spawnPoint;

    public void Initialize()
    {
        blocks = new SpriteRenderer[rows, cols];

        spawnPoint = new Vector3((float)(cols / 2), 0f, 0f);

        CreateBoard();
        Clear();
        //ChessBoard();
    }

    public void Clear()
    {
        FillBoard(false);
    }

    private void ForEach(Action<int,int> blockAction)
    {
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                blockAction(i,j);
    }

    private void CreateBoard()
    {
        ForEach((i, j) =>
        {
            GameObject go = Instantiate(boardBlockPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            go.transform.parent = transform;
            go.transform.position = new Vector3(j, i, 0f);
            blocks[i, j] = go.GetComponent<SpriteRenderer>();
            blocks[i, j].enabled = false;
        });
    }

    private void FillBoard(bool value)
    {
        ForEach((i, j) =>
        {
            blocks[i, j].enabled = value;
        });
    }

    public void SetRow(int row, bool state)
    {
        for (int i = 0; i < cols; i++)
            blocks[row, i].enabled = state;
    }

    public void SetRows(List<int> rows, bool state)
    {
        for (int i = 0; i < rows.Count; i++)
            SetRow(rows[i], state);
    }

    private void ChessBoard()
    {
        ForEach((i, j) =>
        {
            blocks[i, j].enabled = ((i + j) % 2 == 0);
        });
    }

    public bool CheckSideBorders(Vector3i[] figureBlocks)
    {
        for (int i = 0;i < figureBlocks.Length;i++)
        {
            Vector3i figureBlock = figureBlocks[i];
            if (figureBlock.x < 0 || figureBlock.x >= cols)
                return false;
        }
        return true;
    }

    public bool CheckTopBorder(Vector3i[] figureBlocks)
    {
        for (int i = 0; i < figureBlocks.Length; i++)
        {
            Vector3i figureBlock = figureBlocks[i];
            if (figureBlock.y >= rows)
                return false;
        }
        return true;
    }

    public bool CheckBoard(Vector3i[] figureBlocks)
    {
        for (int i = 0; i < figureBlocks.Length; i++)
        {
            Vector3i figureBlock = figureBlocks[i];
            if (figureBlock.x >= 0 && figureBlock.x <= cols && figureBlock.y >= 0 && figureBlock.y <= rows)
                if (blocks[figureBlock.y, figureBlock.x].enabled)
                    return false;
        }
        return true;
    }

    public void MoveToStack(Vector3i[] figureBlocks)
    {
        for (int i = 0; i < figureBlocks.Length; i++)
        {
            int row = figureBlocks[i].y;
            int col = figureBlocks[i].x;
            if (row >= 0 && row < rows && col >= 0 && col < cols)
                blocks[row, col].enabled = true;
        }
    }

    public List<int> GetFullRows()
    {
        List<int> rowsToRemove = new List<int>();
        for (int i = rows - 1; i >= 0; i--)
            if (CheckFullRow(i))
                rowsToRemove.Add(i);

        return rowsToRemove;
    }

    public void RemoveFullRows(List<int> rowsToRemove)
    {
        int offset = 0;
        for (int i = rows - 1; i >= 0; i--)
        {
            if (rowsToRemove.Contains(i))
                offset++;
            else
            {
                if (offset != 0)
                {
                    MoveRow(i, i + offset);
                }
            }
        }
    }

    private void MoveRow(int from, int to)
    {
        for (int i = 0;i < cols;i++)
            blocks[to, i].enabled = blocks[from, i].enabled;
        RemoveRow(from);
    }

    private bool CheckFullRow(int row)
    {
        for (int i = 0;i < cols;i++)
        {
            if (!blocks[row, i].enabled)
                return false;
        }
        return true;
    }

    private void RemoveRow(int row)
    {
        for (int i = 0; i < cols; i++)
            blocks[row, i].enabled = false;
    }
}
