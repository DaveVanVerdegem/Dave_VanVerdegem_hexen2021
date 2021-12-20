using DAE.BoardSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DAE.HexenSystem
{
	public class MovementHelper<TPosition> where TPosition : MonoBehaviour, IPosition
	{
		#region Properties
		public delegate bool Validator(Board<Piece<TPosition>, TPosition> board, Grid<TPosition> grid, Piece<TPosition> piece, TPosition toPosition);
		#endregion

		#region Fields
		private Board<Piece<TPosition>, TPosition> _board;
		private Grid<TPosition> _grid;
		private Piece<TPosition> _piece;

		private List<TPosition> _validPositions = new List<TPosition>();
		private List<TPosition> _targetPositions = new List<TPosition>();
		#endregion

		#region Constructors
		public MovementHelper(Board<Piece<TPosition>, TPosition> board, Grid<TPosition> grid, Piece<TPosition> piece)
		{
			_board = board;
			_grid = grid;
			_piece = piece;
		}
		#endregion

		#region Methods
		public List<TPosition> CollectValidPositions()
		{
			return _validPositions;
		}

		public List<TPosition> CollectTargetPositions()
		{
			return _targetPositions;
		}

		public static bool Empty(Board<Piece<TPosition>, TPosition> board, Grid<TPosition> grid, Piece<TPosition> piece, TPosition toPosition)
			=> !board.TryGetPiece(toPosition, out Piece<TPosition> _);

		public static bool ContainsEnemy(Board<Piece<TPosition>, TPosition> board, Grid<TPosition> grid, Piece<TPosition> piece, TPosition toPosition)
		   => board.TryGetPiece(toPosition, out Piece<TPosition> toPiece) && toPiece.PlayerID != piece.PlayerID;
		#endregion

		#region Moves
		public MovementHelper<TPosition> Collect(int qOffset, int rOffset, int sOffset, int maxSteps = int.MaxValue, params Validator[] validators)
		{
			if (!_board.TryGetPosition(_piece, out TPosition currentPosition))
				return this;

			if (!_grid.TryGetCoordinatesAt(currentPosition, out (int q, int r, int s) currentCoordinates))
				return this;

			int nextCoordinateQ = currentCoordinates.q + qOffset;
			int nextCoordinateR = currentCoordinates.r + rOffset;
			int nextCoordinateS = currentCoordinates.s + sOffset;

			_grid.TryGetPositionAt(nextCoordinateQ, nextCoordinateR, nextCoordinateS, out TPosition nextPosition);

			int steps = 0;
			while (steps < maxSteps && nextPosition != null && validators.All((validator) => validator(_board, _grid, _piece, nextPosition)))
			{
				if (_board.TryGetPiece(nextPosition, out Piece<TPosition> nextPiece))
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

				_grid.TryGetPositionAt(nextCoordinateQ, nextCoordinateR, nextCoordinateS, out nextPosition);
				steps++;
			}

			return this;
		}

		public MovementHelper<TPosition> Direction(int direction,int maxSteps = int.MaxValue, params Validator[] validators)
		{
			Vector3Int directionVector = Directions.Get(direction);
			return Collect(directionVector.x, directionVector.y, directionVector.z, maxSteps, validators);
		}
		#endregion
	}
}
