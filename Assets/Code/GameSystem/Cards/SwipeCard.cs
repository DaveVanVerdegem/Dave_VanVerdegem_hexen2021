using DAE.HexenSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAE.GameSystem.Cards
{
	public class SwipeCard : BaseCard<Piece<HexagonTile>, HexagonTile>
	{
		#region Methods
		public override List<HexagonTile> Positions(Piece<HexagonTile> piece, HexagonTile tile)
		{
			_board.TryGetPosition(piece, out HexagonTile playerTile);
			List<HexagonTile> tiles = GetNeighbours(playerTile);

			if (tiles.Contains(tile))
			{
				int direction = tile.GetDirectionFromTile(playerTile);

				_validTiles = new List<HexagonTile>
				{
					GetNeighbour(playerTile, (direction - 1).Modulo(6)),
					GetNeighbour(playerTile, (direction).Modulo(6)),
					GetNeighbour(playerTile, (direction + 1).Modulo(6))
				};
			}
			else
			{
				_validTiles = new List<HexagonTile>(tiles);
			}

			return _validTiles;
		}

		public override bool Execute(Piece<HexagonTile> piece, HexagonTile tile)
		{
			if (!_validTiles.Contains(tile)) return false;

			TakePiecesOnValidTiles();

			base.Execute(piece, tile);

			return true;
		}

		private List<HexagonTile> GetNeighbours(HexagonTile tile)
		{
			List<HexagonTile> tiles = new List<HexagonTile>();

			for (int i = 0; i < 6; i++)
			{
				HexagonTile hexagonTile = GetNeighbour(tile, i);
				if (hexagonTile != null)
					tiles.Add(hexagonTile);
			}

			return tiles;
		}

		private HexagonTile GetNeighbour(HexagonTile tile, int direction)
		{
			Vector3Int offset = tile.Hexagon.Direction(direction);

			_grid.TryGetTileAt(tile.Hexagon.Q + offset.x, tile.Hexagon.R + offset.y, tile.Hexagon.S + offset.z, out HexagonTile neighbourTile);

			return neighbourTile;	
		}
		#endregion
	}
}
