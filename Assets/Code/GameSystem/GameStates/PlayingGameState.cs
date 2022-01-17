using DAE.BoardSystem;
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
		private Grid<HexagonTile> _grid;

		public PlayingGameState(StateMachine<GameStateBase> stateMachine, Board<Piece<HexagonTile>, HexagonTile> board, Grid<HexagonTile> grid, ReplayManager replayManager) : base(stateMachine)
		{
			_board = board;
			_grid = grid;

		}

		public override void OnEnter()
		{
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
