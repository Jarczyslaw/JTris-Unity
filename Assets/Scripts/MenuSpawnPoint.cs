using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpawnPoint : MonoBehaviour
{
    public FiguresPool figuresPool;

    private void Awake()
    {
        var randomFigure = figuresPool.figures[Random.Range(0, figuresPool.figures.Length)];
        var figureClone = Instantiate(randomFigure.gameObject, transform) as GameObject;
        Figure figureComponent = figureClone.GetComponent<Figure>();
        if (figureComponent != null)
            Destroy(figureComponent);
        figureClone.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        figureClone.transform.position = transform.position;
    }

}
