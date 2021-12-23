using DAE.HexenSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DAE.GameSystem
{
	public class HexagonTile : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDropHandler, ITile
	{
		#region Inspector Fields
		[SerializeField] private UnityEvent OnActivate;
		[SerializeField] private UnityEvent OnDeactivate;
		#endregion

		#region Properties
		public event EventHandler<HexagonTileEventArgs> Entered;
		public event EventHandler<HexagonTileEventArgs> Exited;
		public event EventHandler<HexagonTileEventArgs> Clicked;
		public Hexagon Hexagon { get; set; }

		public bool Highlight
		{
			set
			{
				if (value)
					OnActivate.Invoke();
				else
					OnDeactivate.Invoke();
			}
		}
		#endregion

		#region IPointerHandler
		public void OnPointerEnter(PointerEventData eventData)
		{
			Debug.Log("Entered Tile.");

			GameLoop.Instance.Highlight(this);
		}

		protected virtual void OnEntered(HexagonTileEventArgs eventArgs)
		{
			EventHandler<HexagonTileEventArgs> handler = Entered;
			handler?.Invoke(this, eventArgs);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			Debug.Log("Exited Tile.");
		}

		protected virtual void OnExited(HexagonTileEventArgs eventArgs)
		{
			EventHandler<HexagonTileEventArgs> handler = Exited;
			handler?.Invoke(this, eventArgs);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			OnClick(new HexagonTileEventArgs(this));

			DebugTile();
		}

		protected virtual void OnClick(HexagonTileEventArgs eventArgs)
		{
			EventHandler<HexagonTileEventArgs> handler = Clicked;
			handler?.Invoke(this, eventArgs);
		}
		#endregion

		#region Methods
		#endregion

		internal void DebugTile()
		{
			Debug.Log($"Tile {name} at GP {Hexagon.ToVector3Int()} and WP {Hexagon.ToWorldPosition()}");
		}

		#region IDropHandler
		public void OnDrop(PointerEventData eventData)
		{
			GameLoop.Instance.Execute(this);
		}
		#endregion
	}

	#region EventArgs
	public class HexagonTileEventArgs : EventArgs
	{
		public HexagonTile Tile { get; }

		public HexagonTileEventArgs(HexagonTile tile)
		{
			Tile = tile;
		}
	} 
	#endregion
}


