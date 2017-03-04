using UnityEngine;
using System.Collections;
using System;

public class Timer : MonoBehaviour
{
    [NonSerialized]
    public Action<float> updateEvent;

    [NonSerialized]
    public float updateDelay = 1f;

    [NonSerialized]
    public float value;
    [NonSerialized]
    public bool isEnabled = false;

    private float toNextUpdate;

    void Update()
    {
        if (isEnabled)
        {
            float delta = Time.deltaTime;
            value += delta;
            toNextUpdate += delta;
            if (toNextUpdate >= updateDelay)
            {
                FireEvent();
                toNextUpdate = 0f;
            }
        }
    }

    public void Run()
    {
        value = 0f;
        toNextUpdate = 0f;
        isEnabled = true;
        FireEvent();
    }

    public void Stop()
    {
        isEnabled = false;
    }

    public void Resume()
    {
        isEnabled = true;
        FireEvent();
    }

    private void FireEvent()
    {
        if (updateEvent != null)
            updateEvent(value);
    }
}
