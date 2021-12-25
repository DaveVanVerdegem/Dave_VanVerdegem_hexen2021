using DAE.BoardSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DAE.HexenSystem
{
	public class MovementHelper<TTile> where TTile : MonoBehaviour, ITile
	{
		#region Properties
		public delegate bool Validator(Board<Piece<TTile>, TTile> board, Grid<TTile> grid, Piece<TTile> piece, TTile toPosition);
		#endregion

		#region Fields
		private Board<Piece<TTile>, TTile> _board;
		private Grid<TTile> _grid;
		private Piece<TTile> _piece;

		private List<TTile> _validPositions = new List<TTile>();
		private List<TTile> _targetPositions = new List<TTile>();
		#endregion

		#region Constructors
		public MovementHelper(Board<Piece<TTile>, TTile> board, Grid<TTile> grid, Piece<TTile> piece)
		{
			_board = board;
			_grid = grid;
			_piece = piece;
		}
		#endregion

		#region Methods
		public List<TTile> CollectValidPositions()
		{
			return _validPositions;
		}

		public List<TTile> CollectTargetPositions()
		{
			return _targetPositions;
		}

		public static bool Empty(Board<Piece<TTile>, TTile> board, Grid<TTile> grid, Piece<TTile> piece, TTile toPosition)
			=> !board.TryGetPiece(toPosition, out Piece<TTile> _);

		public static bool ContainsEnemy(Board<Piece<TTile>, TTile> board, Grid<TTile> grid, Piece<TTile> piece, TTile toPosition)
		   => board.TryGetPiece(toPosition, out Piece<TTile> toPiece) && toPiece.PlayerID != piece.PlayerID;
		#endregion

		#region Moves
		public MovementHelper<TTile> Collect(int qOffset, int rOffset, int sOffset, int maxSteps = int.MaxValue, params Validator[] validators)
		{
			if (!_board.TryGetTile(_piece, out TTile currentPosition))
				return this;

			if (!_grid.TryGetCoordinatesAt(currentPosition, out (int q, int r, int s) currentCoordinates))
				return this;

			int nextCoordinateQ = currentCoordinates.q + qOffset;
			int nextCoordinateR = currentCoordinates.r + rOffset;
			int nextCoordinateS = currentCoordinates.s + sOffset;

			_grid.TryGetTileAt(nextCoordinateQ, nextCoordinateR, nextCoordinateS, out TTile nextPosition);

			int steps = 0;
			while (steps < maxSteps && nextPosition != null && validators.All((validator) => validator(_board, _grid, _piece, nextPosition)))
			{
				if (_board.TryGetPiece(nextPosition, out Piece<TTile> nextPiece))
				{
					if (nextPiece.PlayerID != _piece.PlayerID)
						_validPositions.Add(nextPosition);
				}
				else
				{
					_validPositions.Add(nextPosition);
				}

				nextCoordinateQ += qOffset;
				nextCoordinateR += rOffset;
				nextCoordinateS += sOffset;

				_grid.TryGetTileAt(nextCoordinateQ, nextCoordinateR, nextCoordinateS, out nextPosition);
				steps++;
			}

			return this;
		}

		public MovementHelper<TTile> Direction(int direction,int maxSteps = int.MaxValue, params Validator[] validators)
		{
			Vector3Int directionVector = Directions.Get(direction);
			return Collect(directionVector.x, directionVector.y, directionVector.z, maxSteps, validators);
		}
		#endregion
	}
}
