using DAE.BoardSystem;
using DAE.GameSystem.Cards;
using DAE.ReplaySystem;
using DAE.StateSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.GameSystem.GameStates
{
	class PlayingGameState : GameStateBase
	{
		private Board<Piece<HexagonTile>, HexagonTile> _board;
		private ReplayManager _replayManager;
		private GameLoop _gameLoop;


		public PlayingGameState(StateMachine<GameStateBase> stateMachine, Board<Piece<HexagonTile>, HexagonTile> board, ReplayManager replayManager, GameLoop gameLoop) : base(stateMachine)
		{
			_board = board;
			_replayManager = replayManager;
			_gameLoop = gameLoop;
		}

		public override void OnEnter()
		{
			_gameLoop.GenerateDeck(_replayManager);
			_board.PieceMoved += OnPieceMoved;
		}

		public override void OnExit()
		{
			_board.PieceMoved -= OnPieceMoved;
		}


		public override void Backward()
		{
			StateMachine.MoveTo(ReplayingState);
		}

		private void OnPieceMoved(object sender, PieceMovedEventArgs<Piece<HexagonTile>, HexagonTile> eventArgs)
		{
			
		}
	}
}
