using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{
    private class InputEntry
    {
        private GameInput gameInput;

        public FigureMoves move;
        public Func<bool> startAction;
        public Func<bool> continueAction;

        private IEnumerator moveCoroutine = null;

        private float firstDelay = 0.3f;
        private float repeatDelay = 0.05f;

        public InputEntry(GameInput gameInput)
        {
            this.gameInput = gameInput;
        }

        public void CheckAction()
        {
            if (startAction())
            {
                gameInput.PerformMove(move);
                StartMovesCoroutine();
            }
        }

        public void Stop()
        {
            if (moveCoroutine != null)
                gameInput.StopCoroutine(moveCoroutine);
        }

        private void StartMovesCoroutine()
        {
            Stop();
            moveCoroutine = MoveCoroutine();
            gameInput.StartCoroutine(moveCoroutine);
        }

        private IEnumerator MoveCoroutine()
        {
            yield return new WaitForSeconds(firstDelay);
            if (continueAction())
            {
                gameInput.PerformMove(move);
                while (true)
                {
                    yield return new WaitForSeconds(repeatDelay);
                    if (continueAction())
                        gameInput.PerformMove(move);
                    else
                        break;
                }
            }
        }
    }

    public Game game;

    private List<InputEntry> inputEntries = new List<InputEntry>();

    public void AddInputEntry(FigureMoves move, Func<bool> startAction, Func<bool> continueAction)
    {
        inputEntries.Add(new InputEntry(this)
        {
            move = move,
            startAction = startAction,
            continueAction = continueAction,
        });
    }

    public void CheckActions()
    {
        for (int i = 0; i < inputEntries.Count; i++)
            inputEntries[i].CheckAction();  
    }

    public void Stop()
    {
        for (int i = 0; i < inputEntries.Count; i++)
            inputEntries[i].Stop();
    }

    public void PerformMove(FigureMoves move)
    {
        if (move == FigureMoves.Up)
            game.MoveUp();
        else if (move == FigureMoves.Drop)
            game.Drop();
        else if (move == FigureMoves.Left)
            game.MoveLeft();
        else if (move == FigureMoves.Right)
            game.MoveRight();
        else if (move == FigureMoves.RotateLeft)
            game.RotateLeft();
        else if (move == FigureMoves.RotateRight)
            game.RotateRight();
    }
}
