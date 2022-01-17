using DAE.Commons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAE.BoardSystem
{
	public class Board<TPiece, TTile>
	{
		#region Properties
		public bool TryGetPiece(TTile tile, out TPiece piece)
			=> _pieces.TryGetKey(tile, out piece);

		public bool TryGetTile(TPiece piece, out TTile tile)
			=> _pieces.TryGetValue(piece, out tile);
		#endregion

		#region Fields
		private BidirectionalDictionary<TPiece, TTile> _pieces = new BidirectionalDictionary<TPiece, TTile>();

		public event EventHandler<PiecePlacedEventArgs<TPiece, TTile>> PiecePlaced;
		public event EventHandler<PieceMovedEventArgs<TPiece, TTile>> PieceMoved;
		public event EventHandler<PieceTakenEventArgs<TPiece, TTile>> PieceTaken;
		#endregion

		#region Methods
		public void Move(TPiece piece, TTile toTile)
		{
			if (!TryGetTile(piece, out TTile fromTile)) return;
			if (TryGetPiece(toTile, out _)) return;

			if (_pieces.Remove(piece))
				_pieces.Add(piece, toTile);
			OnPieceMoved(new PieceMovedEventArgs<TPiece, TTile>(piece, fromTile, toTile));
		}

		public void Place(TPiece piece, TTile tile)
		{
			if (_pieces.ContainsKey(piece)) return;
			if (_pieces.ContainsValue(tile)) return;

			_pieces.Add(piece, tile);

			OnPiecePlaced(new PiecePlacedEventArgs<TPiece, TTile>(piece, tile));
		}

		public void Take(TPiece piece)
		{
			if (!TryGetTile(piece, out TTile fromPosition)) return;

			if (_pieces.Remove(piece))
				OnPieceTaken(new PieceTakenEventArgs<TPiece, TTile>(piece, fromPosition));
		} 

		public void RemoveTile(TTile tile)
		{
			_pieces.Remove(tile);
		}
		#endregion

		#region Events
		protected virtual void OnPiecePlaced(PiecePlacedEventArgs<TPiece, TTile> eventArgs)
		{
			EventHandler<PiecePlacedEventArgs<TPiece, TTile>> handler = PiecePlaced;
			handler?.Invoke(this, eventArgs);
		}

		protected virtual void OnPieceMoved(PieceMovedEventArgs<TPiece, TTile> eventArgs)
		{
			EventHandler<PieceMovedEventArgs<TPiece, TTile>> handler = PieceMoved;
			handler?.Invoke(this, eventArgs);
		}

		protected virtual void OnPieceTaken(PieceTakenEventArgs<TPiece, TTile> eventArgs)
		{
			EventHandler<PieceTakenEventArgs<TPiece, TTile>> handler = PieceTaken;
			handler?.Invoke(this, eventArgs);
		} 
		#endregion
	}

	#region EventArgs
	public class PiecePlacedEventArgs<TPiece, TTile> : EventArgs
	{
		public TTile AtTile { get; }
		public TPiece Piece { get; }

		public PiecePlacedEventArgs(TPiece piece, TTile atTile)
		{
			AtTile = atTile;
			Piece = piece;
		}
	}

	public class PieceMovedEventArgs<TPiece, TTile>
	{
		public TTile FromTile { get; }
		public TTile ToTile { get; }
		public TPiece Piece { get; }

		public PieceMovedEventArgs(TPiece piece, TTile fromPosition, TTile toPosition)
		{
			FromTile = fromPosition;
			ToTile = toPosition;
			Piece = piece;
		}
	}

	public class PieceTakenEventArgs<TPiece, TTile> : EventArgs
	{
		public TTile FromTile { get; }
		public TPiece Piece { get; }

		public PieceTakenEventArgs(TPiece piece, TTile fromTile)
		{
			FromTile = fromTile;
			Piece = piece;
		}
	} 
	#endregion
}

