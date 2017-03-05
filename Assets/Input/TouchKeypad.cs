using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchKeypad : MonoBehaviour
{
    public TouchButton moveLeft;
    public TouchButton moveRight;
    public TouchButton moveUp;

    public TouchButton rotateLeft;
    public TouchButton rotateRight;
    public TouchButton drop;

    public void Init()
    {
        moveLeft.Init();
        moveRight.Init();
        moveUp.Init();

        rotateLeft.Init();
        rotateRight.Init();
        drop.Init();
    }
}
