using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAE.StateSystem
{
	public class StateMachine<TState> where TState : IState<TState>
	{
		#region Properties
		public TState CurrentState => _states[_currentStateName];

		public string InitialState
		{
			set
			{
				_currentStateName = value;
				CurrentState.OnEnter();
			}
		}

		public Dictionary<string, TState> States => _states;
		#endregion

		#region Fields
		private Dictionary<string, TState> _states = new Dictionary<string, TState>();
		private string _currentStateName;
		#endregion

		#region Methods
		public void Register(string stateName, TState state)
		{
			_states.Add(stateName, state);
		}

		public void MoveTo(string stateName)
		{
			CurrentState?.OnExit();

			_currentStateName = stateName;

			CurrentState.OnEnter();
		} 
		#endregion
	}
}
