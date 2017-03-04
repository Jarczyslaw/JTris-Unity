using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class InputController : Singleton<InputController>
{
    public GameInput gameInput;

    void Awake()
    {
        if (Application.isEditor)
        {
            gameInput.AddInputEntry(FigureMoves.Up,
                () => Input.GetKeyDown(KeyCode.UpArrow),
                () => Input.GetKey(KeyCode.UpArrow));
            gameInput.AddInputEntry(FigureMoves.Drop,
                () => Input.GetKeyDown(KeyCode.W),
                () => Input.GetKey(KeyCode.W));
            gameInput.AddInputEntry(FigureMoves.Left,
                () => Input.GetKeyDown(KeyCode.LeftArrow),
                () => Input.GetKey(KeyCode.LeftArrow));
            gameInput.AddInputEntry(FigureMoves.Right,
                () => Input.GetKeyDown(KeyCode.RightArrow),
                () => Input.GetKey(KeyCode.RightArrow));
            gameInput.AddInputEntry(FigureMoves.RotateLeft,
                () => Input.GetKeyDown(KeyCode.A),
                () => Input.GetKey(KeyCode.A));
            gameInput.AddInputEntry(FigureMoves.RotateRight,
                () => Input.GetKeyDown(KeyCode.D),
                () => Input.GetKey(KeyCode.D));
        }
    }

    private void Update()
    {
        gameInput.CheckActions();
    }
}
