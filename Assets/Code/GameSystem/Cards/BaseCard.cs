using DAE.BoardSystem;
using DAE.HexenSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DAE.GameSystem.Cards
{
	public class BaseCard<TPiece, TTile> : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler, ICard<TPiece, TTile>
	{
		#region Properties
		public event EventHandler<CardEventArgs<BaseCard<TPiece, TTile>>> CardBeginDrag;
		public event EventHandler<CardEventArgs<BaseCard<TPiece, TTile>>> CardEndDrag;
		#endregion

		#region Fields
		private RectTransform _rectTransform;

		private Vector3 _originalPosition = Vector3.zero;

		protected Board<TPiece, TTile> _board;
		protected Grid<TTile> _grid;

		protected List<TTile> _validTiles = new List<TTile>();
		#endregion

		#region Life Cycle
		private void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();
		}

		public void Initialize(Board<TPiece, TTile> board, Grid<TTile> grid)
		{
			_board = board;
			_grid = grid;

			gameObject.SetActive(false);
		}
		#endregion

		#region Methods
		public virtual bool Execute(TPiece piece, TTile tile)
		{
			gameObject.SetActive(false);

			return true;
		}

		public virtual List<TTile> Positions(TPiece piece, TTile tile)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region IDragHandler
		public void OnCardBeginDrag(CardEventArgs<BaseCard<TPiece, TTile>> eventArgs)
		{
			EventHandler<CardEventArgs<BaseCard<TPiece, TTile>>> handler = CardBeginDrag;
			handler?.Invoke(this, eventArgs);
		}

		public void OnCardEndDrag(CardEventArgs<BaseCard<TPiece, TTile>> eventArgs)
		{
			EventHandler<CardEventArgs<BaseCard<TPiece, TTile>>> handler = CardEndDrag;
			handler?.Invoke(this, eventArgs);
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			_originalPosition = _rectTransform.position;

			OnCardBeginDrag(new CardEventArgs<BaseCard<TPiece, TTile>>(this));
		}

		public void OnDrag(PointerEventData eventData)
		{
			_rectTransform.transform.position = eventData.position + Vector2.down;
		}

		public void OnDrop(PointerEventData eventData)
		{
			Debug.Log("Dropped card");

			_rectTransform.position = _originalPosition;

			OnCardEndDrag(new CardEventArgs<BaseCard<TPiece, TTile>>(this));
		}
		#endregion
	}
}
