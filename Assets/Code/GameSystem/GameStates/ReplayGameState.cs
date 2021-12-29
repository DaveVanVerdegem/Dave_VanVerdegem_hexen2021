using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAE.ReplaySystem;
using DAE.StateSystem;

namespace DAE.GameSystem.GameStates
{
	class ReplayGameState : GameStateBase
	{
		#region Fields
		private readonly ReplayManager _replayManager; 
		#endregion

		#region Constructors
		public ReplayGameState(StateMachine<GameStateBase> stateMachine, ReplayManager replayManager) : base(stateMachine)
		{
			_replayManager = replayManager;
		} 
		#endregion

		#region Methods
		public override void OnEnter()
		{
			Backward();
		}

		public override void Backward()
		{
			_replayManager.Backward();
		}

		public override void Forward()
		{
			_replayManager.Forward();

			if (_replayManager.IsAtEnd)
				StateMachine.MoveTo(PlayingState);
		} 
		#endregion
	}
}
