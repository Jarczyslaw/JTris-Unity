using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine<T1, T2>
{
	private class Transition
	{
		public T1 Command;
		public T2 NextState;
			
		public Transition (T1 c, T2 s)
		{
			Command = c;
			NextState = s;
		}
	}
		
	public T2 CurrentState { get; private set; }
	public T2 LastState { get; private set; }

	private Dictionary<T2, List<Transition>> Transitions;
		
	public StateMachine ()
	{
		Transitions = new Dictionary<T2, List<Transition>> ();         
	}
		
	public void Add (T2 source, T1 command, T2 destination)
	{
		Transition tran = new Transition (command, destination);
		if (!Transitions.ContainsKey(source))
			Transitions [source] = new List<Transition> ();
		if (Transitions.Count == 0)
			CurrentState = source;
		if (GetDestinationsByCommand (source, command).Count == 0)
			Transitions [source].Add (tran);
		else
			Debug.LogError ("State machine invalid operation in adding transition");
	}
		
	public T2 Next (T1 command)
	{
		List<T2> dests = GetDestinationsByCommand (CurrentState, command);
		if (dests.Count == 1) {
			LastState = CurrentState;
			CurrentState = dests [0];
		} else {
			Debug.LogError ("State machine invalid transition");
		}
			
		return CurrentState;
	}
		
	private List<T2> GetDestinationsByCommand (T2 start, T1 comm)
	{
		List<Transition> trans = Transitions [start];
		List<T2> res = new List<T2> ();
		for (int i = 0; i < trans.Count; i++)
			if (trans [i].Command.Equals(comm))
				res.Add (trans [i].NextState);
		return res;
	}

	public void SetState(T2 state) {
		LastState = CurrentState;
		CurrentState = state;
	}

}
