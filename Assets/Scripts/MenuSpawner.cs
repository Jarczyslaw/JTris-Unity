using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpawner : MonoBehaviour
{
    public FiguresPool figuresPool;

    private Transform[] spawnPoints;

    private void Awake()
    {
        GetSpawnPoints();
        SpawnFigures();
    }

    private void GetSpawnPoints()
    {
        int childrenCount = transform.childCount;
        spawnPoints = new Transform[childrenCount];

        for (int i = 0; i < childrenCount; i++)
            spawnPoints[i] = transform.GetChild(i);
    }

    private void SpawnFigures()
    {
        List<int> indexes = new List<int>();
        FillList(indexes);
        foreach(Transform spawnPoint in spawnPoints)
        {
            int rand = indexes[Random.Range(0, indexes.Count)];
            var figure = CopyFigure(figuresPool.figures[rand]);
            figure.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            figure.transform.position = spawnPoint.position;
            figure.transform.parent = spawnPoint;

            indexes.Remove(rand);
            if (indexes.Count == 0)
                FillList(indexes);
        }
    }

    private GameObject CopyFigure(Figure figure)
    {
        var figureClone = Instantiate(figure.gameObject, transform) as GameObject;
        Figure figureComponent = figureClone.GetComponent<Figure>();
        if (figureComponent != null)
            Destroy(figureComponent);
        return figureClone;
    }

    private void FillList(List<int> list)
    {
        list.Clear();
        for (int i = 0; i < figuresPool.figures.Length; i++)
            list.Add(i);
    }
}
