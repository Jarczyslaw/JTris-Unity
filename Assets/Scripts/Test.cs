using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public Game game;

	public void ResetButtonClick()
    {
        game.Clear();
    }
}
