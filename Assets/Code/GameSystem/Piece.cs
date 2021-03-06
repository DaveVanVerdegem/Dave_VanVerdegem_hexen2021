using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DAE.GameSystem
{
	public class Piece<TTile> : MonoBehaviour, IPointerClickHandler where TTile: MonoBehaviour, ITile
	{
		#region Properties
		public event EventHandler<PieceEventArgs<TTile>> Placed;
		public event EventHandler<PieceEventArgs<TTile>> Taken;
		public event EventHandler<PieceEventArgs<TTile>> Moved;

		public event EventHandler<ClickEventArgs<TTile>> Clicked;
		#endregion

		#region Fields
		internal bool _hasMoved { get; set; }
		#endregion

		#region Methods
		public void MoveTo(TTile tile)
		{
			transform.position = tile.transform.position;

			OnMoved(new PieceEventArgs<TTile>(tile));
		}

		public void TakeFrom(TTile tile)
		{
			gameObject.SetActive(false);

			OnTaken(new PieceEventArgs<TTile>(tile));
		}

		public void PlaceAt(TTile tile)
		{
			transform.position = tile.transform.position;
			gameObject.SetActive(true);

			OnPlaced(new PieceEventArgs<TTile>(tile));
		}
		#endregion

		#region Events
		protected virtual void OnPlaced(PieceEventArgs<TTile> eventArgs)
		{
			EventHandler<PieceEventArgs<TTile>> handler = Placed;
			handler?.Invoke(this, eventArgs);
		}

		protected virtual void OnMoved(PieceEventArgs<TTile> eventArgs)
		{
			EventHandler<PieceEventArgs<TTile>> handler = Moved;
			handler?.Invoke(this, eventArgs);
		}

		protected virtual void OnTaken(PieceEventArgs<TTile> eventArgs)
		{
			EventHandler<PieceEventArgs<TTile>> handler = Taken;
			handler?.Invoke(this, eventArgs);
		}
		#endregion

		#region IPointerClickHandler
		public void OnPointerClick(PointerEventData eventData)
		{
			OnClicked(new ClickEventArgs<TTile>(this));
		}

		protected virtual void OnClicked(ClickEventArgs<TTile> eventArgs)
		{
			EventHandler<ClickEventArgs<TTile>> handler = Clicked;
			handler?.Invoke(this, eventArgs);
		}
		#endregion
	}

	#region EventArgs
	public class PieceEventArgs<TTile> : EventArgs
	{
		public TTile Tile { get; }

		public PieceEventArgs(TTile tile)
		{
			Tile = tile;
		}
	}

	public class ActivateEventArgs : EventArgs
	{
		public bool Status { get; }

		public ActivateEventArgs(bool status)
		{
			Status = status;
		}
	}

	public class ClickEventArgs<TTile> : EventArgs where TTile : MonoBehaviour, ITile
	{
		public Piece<TTile> Piece { get; }

		public ClickEventArgs(Piece<TTile> piece)
		{
			Piece = piece;
		}
	} 
	#endregion
}
