using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FiguresPool : MonoBehaviour {

    public Figure[] figures;

    [NonSerialized]
    public Figure currentFigure = null;
    [NonSerialized]
    public Figure nextFigure = null;

    private Dictionary<Figure,Vector3> startPositions;

	public void Initialize ()
    {
        startPositions = new Dictionary<Figure, Vector3>();
        foreach (Figure figure in figures)
        {
            figure.Initialize();
            startPositions.Add(figure, figure.transform.position);
        }   
	}

    public void Clear()
    {
        if (currentFigure != null)
        {
            ReturnToPool(currentFigure);
            currentFigure = null;
        }
        if (nextFigure != null)
        {
            ReturnToPool(nextFigure);
            nextFigure = null;
        }
    }
	
    public void GetFigures()
    {
        if (nextFigure == null)
            nextFigure = figures[UnityEngine.Random.Range(0, figures.Length)];

        currentFigure = nextFigure;

        List<Figure> figs = new List<Figure>(figures);
        if (nextFigure != null)
            figs.Remove(nextFigure);
        nextFigure = figs[UnityEngine.Random.Range(0, figs.Count)];
    }

    public void ReturnToPool(Figure figure)
    {
        figure.transform.rotation = Quaternion.identity;
        figure.transform.position = startPositions[figure];
    }
	
}
