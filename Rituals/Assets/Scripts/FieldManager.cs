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
		public RuneSlot[] RuneSlots = new RuneSlot[8];
		public GameObject LineRendererPrefab;
		public Camera camera;

		public static bool CardinalDirectionForConnectionIsFree (CardinalDirectionEnum Enum)
		{
			return !lineConnectionList.ContainsKey (Enum) &&
			!lineConnectionList.ContainsValue (Enum);
		}

		public void DeleteConnection (CardinalDirectionEnum Enum)
		{
			CardinalDirectionEnum target = CardinalDirectionEnum.NORTH;
			if (lineConnectionList.ContainsKey (Enum)) {
				target = lineConnectionList [Enum];
				lineConnectionList.Remove (Enum);
			} else {
				
				foreach (CardinalDirectionEnum key in lineConnectionList.Keys) {
					if (lineConnectionList [key] == Enum) {
						target = key;
						break;
					}
				}
				lineConnectionList.Remove (target);
			}
			foreach (LineSlot slot in LineSlots) {
				if (slot.CardinalDirectionEnum.Equals (Enum)) {
					Destroy (slot.lineRenderer.gameObject);
					slot.lineRenderer = null;
				} else if (slot.CardinalDirectionEnum.Equals (target)) {
					slot.lineRenderer = null;
				}
			}

		}

		void Start ()
		{
			foreach (LineSlot lineSlot in LineSlots) {
				lineSlot.fieldManager = this;
			}
		}

		public void Check ()
		{
			if (Validate ()) {
				Debug.Log ("win");
				GameManager.WonGame = true;
			} else {
				Debug.Log ("loose");
				GameManager.WonGame = false;
			}
			//TODO Show Won or Loose Sceene
		}

		public bool Validate ()
		{
			foreach (RuneSlot slot in RuneSlots) {
				if (!slot.ValidateRune ()) {
					return false;
				}
			}
			foreach (Connection connection in GameManager.ConnectionList) {
				CardinalDirectionEnum a = connection.SymbolA.CardinalDirection.CardinalDirectionEnum;
				CardinalDirectionEnum b = connection.SymbolB.CardinalDirection.CardinalDirectionEnum;
				Debug.Log (a + " <-> " + b);
				Debug.Log ("contains a " + lineConnectionList.ContainsKey (a)); 
				if (lineConnectionList.ContainsKey (a)) {
					Debug.Log (lineConnectionList [a].Equals (b));
				}
				Debug.Log ("contains b " + lineConnectionList.ContainsKey (b));
				if (!((lineConnectionList.ContainsKey (a) && lineConnectionList [a].Equals (b))
				    || (lineConnectionList.ContainsKey (b) && lineConnectionList [b].Equals (a)))) {
					return false;
				} else {
					foreach (LineSlot slot in LineSlots) {
						if (slot.CardinalDirectionEnum.Equals (a)) {
							slot.lineRenderer.SetColors (Color.green, Color.green);
						}
					}
				}
			}
			return true;
		}
	}
}
