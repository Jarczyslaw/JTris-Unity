using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Highscores : MonoBehaviour
{
    private readonly string scoresFile = "records.jtr";
    
    [HideInInspector]
    public Records records;

    private void Awake()
    {
        Load();
    }

    public void Load()
    {
        records = JsonSerializer.FromFile<Records>(scoresFile);
        if (records == null)
            records = new Records();
        Print();
    }

    public bool AddScore(int score)
    {
        bool newRecord = records.Add(score);
        if (newRecord)
            JsonSerializer.ToFile(records, scoresFile);
        return newRecord;   
    }

    public void Print()
    {
        foreach (Records.Score s in records.scores)
            Debug.Log(s.ToString());
    }
}
