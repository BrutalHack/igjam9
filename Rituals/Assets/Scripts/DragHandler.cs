using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace MainGame
{

	public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{

		private Vector3 startPosition;

		public void OnBeginDrag (PointerEventData eventData)
		{
			startPosition = transform.position;
		}

		public void OnDrag (PointerEventData eventData)
		{
			throw new System.NotImplementedException ();
		}

		public void OnEndDrag (PointerEventData eventData)
		{
			throw new System.NotImplementedException ();
		}
	}
}
