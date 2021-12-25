using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.HexenSystem.Moves
{
	abstract class MoveBase<TTile> : IMove<TTile> where TTile : MonoBehaviour, ITile
	{
		#region Fields
		protected Board<Piece<TTile>, TTile> _board { get; }
		protected Grid<TTile> _grid { get; }
		#endregion

		#region Constructors
		protected MoveBase(Board<Piece<TTile>, TTile> board, Grid<TTile> grid)
		{
			_board = board;
			_grid = grid;
		}
		#endregion

		#region Methods
		public bool CanExecute(Piece<TTile> piece)
			=> true;

		public void Execute(Piece<TTile> piece, TTile position)
		{
			if (_board.TryGetPiece(position, out var toPiece))
				_board.Take(toPiece);
		}

		public abstract List<TTile> Positions(Piece<TTile> piece); 
		#endregion

	}
}
