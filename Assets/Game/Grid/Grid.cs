using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject gridBlockPrefab;

    public void Initialize(Board board)
    {
        int rows = board.rows;
        int cols = board.cols;
        for (int i = 0;i < rows;i++)
        {
            for (int j = 0;j < cols;j++)
            {
                GameObject go = Instantiate(gridBlockPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                go.transform.parent = transform;
                go.transform.position = new Vector3(j, i, 0f);
            }
        }
    }
}
