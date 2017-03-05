using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class InputController : Singleton<InputController>
{
    public GameInput gameInput;

    public TouchKeypad touchKeypad;

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

        gameInput.AddInputEntry(FigureMoves.Left,
               () => touchKeypad.moveLeft.GetButtonDown(),
               () => touchKeypad.moveLeft.GetButton());
        gameInput.AddInputEntry(FigureMoves.Right,
           () => touchKeypad.moveRight.GetButtonDown(),
           () => touchKeypad.moveRight.GetButton());
        gameInput.AddInputEntry(FigureMoves.Up,
           () => touchKeypad.moveUp.GetButtonDown(),
           () => touchKeypad.moveUp.GetButton());
        gameInput.AddInputEntry(FigureMoves.RotateLeft,
           () => touchKeypad.rotateLeft.GetButtonDown(),
           () => touchKeypad.rotateLeft.GetButton());
        gameInput.AddInputEntry(FigureMoves.RotateRight,
           () => touchKeypad.rotateRight.GetButtonDown(),
           () => touchKeypad.rotateRight.GetButton());
        gameInput.AddInputEntry(FigureMoves.Drop,
           () => touchKeypad.drop.GetButtonDown(),
           () => touchKeypad.drop.GetButton());
    }

    private void Update()
    {
        gameInput.CheckActions();
        ListenForBackButton();
    }

    private void ListenForBackButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
            GraphicalUI.I.BackbuttonAction();
    }
}
