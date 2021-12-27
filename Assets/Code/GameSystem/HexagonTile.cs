using DAE.GameSystem;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DAE.GameSystem
{
	public class HexagonTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler, ITile
	{
		#region Inspector Fields
		[SerializeField] private UnityEvent OnActivate;
		[SerializeField] private UnityEvent OnDeactivate;
		#endregion

		#region Properties
		public event EventHandler<HexagonTileEventArgs> Entered;
		public event EventHandler<HexagonTileEventArgs> Exited;
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
			GameLoop.Instance.Highlight(this);
		}

		protected virtual void OnEntered(HexagonTileEventArgs eventArgs)
		{
			EventHandler<HexagonTileEventArgs> handler = Entered;
			handler?.Invoke(this, eventArgs);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			GameLoop.Instance.UnhighlightAll();
		}

		protected virtual void OnExited(HexagonTileEventArgs eventArgs)
		{
			EventHandler<HexagonTileEventArgs> handler = Exited;
			handler?.Invoke(this, eventArgs);
		}
		#endregion

		#region Methods
		public int GetDirectionFromTile(HexagonTile otherTile)
		{
			return Directions.Get(Hexagon.Subtract(Hexagon, otherTile.Hexagon).Normalized().ToVector3Int());
		}
		#endregion

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


