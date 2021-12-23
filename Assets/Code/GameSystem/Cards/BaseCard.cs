using DAE.BoardSystem;
using DAE.CardSystem;
using DAE.HexenSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DAE.GameSystem.Cards
{
	public class BaseCard<TTile> : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler
	{

		#region Inspector Fields
		public CardType CardType = CardType.Teleport;
		#endregion

		#region Properties
		public event EventHandler<CardEventArgs<BaseCard<TTile>>> CardBeginDrag;
		public event EventHandler<CardEventArgs<BaseCard<TTile>>> CardEndDrag;
		#endregion

		#region Fields
		private RectTransform _rectTransform;

		private Vector3 _originalPosition = Vector3.zero;

		private Board<Piece<HexagonTile>, TTile> _board;
		private Grid<TTile> _grid;

		private List<TTile> _validPositions = new List<TTile>();
		#endregion

		#region Life Cycle
		private void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();
		}
		#endregion

		#region Methods
		public void OnCardBeginDrag(CardEventArgs<BaseCard<TTile>> eventArgs)
		{
			EventHandler<CardEventArgs<BaseCard<TTile>>> handler = CardBeginDrag;
			handler?.Invoke(this, eventArgs);
		}

		public void OnCardEndDrag(CardEventArgs<BaseCard<TTile>> eventArgs)
		{
			EventHandler<CardEventArgs<BaseCard<TTile>>> handler = CardEndDrag;
			handler?.Invoke(this, eventArgs);
		}
		#endregion

		#region IDragHandler
		public void OnBeginDrag(PointerEventData eventData)
		{
			_originalPosition = _rectTransform.position;
			OnCardBeginDrag(new CardEventArgs<BaseCard<TTile>>(this));
		}

		public void OnDrag(PointerEventData eventData)
		{
			_rectTransform.transform.position = eventData.position;
		}

		public void OnDrop(PointerEventData eventData)
		{
			//GameLoop.Instance.UnhighlightAll();

			_rectTransform.position = _originalPosition;

			OnCardEndDrag(new CardEventArgs<BaseCard<TTile>>(this));
		}
		#endregion

		#region Methods
		public List<TTile> Direction(int direction, int maxSteps = int.MaxValue)
		{
			Vector3Int directionVector = Directions.Get(direction);
			return Collect(directionVector.x, directionVector.y, directionVector.z, maxSteps);
		}

		public List<TTile> Collect(int qOffset, int rOffset, int sOffset, int maxSteps = int.MaxValue)
		{
			List<TTile> tiles = new List<TTile>();

			if (!_board.TryGetPosition(GameLoop.Instance.PlayerPiece, out TTile currentTile))
				return tiles;

			if (!_grid.TryGetCoordinatesAt(currentTile, out (int q, int r, int s) currentCoordinates))
				return tiles;

			int nextCoordinateQ = currentCoordinates.q + qOffset;
			int nextCoordinateR = currentCoordinates.r + rOffset;
			int nextCoordinateS = currentCoordinates.s + sOffset;

			_grid.TryGetPositionAt(nextCoordinateQ, nextCoordinateR, nextCoordinateS, out TTile nextPosition);

			int steps = 0;
			while (steps < maxSteps && nextPosition != null)
			{
				//if (_board.TryGetPiece(nextPosition, out Piece<TTile> nextPiece))
				//{
				//	if (nextPiece.PlayerID != _piece.PlayerID)
				//		_validPositions.Add(nextPosition);
				//}
				//else
				//{
					_validPositions.Add(nextPosition);
				//}

				nextCoordinateQ += qOffset;
				nextCoordinateR += rOffset;
				nextCoordinateS += sOffset;

				_grid.TryGetPositionAt(nextCoordinateQ, nextCoordinateR, nextCoordinateS, out nextPosition);
				steps++;
			}

			return tiles;
		}
		#endregion
	}
}
