using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.GameSystem.Cards
{
	public interface ICard<TPiece, TTile>
	{
		void Initialize(Board<TPiece, TTile> board, Grid<TTile> grid);
		bool Execute(TPiece piece, TTile tile);

		List<TTile> Positions(TPiece piece, TTile tile);

	}
}
