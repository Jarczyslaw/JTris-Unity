using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Routine
{
    private MonoBehaviour parent;

    private IEnumerator coroutine;

    public Routine(MonoBehaviour parent)
    {
        this.parent = parent;
    }

    public void Run(IEnumerator cor, bool stopPrevious = true)
    {
        if (stopPrevious)
            Stop();
        coroutine = cor;
        parent.StartCoroutine(coroutine);
    }
    
    public void Stop()
    {
        if (coroutine != null)
            parent.StopCoroutine(coroutine);
    }
}
