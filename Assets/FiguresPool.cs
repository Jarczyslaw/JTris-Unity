using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FiguresPool : MonoBehaviour {

    public Figure[] figures;

    private Figure lastFigure = null;

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
	
    public Figure GetRandomFigure()
    {
        List<Figure> figs = new List<Figure>(figures);
        if (lastFigure != null)
            figs.Remove(lastFigure);
        lastFigure = figs[Random.Range(0, figs.Count)];
        return lastFigure;
    }

    public void ReturnToPool(Figure figure)
    {
        figure.transform.rotation = Quaternion.identity;
        figure.transform.position = startPositions[figure];
    }
	
}
