using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.HexenSystem.Moves
{
	abstract class MoveBase<TPosition> : IMove<TPosition> where TPosition : MonoBehaviour, IPosition
	{
		#region Fields
		protected Board<Piece<TPosition>, TPosition> _board { get; }
		protected Grid<TPosition> _grid { get; }
		#endregion

		#region Constructors
		protected MoveBase(Board<Piece<TPosition>, TPosition> board, Grid<TPosition> grid)
		{
			_board = board;
			_grid = grid;
		}
		#endregion

		#region Methods
		public bool CanExecute(Piece<TPosition> piece)
			=> true;

		public void Execute(Piece<TPosition> piece, TPosition position)
		{
			if (_board.TryGetPiece(position, out var toPiece))
				_board.Take(toPiece);

			//_board.Move(piece, position);
		}

		public abstract List<TPosition> Positions(Piece<TPosition> piece); 
		#endregion

	}
}
