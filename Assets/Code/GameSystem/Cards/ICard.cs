using DAE.BoardSystem;
using System;
using System.Collections.Generic;

namespace DAE.GameSystem.Cards
{
	public interface ICard<TPiece, TTile>
	{
		#region Methods
		void Initialize(Board<TPiece, TTile> board, Grid<TTile> grid);
		bool CanExecute(TTile tile);
		void Execute(TPiece piece, TTile tile, out Action forward, out Action backward);

		List<TTile> Positions(TPiece piece, TTile tile);
		#endregion
	}
}
