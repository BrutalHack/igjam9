using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace MainGame
{

	public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{

		public void OnBeginDrag (PointerEventData eventData)
		{
			GetComponent <CanvasGroup> ().blocksRaycasts = false;
		}

		public void OnDrag (PointerEventData eventData)
		{
			if (Input.touchCount > 0) {
				transform.position = Input.touches [0].position;
			} else {
				transform.position = Input.mousePosition;
			}
		}

		public void OnEndDrag (PointerEventData eventData)
		{
			transform.localPosition = Vector3.zero;
			GetComponent <CanvasGroup> ().blocksRaycasts = true;
		}
	}
}
