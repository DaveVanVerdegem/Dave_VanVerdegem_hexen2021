using DAE.GameSystem;
using System;

namespace DAE.GameSystem.Cards
{
	public class PushBackCard : SwipeCard
	{
		#region Methods
		public override void Execute(Piece<HexagonTile> piece, HexagonTile tile, out Action forward, out Action backward)
		{
			forward = null;
			backward = null;

			if (!_validTiles.Contains(tile)) return;

			_board.TryGetTile(piece, out HexagonTile playerTile);

			foreach (HexagonTile targetTile in _validTiles)
			{
				if(_board.TryGetPiece(targetTile, out Piece<HexagonTile> targetPiece))
				{
					PushPiece(targetPiece, targetTile.GetDirectionFromTile(playerTile));
				}
			}

			gameObject.SetActive(false);
		}

		private void PushPiece(Piece<HexagonTile> piece, int direction)
		{
			if (_board.TryGetTile(piece, out HexagonTile pieceTile) == false) return;


			if(!TryGetNeighbour(pieceTile, direction, out HexagonTile targetTile))
			{
				_board.Take(piece);
				return;
			}

			if (_board.TryGetPiece(targetTile, out _)) return;

			_board.Move(piece, targetTile);
		}
		#endregion
	}
}
