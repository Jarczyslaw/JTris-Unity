using UnityEngine;
using System.Collections;
using System;

public class GraphicalUI : Singleton<GraphicalUI> {

	public enum Screens {
		Menu, Game, Pause, GameOver
	}

	public enum Transitions {
		MenuToGame, GameToPause, PauseToGame, PauseToGameOver, GameToGameOver, GameOverToMenu
	}

	[NonSerialized] public StateMachine<Transitions, Screens> state;
	[NonSerialized] public Screens lastScreen;
	[NonSerialized] public Transitions lastTransition;

	[Header ("SCREENS")] 
	public MenuScreen menu;
	public GameScreen game;
	public PauseScreen pause;
	public GameOverScreen gameOver;

	public static T ParseEnum<T>(string value) {
		return (T) Enum.Parse(typeof(T), value, true);
	}

	void Awake () {
		state = new StateMachine<Transitions, Screens> ();

		state.Add (Screens.Menu, Transitions.MenuToGame, Screens.Game);
		state.Add (Screens.Game, Transitions.GameToPause, Screens.Pause);
		state.Add (Screens.Game, Transitions.GameToGameOver, Screens.GameOver);
		state.Add (Screens.Pause, Transitions.PauseToGame, Screens.Game);
        state.Add (Screens.Pause, Transitions.PauseToGameOver, Screens.GameOver);
        state.Add (Screens.GameOver, Transitions.GameOverToMenu, Screens.Menu);

		state.SetState (Screens.Menu);
		UpdateScreens ();
	}

	public void Next(Transitions transition) {
		lastScreen = state.CurrentState;
		lastTransition = transition;
		NextFinish ();
	}

	private void NextFinish() {
		state.Next (lastTransition);
		Debug.Log ("Last screen: " + lastScreen.ToString () + ", transition: " + lastTransition.ToString () + ", new screen: " + state.CurrentState);
		UpdateByTransition (lastTransition);
		UpdateByState (state.CurrentState);
		UpdateScreens ();
	}

	private void UpdateByState(Screens s) {
		
	}

	private void UpdateByTransition(Transitions t) {
	}

	private void UpdateScreens() {
		HideAll ();

		Screens s = state.CurrentState;
		if (s == Screens.Menu)
			menu.Show ();
		else if (s == Screens.Game)
			game.Show ();
		else if (s == Screens.Pause)
			pause.Show ();
		else if (s == Screens.GameOver)
			gameOver.Show ();
	}

	private void HideAll () {
		menu.Hide ();
		game.Hide ();
		pause.Hide ();
		gameOver.Hide ();
	}
}


