using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statics
{
    public static string SecondsToTime(float sec)
    {
        int hours = Mathf.FloorToInt(sec / 3600f);
        int minutes = Mathf.FloorToInt(sec / 60f);
        int seconds = Mathf.FloorToInt(sec - minutes * 60);
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}
