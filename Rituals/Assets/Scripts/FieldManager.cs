using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MainGame
{

	public class FieldManager : MonoBehaviour
	{
		public static Dictionary<CardinalDirectionEnum, CardinalDirectionEnum> lineConnectionList = new Dictionary<CardinalDirectionEnum, CardinalDirectionEnum> ();
		public static Dictionary<Symbol, CardinalDirectionEnum> symbolPositionList = new Dictionary<Symbol, CardinalDirectionEnum> ();

		public LineSlot[] LineSlots = new LineSlot[8];
		public GameObject LineRendererPrefab;
		public Camera camera;

		public static bool CardinalDirectionForConnectionIsFree (CardinalDirectionEnum Enum)
		{
			return !lineConnectionList.ContainsKey (Enum) &&
			!lineConnectionList.ContainsValue (Enum);
		}

		void Start ()
		{
			foreach (LineSlot lineSlot in LineSlots) {
				lineSlot.fieldManager = this;
			}
		}

		public void CascadeRemoveLineRenderer (LineRenderer lineRenderer, CardinalDirectionEnum cardinalDirectionEnum)
		{
			foreach (LineSlot lineSlot in LineSlots) {
				lineSlot.RemoveLineRenderer (lineRenderer);
			}
			Destroy (lineRenderer);
			if (lineConnectionList.ContainsKey (cardinalDirectionEnum)) {
				lineConnectionList.Remove (cardinalDirectionEnum);
			}
			if (lineConnectionList.ContainsValue (cardinalDirectionEnum)) {
				bool foundKey = false;
				CardinalDirectionEnum searchedKey = CardinalDirectionEnum.NORTH;
				foreach (CardinalDirectionEnum key in lineConnectionList.Keys) {
					if (lineConnectionList [key] == cardinalDirectionEnum) {
						foundKey = true;
						searchedKey = key;
						break;
					}
				}
				if (foundKey) {
					lineConnectionList.Remove (searchedKey);
				}
			}
		}
	}
}
