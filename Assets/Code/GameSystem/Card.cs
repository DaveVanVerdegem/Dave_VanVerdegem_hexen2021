using DAE.HexenSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DAE.GameSystem
{
	public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler
	{

		#region Inspector Fields
		public CardType CardType = CardType.Teleport;
		#endregion

		#region Properties

		#endregion

		#region Fields
		private RectTransform _rectTransform;

		private Vector3 _originalPosition = Vector3.zero;

		private bool _raycastHit = false;
		private HexagonTile _tileHit = null;
		#endregion

		#region Life Cycle
		private void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();
		}
		#endregion

		#region Methods
		private void CastRay(PointerEventData eventData)
		{
			Ray ray = Camera.main.ScreenPointToRay(eventData.position);
			if (Physics.Raycast(ray, out RaycastHit hitInfo))
			{
				_raycastHit = hitInfo.collider.TryGetComponent(out _tileHit);
			}
			else
				_raycastHit = false;
		}
		#endregion

		#region IDragHandler
		public void OnBeginDrag(PointerEventData eventData)
		{
			_originalPosition = _rectTransform.position;
		}

		public void OnDrag(PointerEventData eventData)
		{
			CastRay(eventData);

			if(_raycastHit)
				GameLoop.Instance.Highlight(this, _tileHit);
			else
				GameLoop.Instance.UnhighlightAll();

			_rectTransform.transform.position = eventData.position;
		}

		public void OnDrop(PointerEventData eventData)
		{
			GameLoop.Instance.UnhighlightAll();

			_rectTransform.position = _originalPosition;
		}
		#endregion
	}
}
