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
		internal void MoveTo(TTile position)
		{
			OnMoved(new PieceEventArgs<TTile>(position));
		}

		internal void TakeFrom(TTile position)
		{
			OnTaken(new PieceEventArgs<TTile>(position));
		}

		internal void PlaceAt(TTile position)
		{
			OnPlaced(new PieceEventArgs<TTile>(position));
		}

		protected virtual void OnPlaced(PieceEventArgs<TTile> eventArgs)
		{
			EventHandler<PieceEventArgs<TTile>> handler = Placed;
			handler?.Invoke(this, eventArgs);

			transform.position = eventArgs.Position.transform.position;
			gameObject.SetActive(true);
		}

		protected virtual void OnMoved(PieceEventArgs<TTile> eventArgs)
		{
			EventHandler<PieceEventArgs<TTile>> handler = Moved;
			handler?.Invoke(this, eventArgs);

			transform.position = eventArgs.Position.transform.position;
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
			//Debug.Log($"Clicked {gameObject.name}");

			OnClicked(new ClickEventArgs<TTile>(this));
		}

		protected virtual void OnClicked(ClickEventArgs<TTile> eventArgs)
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

	public class ClickEventArgs<TPosition> : EventArgs where TPosition : MonoBehaviour, ITile
	{
		public Piece<TPosition> Piece { get; }

		public ClickEventArgs(Piece<TPosition> piece)
		{
			Piece = piece;
		}
	} 
	#endregion
}
