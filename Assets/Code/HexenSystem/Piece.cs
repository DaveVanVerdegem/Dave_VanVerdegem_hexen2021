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
	public class Piece<TPosition> : MonoBehaviour, IPointerClickHandler where TPosition: MonoBehaviour, IPosition
	{
		#region Inspector Fields
		[SerializeField]
		private int _playerId;

		//[SerializeField]
		//private UnityEvent<bool> OnHighlight;
		#endregion

		#region Properties
		public int PlayerID => _playerId;

		public event EventHandler<PieceEventArgs<TPosition>> Placed;
		public event EventHandler<PieceEventArgs<TPosition>> Taken;
		public event EventHandler<PieceEventArgs<TPosition>> Moved;

		//public bool Highlight
		//{
		//	set
		//	{
		//		OnHighlight.Invoke(!value);
		//	}
		//}

		public event EventHandler<ClickEventArgs<TPosition>> Clicked;
		#endregion

		#region Fields
		internal bool _hasMoved { get; set; }
		#endregion

		#region Methods
		internal void MoveTo(TPosition position)
		{
			OnMoved(new PieceEventArgs<TPosition>(position));
		}

		internal void TakeFrom(TPosition position)
		{
			OnTaken(new PieceEventArgs<TPosition>(position));
		}

		internal void PlaceAt(TPosition position)
		{
			OnPlaced(new PieceEventArgs<TPosition>(position));
		}

		protected virtual void OnPlaced(PieceEventArgs<TPosition> eventArgs)
		{
			EventHandler<PieceEventArgs<TPosition>> handler = Placed;
			handler?.Invoke(this, eventArgs);

			transform.position = eventArgs.Position.transform.position;
			gameObject.SetActive(true);
		}

		protected virtual void OnMoved(PieceEventArgs<TPosition> eventArgs)
		{
			EventHandler<PieceEventArgs<TPosition>> handler = Moved;
			handler?.Invoke(this, eventArgs);

			transform.position = eventArgs.Position.transform.position;
		}

		protected virtual void OnTaken(PieceEventArgs<TPosition> eventArgs)
		{
			EventHandler<PieceEventArgs<TPosition>> handler = Taken;
			handler?.Invoke(this, eventArgs);

			gameObject.SetActive(false);
		}
		#endregion

		#region IPointerClickHandler
		public void OnPointerClick(PointerEventData eventData)
		{
			//Debug.Log($"Clicked {gameObject.name}");

			OnClicked(new ClickEventArgs<TPosition>(this));
		}

		protected virtual void OnClicked(ClickEventArgs<TPosition> eventArgs)
		{
			var handler = Clicked;
			handler?.Invoke(this, eventArgs);
		}
		#endregion
	}

	#region EventArgs
	public class PieceEventArgs<TPosition> : EventArgs
	{
		public TPosition Position { get; }

		public PieceEventArgs(TPosition position)
		{
			Position = position;
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

	public class ClickEventArgs<TPosition> : EventArgs where TPosition : MonoBehaviour, IPosition
	{
		public Piece<TPosition> Piece { get; }

		public ClickEventArgs(Piece<TPosition> piece)
		{
			Piece = piece;
		}
	} 
	#endregion
}
