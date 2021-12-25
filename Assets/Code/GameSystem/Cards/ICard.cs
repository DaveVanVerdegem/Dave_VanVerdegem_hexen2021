using DAE.BoardSystem;
using System.Collections.Generic;

namespace DAE.GameSystem.Cards
{
	public interface ICard<TPiece, TTile>
	{
		#region Methods
		void Initialize(Board<TPiece, TTile> board, Grid<TTile> grid);
		bool Execute(TPiece piece, TTile tile);

		List<TTile> Positions(TPiece piece, TTile tile);
		#endregion
	}
}
