using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace MainGame
{

	public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		[HideInInspector]
		public Camera camera;

		public void OnBeginDrag (PointerEventData eventData)
		{
			GetComponent <CanvasGroup> ().blocksRaycasts = false;
		}

		public void OnDrag (PointerEventData eventData)
		{
			Vector2 inputPosition;
			if (Input.touchCount > 0) {
				inputPosition = Input.touches [0].position;
			} else {
				inputPosition = Input.mousePosition;
			}
			//TODO translate Verctor2d to Camera Position
			Vector3 newPos = camera.ScreenToWorldPoint (new Vector3 (inputPosition.x, inputPosition.y, 100));
			transform.position = newPos;
		}

		public void OnEndDrag (PointerEventData eventData)
		{
			transform.localPosition = Vector3.zero;
			GetComponent <CanvasGroup> ().blocksRaycasts = true;
		}
	}
}
