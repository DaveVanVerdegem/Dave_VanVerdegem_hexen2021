using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DAE.HexenSystem
{
	public class Piece<TTile> : MonoBehaviour, IPointerClickHandler where TTile: MonoBehaviour, ITile
	{
		#region Inspector Fields
		[SerializeField]
		private int _playerId;

		//[SerializeField]
		//private UnityEvent<bool> OnHighlight;
		#endregion

		#region Properties
		public int PlayerID => _playerId;

		public event EventHandler<PieceEventArgs<TTile>> Placed;
		public event EventHandler<PieceEventArgs<TTile>> Taken;
		public event EventHandler<PieceEventArgs<TTile>> Moved;

		//public bool Highlight
		//{
		//	set
		//	{
		//		OnHighlight.Invoke(!value);
		//	}
		//}

		public event EventHandler<ClickEventArgs<TTile>> Clicked;
		#endregion

		#region Fields
		internal bool _hasMoved { get; set; }
		#endregion

		#region Methods
		public void MoveTo(TTile tile)
		{
			OnMoved(new PieceEventArgs<TTile>(tile));
		}

		public void TakeFrom(TTile tile)
		{
			OnTaken(new PieceEventArgs<TTile>(tile));
		}

		public void PlaceAt(TTile tile)
		{
			OnPlaced(new PieceEventArgs<TTile>(tile));
		}

		protected virtual void OnPlaced(PieceEventArgs<TTile> eventArgs)
		{
			EventHandler<PieceEventArgs<TTile>> handler = Placed;
			handler?.Invoke(this, eventArgs);

			transform.position = eventArgs.Tile.transform.position;
			gameObject.SetActive(true);
		}

		protected virtual void OnMoved(PieceEventArgs<TTile> eventArgs)
		{
			EventHandler<PieceEventArgs<TTile>> handler = Moved;
			handler?.Invoke(this, eventArgs);

			transform.position = eventArgs.Tile.transform.position;
		}

		protected virtual void OnTaken(PieceEventArgs<TTile> eventArgs)
		{
			EventHandler<PieceEventArgs<TTile>> handler = Taken;
			handler?.Invoke(this, eventArgs);

			gameObject.SetActive(false);
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
