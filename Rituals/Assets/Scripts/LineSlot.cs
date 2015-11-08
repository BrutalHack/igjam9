﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace MainGame
{

	public class LineSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
	{

		public CardinalDirectionEnum CardinalDirectionEnum;
		public Dictionary<LineRenderer, bool> LineRendererWithStartDirection = new Dictionary<LineRenderer, bool> ();
		[HideInInspector]
		public FieldManager fieldManager;
		[HideInInspector]
		public LineRenderer lineRenderer = null;
		[HideInInspector]
		public bool lineDroped = false;

		public void RemoveLineRenderer (LineRenderer lineRenderer)
		{
			Debug.Log ("RemoveLineRenderer");
			if (LineRendererWithStartDirection.ContainsKey (lineRenderer)) {
				LineRendererWithStartDirection.Clear ();
				Debug.Log ("RemoveLineRenderer --- Clear");
			}
		}

		public void CheckForRemovingTheLineRenderer ()
		{
			if (LineRendererWithStartDirection.Count > 0) {
				LineRenderer tempKey = null;
				foreach (LineRenderer key in LineRendererWithStartDirection.Keys) {
					tempKey = key;
					break;
				}
				fieldManager.CascadeRemoveLineRenderer (tempKey, CardinalDirectionEnum);
			}
		}

		public void OnBeginDrag (PointerEventData eventData)
		{
			Debug.Log ("OnBeginDrag");
			lineDroped = false;
			CheckForRemovingTheLineRenderer ();
			lineRenderer = Instantiate (fieldManager.LineRendererPrefab).gameObject.GetComponent <LineRenderer> ();
			lineRenderer.transform.SetParent (fieldManager.transform);
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

				lineSlot.CheckForRemovingTheLineRenderer ();

				lineSlot.lineDroped = true;
				lineSlot.LineRendererWithStartDirection.Add (lineRenderer, true);
				LineRendererWithStartDirection.Add (lineRenderer, false);
				FieldManager.lineConnectionList.Add (CardinalDirectionEnum, lineSlot.CardinalDirectionEnum);
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