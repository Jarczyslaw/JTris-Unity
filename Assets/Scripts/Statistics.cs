using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Statistics
{
    private readonly string totalGamesKey = "totalGames";
    private readonly string totalPointsKey = "totalPoints";
    private readonly string totalTimeKey = "totalTime";

    [NonSerialized]
    public int totalGames;
    [NonSerialized]
    public int totalPoints;
    [NonSerialized]
    public float totalTime;

    public Statistics()
    {
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetInt(totalGamesKey, totalGames);
        PlayerPrefs.SetInt(totalPointsKey, totalPoints);
        PlayerPrefs.SetFloat(totalTimeKey, totalTime);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        totalGames = PlayerPrefs.GetInt(totalGamesKey, 0);
        totalPoints = PlayerPrefs.GetInt(totalPointsKey, 0);
        totalTime = PlayerPrefs.GetFloat(totalTimeKey, 0f);
    }

    public void Clear()
    {
        PlayerPrefs.SetInt(totalGamesKey, 0);
        PlayerPrefs.SetInt(totalPointsKey, 0);
        PlayerPrefs.SetFloat(totalTimeKey, 0f);
        PlayerPrefs.Save();
    }

    public void AddGame(int score, float time)
    {
        totalGames++;
        totalPoints += score;
        totalTime += time;
        Save();
    }

    public string GetAveragePoints()
    {
        string result = "-";
        if (totalGames != 0)
            result = Mathf.Round((float)totalPoints / totalGames).ToString("0.00");
        return result;
    }

    public string GetAverageTime()
    {
        string result = "-";
        if (totalGames != 0)
            result = Statics.SecondsToTime(Mathf.Round((float)totalTime / totalGames));
        return result;
    }

    public string[,] ToArray()
    {
        string[,] result = new string[5, 2];
        result[0, 0] = "Games:";
        result[0, 1] = totalGames.ToString();
        result[1, 0] = "Points:";
        result[1, 1] = totalPoints.ToString();
        result[2, 0] = "Time:";
        result[2, 1] = Statics.SecondsToTime(totalTime);
        result[3, 0] = "Avg points:";
        result[3, 1] = GetAveragePoints();
        result[4, 0] = "Avg time:";
        result[4, 1] = GetAverageTime();
        return result;
    }
}
