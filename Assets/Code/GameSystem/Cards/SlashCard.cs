using DAE.HexenSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DAE.GameSystem.Cards
{
	public class SlashCard : BaseCard<Piece<HexagonTile>, HexagonTile>
	{
		#region Methods
		public override List<HexagonTile> Positions(Piece<HexagonTile> piece, HexagonTile tile)
		{
			List<HexagonTile> tiles = Direction(0);
			tiles.AddRange(Direction(1));
			tiles.AddRange(Direction(2));
			tiles.AddRange(Direction(3));
			tiles.AddRange(Direction(4));
			tiles.AddRange(Direction(5));

			if (tiles.Contains(tile))
			{
				_board.TryGetTile(piece, out HexagonTile pieceTile);
				int direction = tile.GetDirectionFromTile(pieceTile);

				_validTiles = Direction(direction);
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

		private List<HexagonTile> Direction(int direction, int maxSteps = int.MaxValue)
		{
			Vector3Int directionVector = Directions.Get(direction);
			return Collect(directionVector.x, directionVector.y, directionVector.z, maxSteps);
		}

		private List<HexagonTile> Collect(int qOffset, int rOffset, int sOffset, int maxSteps = int.MaxValue)
		{
			List<HexagonTile> tiles = new List<HexagonTile>();

			if (!_board.TryGetTile(GameLoop.Instance.PlayerPiece, out HexagonTile currentTile))
				return tiles;

			if (!_grid.TryGetCoordinatesAt(currentTile, out (int q, int r, int s) currentCoordinates))
				return tiles;

			int nextCoordinateQ = currentCoordinates.q + qOffset;
			int nextCoordinateR = currentCoordinates.r + rOffset;
			int nextCoordinateS = currentCoordinates.s + sOffset;

			_grid.TryGetTileAt(nextCoordinateQ, nextCoordinateR, nextCoordinateS, out HexagonTile nextPosition);

			int steps = 0;
			while (steps < maxSteps && nextPosition != null)
			{
				tiles.Add(nextPosition);

				nextCoordinateQ += qOffset;
				nextCoordinateR += rOffset;
				nextCoordinateS += sOffset;

				_grid.TryGetTileAt(nextCoordinateQ, nextCoordinateR, nextCoordinateS, out nextPosition);
				steps++;
			}

			return tiles;
		}
		#endregion
	}
}
