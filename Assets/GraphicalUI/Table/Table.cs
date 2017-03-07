using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Table : MonoBehaviour
{
    public Text titleText;
    public GameObject container;
    public Text[,] texts;

    private int columnsCount = -1;
    private int rowsCount = -1;

    public void Init()
    {
        columnsCount = container.transform.childCount;
        GameObject[] columns = new GameObject[columnsCount];
        for (int i = 0; i < columnsCount; i++)
            columns[i] = container.transform.GetChild(i).gameObject;

        rowsCount = columns[0].transform.childCount;
        texts = new Text[rowsCount, columnsCount];
        for (int i = 0; i < columns.Length; i++)
        {
            int cnt = columns[i].transform.childCount;
            if (rowsCount != cnt)
                Debug.LogError("Invalid rows count. Each column should have the same rows");
            for (int j = 0; j < rowsCount; j++)
            {
                texts[j, i] = columns[i].transform.GetChild(j).GetComponent<Text>();
            }
        }

        ClearTable();
    }

    public void SetRow(string[] text, int rowId)
    {
        for (int i = 0; i < text.Length; i++)
        {
            texts[rowId, i].text = text[i];
        }
    }

    public void SetTitle(string title)
    {
        titleText.text = title;
    }

    public void ClearTable()
    {
        for (int i = 0; i < rowsCount; i++)
        {
            for (int j = 0; j < columnsCount; j++)
            {
                texts[i, j].text = "";
            }
        }
    }

    public string[] GetRow(int rowId)
    {
        string[] result = new string[texts.GetLength(1)];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = texts[rowId, i].text;
        }
        return result;
    }

    public string[] GetColumn(int columnId)
    {
        string[] result = new string[texts.GetLength(0)];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = texts[i, columnId].text;
        }
        return result;
    }

    public void SetColumn(string[] text, int columnId)
    {
        for (int i = 0; i < text.Length; i++)
            texts[i, columnId].text = text[i];
    }

    public string[,] GetContent()
    {
        string[,] result = new string[rowsCount, columnsCount];
        for (int i = 0; i < rowsCount; i++)
        {
            for (int j = 0; j < columnsCount; j++)
            {
                result[i, j] = texts[i, j].text;
            }
        }
        return result;
    }

    public void FillTable(string[,] content)
    {
        int contentRows = content.GetLength(0);
        int contentCols = content.GetLength(1);
        if (contentRows != rowsCount || contentCols != columnsCount)
            Debug.LogError("Invalid rows or columns count");

        for (int i = 0; i < rowsCount; i++)
        {
            for (int j = 0; j < columnsCount; j++)
            {
                texts[i, j].text = content[i, j];
            }
        }
    }
}
