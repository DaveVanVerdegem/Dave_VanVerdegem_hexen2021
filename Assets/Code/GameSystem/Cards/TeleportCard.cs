using DAE.GameSystem;
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

		public override bool Execute(Piece<HexagonTile> piece, HexagonTile tile)
		{
			if (!_validTiles.Contains(tile)) return false;

			_board.Move(piece, tile);

			base.Execute(piece, tile);

			return true;
		}
		#endregion
	} 
}