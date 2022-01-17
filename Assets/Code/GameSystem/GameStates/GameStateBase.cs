using DAE.StateSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.GameSystem.GameStates
{
	abstract class GameStateBase : IState<GameStateBase>
	{
		public const string PlayingState = "playing";
		public const string ReplayingState = "replaying";
		public const string StartState = "startscreen";
		public const string EndState = "endstate";

		private StateMachine<GameStateBase> _stateMachine;

		public StateMachine<GameStateBase> StateMachine => _stateMachine;

		protected GameStateBase(StateMachine<GameStateBase> stateMachine)
		{
			_stateMachine = stateMachine;
		}

		public virtual void OnEnter()
		{
		}

		public virtual void OnExit()
		{
		}

		public virtual void Forward() { }

		public virtual void Backward() { }

		public virtual void Select(Piece<HexagonTile> piece) { }

		public virtual void Select(HexagonTile tile) { }
	}
}