using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Highscores
{
    private readonly string scoresFile = "records.jtr";
    
    [HideInInspector]
    public Records records;

    public Highscores()
    {
        Load();
    }

    public void Load()
    {
        records = JsonSerializer.FromFile<Records>(scoresFile);
        if (records == null)
            records = new Records();
        //Print();
    }

    public bool AddScore(int score)
    {
        bool newRecord = records.Add(score);
        if (newRecord)
            JsonSerializer.ToFile(records, scoresFile);
        return newRecord;   
    }

    public void Clear()
    {
        records.scores.Clear();
        JsonSerializer.ToFile(records, scoresFile);
    }

    public int GetTopScore()
    {
        if (records.scores.Count == 0)
            return 0;
        else
            return records.scores[0].score;
    }

    public string[,] ToArray()
    {
        int limit = records.scoresLimit;
        var entries = records.scores;
        string[,] result = new string[limit, 3];
        for (int i = 0;i < limit;i++)
        {
            result[i, 0] = (i + 1).ToString();
            if (i < entries.Count)
            {
                result[i, 1] = entries[i].score.ToString();
                result[i, 2] = entries[i].GetDate().ToString("yyyy-MM-dd");
            }
            else
            {
                result[i, 1] = "-";
                result[i, 2] = "-";
            }
        }
        return result;
    }

    public void Print()
    {
        foreach (Records.Score s in records.scores)
            Debug.Log(s.ToString());
    }
}
