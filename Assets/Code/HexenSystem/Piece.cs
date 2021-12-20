using DAE.HexagonalSystem;
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
	public class Piece<TPosition> : MonoBehaviour, IPointerClickHandler where TPosition: MonoBehaviour
	{
		#region Inspector Fields
		[SerializeField]
		private int _playerId;

		[SerializeField]
		private UnityEvent<bool> OnHighlight;
		#endregion

		#region Properties
		public int PlayerID { get; set; }
		public event EventHandler<ActivateEventArgs> ActivationStatusChanged;
		public event EventHandler<ClickEventArgs<TPosition>> Clicked;

		public bool Activate
		{
			get => _activate;
			set
			{
				_activate = value;
				OnActivationStatusChanged(new ActivateEventArgs(_activate));
			}
		}
		#endregion

		#region Fields
		internal bool _hasMoved { get; set; }

		private bool _activate;
		#endregion

		#region Life Cycle
		private void Start()
		{
			ActivationStatusChanged -= OnPieceActivationChanged;
		}

		private void OnDisable()
		{
			ActivationStatusChanged += OnPieceActivationChanged;
		}
		#endregion

		#region Methods
		public bool Highlight
		{
			set
			{
				OnHighlight.Invoke(!value);
			}
		}

		private void OnPieceActivationChanged(object source, ActivateEventArgs eventArgs)
		{
			Debug.Log("ACTIVATED: " + eventArgs.Status);
		}

		protected virtual void OnActivationStatusChanged(ActivateEventArgs eventArgs)
		{
			EventHandler<ActivateEventArgs> handler = ActivationStatusChanged;
			handler?.Invoke(this, eventArgs);
		}
		#endregion

		#region IPointerClickHandler
		public void OnPointerClick(PointerEventData eventData)
		{
			Debug.Log($"Clicked {gameObject.name}");

			OnClicked(new ClickEventArgs<TPosition>(this));
		}

		protected virtual void OnClicked(ClickEventArgs<TPosition> eventArgs)
		{
			EventHandler<ClickEventArgs<TPosition>> handler = Clicked;
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

	public class ClickEventArgs<TPosition> : EventArgs where TPosition : MonoBehaviour
	{
		public Piece<TPosition> Piece { get; }

		public ClickEventArgs(Piece<TPosition> piece)
		{
			Piece = piece;
		}
	} 
	#endregion
}
