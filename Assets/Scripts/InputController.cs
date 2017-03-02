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
        if (Input.GetKeyDown(KeyCode.A))
        {
            game.RotateLeft();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            game.RotateRight();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            bool x = false;
            game.MoveUp(ref x);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            game.MoveDown();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            game.MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            game.MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            game.Drop();
        }
    }
}
