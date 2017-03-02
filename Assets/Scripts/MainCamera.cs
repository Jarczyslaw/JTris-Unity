using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{
    public Camera mainCamera;
    public Board board;

    void Start()
    {
        float yPos = (board.rows - 1f) / 2f;
        float xPos = (board.cols - 1f) / 2f;
        transform.position = new Vector3(xPos, yPos, -10f);

        mainCamera.orthographicSize = board.rows / 2f;
    }
}
