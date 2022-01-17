using DAE.StateSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAE.GameSystem.GameStates
{
	class EndGameState : GameStateBase
{
		private GameObject _endScreen;

		public EndGameState(StateMachine<GameStateBase> stateMachine, GameObject endScreen) : base(stateMachine)
		{
			_endScreen = endScreen;
		}

		public override void OnEnter()

		{
			_endScreen.SetActive(true);
		}

		public override void OnExit()
		{
			_endScreen.SetActive(false);
		}
	} 
}
