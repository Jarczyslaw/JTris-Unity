using UnityEngine;
using System.Collections;

public class PauseScreen : GenericScreen
{
    public void ContinueButtonClick()
    {
        gui.Next(GraphicalUI.Transitions.PauseToGame);
        Main.I.Resume();
    }
    
    public void ExitButtonClick()
    {
        Main.I.GameEnd();
        gui.Next(GraphicalUI.Transitions.PauseToGameOver);
    }
}


