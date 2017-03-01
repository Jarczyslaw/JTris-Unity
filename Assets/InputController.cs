using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
    public Game game;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            game.RotateLeft();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            game.RotateRight();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            game.MoveUp();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            game.MoveDown();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            game.MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            game.MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            game.Drop();
        }
    }
}
