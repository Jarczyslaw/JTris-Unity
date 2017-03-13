using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameTick : MonoBehaviour
{
    [NonSerialized]
    public int level = 1;
    [NonSerialized]
    public Action tickAction;
    [NonSerialized]
    public Action<int> intervalChangeAction;

    private readonly float changeInterval = 60f;
    private readonly float startTickInterval = 5f;
    private readonly float minTickInterval = 0.05f;
    private readonly float tickMultiplier = 0.75f;
    
    private bool running = false;

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
                tickTimeAccu = 0f;

                if (pendingChange)
                {
                    currentTickInterval *= tickMultiplier;
                    if (currentTickInterval < minTickInterval)
                        currentTickInterval = minTickInterval;

                    level++;
                    if (intervalChangeAction != null)
                        intervalChangeAction(level);

                    changeTimeAccu = 0f;
                    pendingChange = false;
                }
            }
        }
    }

    public void Run()
    {
        level = 1;
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

    public void RestoreTickTime()
    {
        tickTimeAccu = 0f;
    }
}
