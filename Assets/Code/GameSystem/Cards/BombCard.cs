using System;
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

		public override void Execute(Piece<HexagonTile> piece, HexagonTile tile, out Action forward, out Action backward)
		{
			forward = null;
			backward = null;

			if (!_validTiles.Contains(tile)) return;

			Dictionary<Piece<HexagonTile>, HexagonTile> piecesToTake = PiecesOnValidTiles();


			forward = () =>
			{
				TakePieces(piecesToTake);
				RemoveTiles(_validTiles);
			};

			backward = () =>
			{
				throw new NotImplementedException();
			};
		}
		#endregion
	} 
}
