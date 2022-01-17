using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAE.GameSystem.Cards
{
	public class BombCard : SwipeCard
	{
		#region Methods
		public override List<HexagonTile> Positions(Piece<HexagonTile> piece, HexagonTile tile)
		{
			_validTiles = GetNeighbours(tile);
			_validTiles.Add(tile);

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
