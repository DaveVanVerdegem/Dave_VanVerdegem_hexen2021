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

		#region Inspector Fields
		public CardType CardType = CardType.Teleport;
		#endregion

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

		//private Image _image = null;
		#endregion

		#region Life Cycle
		private void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();
			//_image = GetComponent<Image>();
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
			//_image.raycastTarget = false;

			OnCardBeginDrag(new CardEventArgs<BaseCard<TPiece, TTile>>(this));
		}

		public void OnDrag(PointerEventData eventData)
		{
			_rectTransform.transform.position = eventData.position + Vector2.down;
		}

		public void OnDrop(PointerEventData eventData)
		{
			//GameLoop.Instance.UnhighlightAll();

			Debug.Log("Dropped card");

			_rectTransform.position = _originalPosition;
			//_image.raycastTarget = true;

			OnCardEndDrag(new CardEventArgs<BaseCard<TPiece, TTile>>(this));
		}
		#endregion

		#region Action Methods
		//public List<TTile> Direction(int direction, int maxSteps = int.MaxValue)
		//{
		//	Vector3Int directionVector = Directions.Get(direction);
		//	return Collect(directionVector.x, directionVector.y, directionVector.z, maxSteps);
		//}

		//public List<TTile> Collect(int qOffset, int rOffset, int sOffset, int maxSteps = int.MaxValue)
		//{
		//	List<TTile> tiles = new List<TTile>();

		//	if (!_board.TryGetPosition(GameLoop.Instance.PlayerPiece, out TTile currentTile))
		//		return tiles;

		//	if (!_grid.TryGetCoordinatesAt(currentTile, out (int q, int r, int s) currentCoordinates))
		//		return tiles;

		//	int nextCoordinateQ = currentCoordinates.q + qOffset;
		//	int nextCoordinateR = currentCoordinates.r + rOffset;
		//	int nextCoordinateS = currentCoordinates.s + sOffset;

		//	_grid.TryGetTileAt(nextCoordinateQ, nextCoordinateR, nextCoordinateS, out TTile nextPosition);

		//	int steps = 0;
		//	while (steps < maxSteps && nextPosition != null)
		//	{
		//		//if (_board.TryGetPiece(nextPosition, out Piece<TTile> nextPiece))
		//		//{
		//		//	if (nextPiece.PlayerID != _piece.PlayerID)
		//		//		_validPositions.Add(nextPosition);
		//		//}
		//		//else
		//		//{
		//			validTiles.Add(nextPosition);
		//		//}

		//		nextCoordinateQ += qOffset;
		//		nextCoordinateR += rOffset;
		//		nextCoordinateS += sOffset;

		//		_grid.TryGetTileAt(nextCoordinateQ, nextCoordinateR, nextCoordinateS, out nextPosition);
		//		steps++;
		//	}

		//	return tiles;
		//}
		#endregion
	}
}
