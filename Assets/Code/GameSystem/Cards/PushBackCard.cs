using System;
using System.Collections.Generic;

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

			CollectAffectedPieces(out Dictionary<Piece<HexagonTile>, HexagonTile> piecesPushed, out Dictionary<Piece<HexagonTile>, HexagonTile> piecesTaken);

			forward = () =>
			{
				foreach (HexagonTile targetTile in _validTiles)
				{
					if (_board.TryGetPiece(targetTile, out Piece<HexagonTile> targetPiece))
						PushPiece(targetPiece, targetTile.GetDirectionFromTile(playerTile));
				}
			};

			backward = () =>
			{
				foreach (KeyValuePair<Piece<HexagonTile>, HexagonTile> piece in piecesTaken)
					_board.Place(piece.Key, piece.Value);

				foreach (KeyValuePair<Piece<HexagonTile>, HexagonTile> piece in piecesPushed)
					_board.Move(piece.Key, piece.Value);
			};
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

		private void CollectAffectedPieces(out Dictionary<Piece<HexagonTile>, HexagonTile> piecesPushed, out Dictionary<Piece<HexagonTile>, HexagonTile> piecesTaken)
		{
			piecesPushed = new Dictionary<Piece<HexagonTile>, HexagonTile>();
			piecesTaken = new Dictionary<Piece<HexagonTile>, HexagonTile>();

			_board.TryGetTile(GameLoop.Instance.PlayerPiece, out HexagonTile playerTile);

			foreach (HexagonTile tile in _validTiles)
			{
				if (_board.TryGetPiece(tile, out Piece<HexagonTile> targetPiece))
				{
					if (!TryGetNeighbour(tile, tile.GetDirectionFromTile(playerTile), out HexagonTile neighbourTile))
					{
						piecesTaken.Add(targetPiece, tile);
						continue;
					}

					if (_board.TryGetPiece(neighbourTile, out _)) continue;

					piecesPushed.Add(targetPiece, tile);
				}
			}
		}
		#endregion
	}
}
