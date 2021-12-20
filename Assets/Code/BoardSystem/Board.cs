using DAE.Commons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAE.BoardSystem
{
	public class Board<TPiece, TPosition>
	{
		#region Properties
		public bool TryGetPiece(TPosition position, out TPiece piece)
			=> _pieces.TryGetKey(position, out piece);

		public bool TryGetPosition(TPiece piece, out TPosition position)
			=> _pieces.TryGetValue(piece, out position);
		#endregion

		#region Fields
		private BidirectionalDictionary<TPiece, TPosition> _pieces = new BidirectionalDictionary<TPiece, TPosition>();

		public event EventHandler<PiecePlacedEventArgs<TPiece, TPosition>> PiecePlaced;
		public event EventHandler<PieceMovedEventArgs<TPiece, TPosition>> PieceMoved;
		public event EventHandler<PieceTakenEventArgs<TPiece, TPosition>> PieceTaken;
		#endregion

		#region Methods
		public void Move(TPiece piece, TPosition toPosition)
		{
			if (!TryGetPosition(piece, out TPosition fromPosition)) return;
			if (TryGetPiece(toPosition, out _)) return;

			if (_pieces.Remove(piece))
				_pieces.Add(piece, toPosition);
			OnPieceMoved(new PieceMovedEventArgs<TPiece, TPosition>(piece, fromPosition, toPosition));
		}

		public void Place(TPiece piece, TPosition position)
		{
			if (_pieces.ContainsKey(piece)) return;
			if (_pieces.ContainsValue(position)) return;

			_pieces.Add(piece, position);

			OnPiecePlaced(new PiecePlacedEventArgs<TPiece, TPosition>(piece, position));
		}

		public void Take(TPiece piece)
		{
			if (!TryGetPosition(piece, out TPosition fromPosition)) return;

			if (_pieces.Remove(piece))
				OnPieceTaken(new PieceTakenEventArgs<TPiece, TPosition>(piece, fromPosition));
		} 
		#endregion

		#region Events
		protected virtual void OnPiecePlaced(PiecePlacedEventArgs<TPiece, TPosition> eventArgs)
		{
			EventHandler<PiecePlacedEventArgs<TPiece, TPosition>> handler = PiecePlaced;
			handler?.Invoke(this, eventArgs);
		}

		protected virtual void OnPieceMoved(PieceMovedEventArgs<TPiece, TPosition> eventArgs)
		{
			EventHandler<PieceMovedEventArgs<TPiece, TPosition>> handler = PieceMoved;
			handler?.Invoke(this, eventArgs);
		}

		protected virtual void OnPieceTaken(PieceTakenEventArgs<TPiece, TPosition> eventArgs)
		{
			EventHandler<PieceTakenEventArgs<TPiece, TPosition>> handler = PieceTaken;
			handler?.Invoke(this, eventArgs);
		} 
		#endregion
	}

	#region EventArgs
	public class PiecePlacedEventArgs<TPiece, TPosition> : EventArgs
	{
		public TPosition AtPosition { get; }
		public TPiece Piece { get; }

		public PiecePlacedEventArgs(TPiece piece, TPosition atPosition)
		{
			AtPosition = atPosition;
			Piece = piece;
		}
	}

	public class PieceMovedEventArgs<TPiece, TPosition>
	{
		public TPosition FromPosition { get; }
		public TPosition ToPosition { get; }
		public TPiece Piece { get; }

		public PieceMovedEventArgs(TPiece piece, TPosition fromPosition, TPosition toPosition)
		{
			FromPosition = fromPosition;
			ToPosition = toPosition;
			Piece = piece;
		}
	}

	public class PieceTakenEventArgs<TPiece, TPosition> : EventArgs
	{
		public TPosition FromPosition { get; }
		public TPiece Piece { get; }

		public PieceTakenEventArgs(TPiece piece, TPosition fromPosition)
		{
			FromPosition = fromPosition;
			Piece = piece;
		}
	} 
	#endregion
}

