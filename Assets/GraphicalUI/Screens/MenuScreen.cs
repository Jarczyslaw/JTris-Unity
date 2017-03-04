using UnityEngine;
using System.Collections;

public class MenuScreen : GenericScreen
{
    public void StartButtonClick()
    {
        gui.Next(GraphicalUI.Transitions.MenuToGame);
        Main.I.GameStart();
    }
}


