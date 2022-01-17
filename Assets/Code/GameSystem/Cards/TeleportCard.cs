using System;
using System.Collections.Generic;
using System.Linq;


namespace DAE.GameSystem.Cards
{
	public class TeleportCard : BaseCard<Piece<HexagonTile>, HexagonTile>
	{
		#region Methods
		public override List<HexagonTile> Positions(Piece<HexagonTile> piece, HexagonTile tile)
		{
			List<HexagonTile> tiles = _grid.GetTiles()
				.Where(tile => _board.TryGetPiece(tile, out _) == false)
				.ToList();

			if(tiles.Contains(tile))
			{
				_validTiles = new List<HexagonTile> { tile };
			}
			else
			{
				_validTiles = new List<HexagonTile>();
			}

			return _validTiles;
		}

		public override void Execute(Piece<HexagonTile> piece, HexagonTile tile, out Action forward, out Action backward)
		{
			forward = null;
			backward = null;

			if (!_board.TryGetTile(piece, out HexagonTile previousTile))
				return;

			forward = () =>
			{
				_board.Move(piece, tile);
			};

			backward = () =>
			{
				_board.Move(piece, previousTile);
			};
		}
		#endregion
	} 
}