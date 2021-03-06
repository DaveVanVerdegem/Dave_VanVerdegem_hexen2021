using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DAE.GameSystem.Cards
{
	public class BaseCard<TPiece, TTile> : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, ICard<TPiece, TTile> where TTile : ITile
	{
		#region Properties
		public event EventHandler<CardEventArgs<BaseCard<TPiece, TTile>>> CardBeginDrag;
		public event EventHandler<CardEventArgs<BaseCard<TPiece, TTile>>> CardEndDrag;
		#endregion

		#region Fields
		protected Board<TPiece, TTile> _board;
		protected Grid<TTile> _grid;
		protected List<TTile> _validTiles = new List<TTile>();

		private RectTransform _rectTransform;
		private Image _image;
		private Vector3 _originalPosition = Vector3.zero;

		private bool _dragging = false;
		#endregion

		#region Life Cycle
		private void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();
			_image = GetComponent<Image>();
		}

		public void Initialize(Board<TPiece, TTile> board, Grid<TTile> grid)
		{
			_board = board;
			_grid = grid;

			gameObject.SetActive(false);
		}
		#endregion

		#region Methods
		public virtual void Execute(TPiece piece, TTile tile, out Action forward, out Action backward)
		{
			forward = null;
			backward = null;

			if (!_board.TryGetTile(piece, out TTile previousTile))
				return;

			forward = () =>
			{
				gameObject.SetActive(false);
			};

			backward = () =>
			{
				gameObject.SetActive(true);
			};
		}

		public bool CanExecute(TTile tile)
			=> _validTiles.Contains(tile);

		public virtual List<TTile> Positions(TPiece piece, TTile tile)
		{
			throw new NotImplementedException();
		}

		protected Dictionary<TPiece, TTile> PiecesOnValidTiles()
		{
			Dictionary<TPiece, TTile> pieces = new Dictionary<TPiece, TTile>();

			foreach (TTile hexagonTile in _validTiles)
			{
				if (_board.TryGetPiece(hexagonTile, out TPiece pieceInRange))
					pieces.Add(pieceInRange, hexagonTile);
			}

			return pieces;
		}

		protected void TakePieces(Dictionary<TPiece, TTile> pieces)
		{
			foreach (KeyValuePair<TPiece, TTile> piece in pieces)
				_board.Take(piece.Key);
		}

		protected void PlacePieces(Dictionary<TPiece, TTile> pieces)
		{
			foreach (KeyValuePair<TPiece, TTile> piece in pieces)
			{
				_board.Place(piece.Key, piece.Value);
			}	
		}

		protected void RemoveTiles(List<TTile> tiles)
		{
			foreach (TTile hexagonTile in tiles)
			{
				hexagonTile.Remove();
				_board.RemoveTile(hexagonTile);
			}
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
			if (!GameLoop.Instance.InPlayState) return;

			_originalPosition = _rectTransform.position;
			_image.raycastTarget = false;
			_dragging = true;

			OnCardBeginDrag(new CardEventArgs<BaseCard<TPiece, TTile>>(this));
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (!_dragging) return;

			_rectTransform.transform.position = eventData.position;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (!_dragging) return;

			_rectTransform.position = _originalPosition;
			_image.raycastTarget = true;
			_dragging = false;

			OnCardEndDrag(new CardEventArgs<BaseCard<TPiece, TTile>>(this));
		}
		#endregion
	}
}
