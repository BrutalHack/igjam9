using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace MainGame
{

	public class RuneSlot : MonoBehaviour, IDropHandler
	{

		public CardinalDirectionEnum CardinalDirectionEnum;

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
				if (childRune != null) {
					childRune.transform.SetParent (dropRune.transform.parent, false);
				}
				dropRune.transform.SetParent (transform, false);
			}
		}
	}
}
