using UnityEngine;
using System.Collections;

public class PauseScreen : GenericScreen
{
    public Toast toast;

    private bool exitConfirmed = false;

    public override void Show()
    {
        toast.Hide();
        exitConfirmed = false;
        base.Show();
    }

    public void ContinueButtonClick()
    {
        gui.Next(GraphicalUI.Transitions.PauseToGame);
        Main.I.Resume();
    }
    
    public void ExitButtonClick()
    {
        if(exitConfirmed)
        {
            Main.I.GameEnd();
            gui.Next(GraphicalUI.Transitions.PauseToGameOver);
        }
        else
        {
            toast.Show("Press again to cancel current game...");
            exitConfirmed = true;
        }
    }
}


