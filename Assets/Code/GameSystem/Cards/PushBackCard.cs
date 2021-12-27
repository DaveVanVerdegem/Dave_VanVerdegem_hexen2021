using DAE.GameSystem;

namespace DAE.GameSystem.Cards
{
	public class PushBackCard : SwipeCard
	{
		#region Methods
		public override bool Execute(Piece<HexagonTile> piece, HexagonTile tile)
		{
			if (!_validTiles.Contains(tile)) return false;

			_board.TryGetTile(piece, out HexagonTile playerTile);

			foreach (HexagonTile targetTile in _validTiles)
			{
				if(_board.TryGetPiece(targetTile, out Piece<HexagonTile> targetPiece))
				{
					PushPiece(targetPiece, targetTile.GetDirectionFromTile(playerTile));
				}
			}

			gameObject.SetActive(false);

			return true;
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
