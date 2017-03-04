using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameTick : MonoBehaviour
{
    [NonSerialized]
    public float changeInterval = 1f * 60f;
    [NonSerialized]
    public float startTickInterval = 5f;
    [NonSerialized]
    public float minTickInterval = 0.2f;
    [NonSerialized]
    public float tickMultiplier = 0.75f;
    [NonSerialized]
    public bool running = false;
    [NonSerialized]
    public Action tickAction;

    private float currentTickInterval;

    private float tickTimeAccu = 0f;
    private float changeTimeAccu = 0f;

    private bool pendingChange = false;

    private void Update()
    {
        if (running)
        {
            if (!pendingChange)
            {
                changeTimeAccu += Time.deltaTime;

                if (changeTimeAccu > changeInterval)
                    pendingChange = true;
            }
                
            tickTimeAccu += Time.deltaTime;
            if (tickTimeAccu > currentTickInterval)
            {
                if (tickAction != null)
                    tickAction();
                tickTimeAccu -= currentTickInterval;

                if (pendingChange)
                {
                    currentTickInterval *= tickMultiplier;
                    if (currentTickInterval < minTickInterval)
                        currentTickInterval = minTickInterval;

                    changeTimeAccu = 0f;
                    pendingChange = false;
                }
            }
        }
    }

    public void Run()
    {
        tickTimeAccu = 0f;
        changeTimeAccu = 0f;
        currentTickInterval = startTickInterval;

        pendingChange = false;
        running = true;
    }

    public void Stop()
    {
        running = false;
    }

    public void Resume()
    {
        running = true;
    }
}
