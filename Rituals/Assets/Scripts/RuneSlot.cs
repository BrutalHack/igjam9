using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

namespace MainGame
{

	public class RuneSlot : MonoBehaviour, IDropHandler
	{

		public CardinalDirectionEnum CardinalDirectionEnum;
		public bool innerCircle;
		[HideInInspector]
		public FieldManager fieldManager;

		private Rune GetChild ()
		{
			if (transform.childCount > 0) {
				Rune childRune = transform.GetChild (0).GetComponent <Rune> ();
				if (childRune != null) {
					return childRune;
				} else {
					while (transform.childCount > 0) {
						Destroy (transform.GetChild (0));
					}
					return null;
				}
			} else {
				return null;
			}
		}

		public void OnDrop (PointerEventData eventData)
		{
			Rune dropRune = eventData.pointerDrag.GetComponent <Rune> ();
			if (dropRune != null) {
				Rune childRune = GetChild ();
				if (childRune == dropRune) {
					return;
				}
				FieldManager.symbolPositionList.Remove (dropRune.Symbol);
				if (childRune != null) {
					FieldManager.symbolPositionList.Remove (childRune.Symbol);
					childRune.transform.SetParent (dropRune.transform.parent, false);
					RuneSlot originRuneSlot = dropRune.transform.parent.gameObject.GetComponent<RuneSlot> ();
					if (originRuneSlot.innerCircle) {
						FieldManager.symbolPositionList.Add (childRune.Symbol, 
							originRuneSlot.CardinalDirectionEnum);
					}
				}
				if (innerCircle) {
					FieldManager.symbolPositionList.Add (dropRune.Symbol, CardinalDirectionEnum);
				}
				dropRune.transform.SetParent (transform, false);
			}
		}

		public bool ValidateRune ()
		{
			Rune child = GetChild ();
			if (!child) {
				Debug.Log ("No child");
				return false;
			} else {
				Debug.Log (child.Symbol.CardinalDirection.CardinalDirectionEnum + " " + CardinalDirectionEnum);

				if (child.Symbol.CardinalDirection.CardinalDirectionEnum.Equals (CardinalDirectionEnum)) {
					child.GetComponent<Image> ().color = Color.green;
				} else {
					child.GetComponent<Image> ().color = Color.red;
				}
				return child.Symbol.CardinalDirection.CardinalDirectionEnum.Equals (CardinalDirectionEnum);
			}

			//return child && child.Symbol.CardinalDirection.CardinalDirectionEnum.Equals (CardinalDirectionEnum);
		}
	}
}
