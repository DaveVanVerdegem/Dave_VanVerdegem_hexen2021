using System;
using System.Collections.Generic;
using UnityEngine;

namespace DAE.GameSystem.Cards
{
	public class SwipeCard : BaseCard<Piece<HexagonTile>, HexagonTile>
	{
		#region Methods
		public override List<HexagonTile> Positions(Piece<HexagonTile> piece, HexagonTile tile)
		{
			_board.TryGetTile(piece, out HexagonTile playerTile);
			List<HexagonTile> tiles = GetNeighbours(playerTile);

			if (tiles.Contains(tile))
			{
				int direction = tile.GetDirectionFromTile(playerTile);

				_validTiles = new List<HexagonTile>();

				HexagonTile neighbourTile = null;
				if (TryGetNeighbour(playerTile, (direction - 1).Modulo(6), out neighbourTile)) 
					_validTiles.Add(neighbourTile);

				if (TryGetNeighbour(playerTile, (direction).Modulo(6), out neighbourTile))
					_validTiles.Add(neighbourTile);

				if (TryGetNeighbour(playerTile, (direction + 1).Modulo(6), out neighbourTile))
					_validTiles.Add(neighbourTile);
			}
			else
			{
				_validTiles = new List<HexagonTile>(tiles);
			}

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
			};

			backward = () =>
			{
				PlacePieces(piecesToTake);
			};
		}

		protected List<HexagonTile> GetNeighbours(HexagonTile tile)
		{
			List<HexagonTile> tiles = new List<HexagonTile>();

			for (int i = 0; i < 6; i++)
			{
				if(TryGetNeighbour(tile, i, out HexagonTile neighbour))
					tiles.Add(neighbour);
			}

			return tiles;
		}

		protected bool TryGetNeighbour(HexagonTile startingTile, int direction, out HexagonTile neighbourTile)
		{
			neighbourTile = null;
			Vector3Int offset = startingTile.Hexagon.Direction(direction);

			return _grid.TryGetTileAt(startingTile.Hexagon.Q + offset.x, startingTile.Hexagon.R + offset.y, startingTile.Hexagon.S + offset.z, out neighbourTile);
		}
		#endregion
	}
}
