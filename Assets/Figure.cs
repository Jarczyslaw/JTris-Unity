using UnityEngine;
using System.Collections;
using System;

public class Figure : MonoBehaviour
{
    public GameObject[] blocks;
    public GameObject axis;

    [NonSerialized]
    public Vector3i[] vects;

    public void Initialize()
    {
        vects = new Vector3i[blocks.Length];
        for (int i = 0; i < blocks.Length; i++)
            vects[i] = new Vector3i();
        UpdateVectors();
    }

    public void UpdateVectors()
    {
        for(int i = 0;i < blocks.Length;i++)
        {
            Vector3 p = blocks[i].transform.position;
            vects[i].Set(p.x, p.y, p.z);
        }
    }

    public void RotateLeft()
    {
        transform.RotateAround(axis.transform.position, Vector3.forward, 90f);
        UpdateVectors();
    }

    public void RotateRight()
    {
        transform.RotateAround(axis.transform.position, Vector3.forward, -90f);
        UpdateVectors();
    }

    public void MoveUp()
    {
        transform.position += Vector3.up;
        UpdateVectors();
    }

    public void MoveDown()
    {
        transform.position += Vector3.down;
        UpdateVectors();
    }

    public void MoveLeft()
    {
        transform.position += Vector3.left;
        UpdateVectors();
    }

    public void MoveRight()
    {
        transform.position += Vector3.right;
        UpdateVectors();
    }
}
