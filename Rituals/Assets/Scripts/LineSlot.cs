using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace MainGame
{

	public class LineSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
	{

		public CardinalDirectionEnum CardinalDirectionEnum;
		[HideInInspector]
		public FieldManager fieldManager;
		public LineRenderer lineRenderer = null;
		[HideInInspector]
		public bool lineDroped = false;

		public void OnBeginDrag (PointerEventData eventData)
		{
			Debug.Log ("OnBeginDrag");
			lineDroped = false;
			if (!FieldManager.CardinalDirectionForConnectionIsFree (CardinalDirectionEnum)) {
				fieldManager.DeleteConnection (CardinalDirectionEnum);
			} 
			GameObject lineR = Instantiate (fieldManager.LineRendererPrefab);
			lineR.transform.SetParent (fieldManager.transform);
			lineRenderer = lineR.GetComponent<LineRenderer> ();
			lineRenderer.SetPosition (0, new Vector3 (transform.position.x, transform.position.y, 0));
			lineRenderer.SetPosition (1, new Vector3 (transform.position.x, transform.position.y, 0));
		}

		void IDragHandler.OnDrag (PointerEventData eventData)
		{
			Vector2 position;
			if (Input.touchCount > 0) {
				position = Input.touches [0].position;
			} else {
				position = Input.mousePosition;
			}
			Vector3 worldPosition = fieldManager.camera.ScreenToWorldPoint (new Vector3 (position.x, position.y, fieldManager.camera.nearClipPlane));
			lineRenderer.SetPosition (1, new Vector3 (worldPosition.x, worldPosition.y, 0));
		}

		public void OnDrop (PointerEventData eventData)
		{
			Debug.Log ("OnDrop");

			LineSlot lineSlot = eventData.pointerDrag.GetComponent <LineSlot> ();
			if (lineSlot != null) {
				lineSlot.lineDroped = true;
				if (!FieldManager.CardinalDirectionForConnectionIsFree (CardinalDirectionEnum)) {
					fieldManager.DeleteConnection (CardinalDirectionEnum);
				} 
				lineRenderer = lineSlot.lineRenderer;
				FieldManager.lineConnectionList.Add (lineSlot.CardinalDirectionEnum, CardinalDirectionEnum);
			}
		}

		public void OnEndDrag (PointerEventData eventData)
		{
			Debug.Log ("OnEndDrag");
			if (!lineDroped) {
				Destroy (lineRenderer);
				lineRenderer = null;
			}
		}
	}
}
