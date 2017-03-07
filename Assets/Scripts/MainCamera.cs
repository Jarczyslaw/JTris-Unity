using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{
    public Camera mainCamera;
    public Board board;
    public GameObject menuSpawner;

    private Vector3 gameViewPosition;
    private float gameOrtoSize;
    private Vector3 menuViewPosition;
    private float menuOrtoSize;

    void Awake()
    {
        float yPos = (board.rows - 1f) / 2f;
        float xPos = (board.cols - 1f) / 2f;
        gameViewPosition = new Vector3(xPos, yPos, -10f);
        gameOrtoSize = board.rows / 2f;

        menuViewPosition = new Vector3(menuSpawner.transform.position.x, 0f, -10f);
        menuOrtoSize = 5f;
    }

    public void SetMenuView()
    {
        transform.position = menuViewPosition;
        mainCamera.orthographicSize = menuOrtoSize;
    }

    public void SetGameView()
    {
        transform.position = gameViewPosition;
        mainCamera.orthographicSize = gameOrtoSize;
    }
}
