using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DAE.HexagonalSystem
{
	public class HexagonTile : MonoBehaviour, IPointerClickHandler
	{
		#region Inspector Fields
		[SerializeField] private UnityEvent OnActivate;
		[SerializeField] private UnityEvent OnDeactivate;
		#endregion

		#region Properties
		public event EventHandler<HexagonTileEventArgs> Clicked;
		public Hexagon Hexagon { get; set; }
		#endregion

		#region IPointerClickHandler
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

		internal void DebugTile()
		{
			Debug.Log($"Tile {name} at GP {Hexagon.ToVector3Int()} and WP {Hexagon.ToWorldPosition()}");
		}
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


