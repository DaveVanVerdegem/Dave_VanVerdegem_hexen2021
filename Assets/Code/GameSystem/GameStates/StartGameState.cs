using DAE.BoardSystem;
using DAE.StateSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAE.GameSystem.GameStates
{
	class StartGameState : GameStateBase
	{
		private GameObject _welcomeScreen;

		public StartGameState(StateMachine<GameStateBase> stateMachine, GameObject welcomeScreen) : base(stateMachine)
		{
			_welcomeScreen = welcomeScreen;
		}

		public override void OnEnter()
		{
			_welcomeScreen.SetActive(true);
		}

		public override void OnExit()
		{
			_welcomeScreen.SetActive(false);
		}
	} 
}
