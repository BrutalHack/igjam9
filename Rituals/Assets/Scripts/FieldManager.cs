using UnityEngine;
using UnityEngine.UI;
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
		public Image FieldImage;

		public void EnableRuneSlotHalo ()
		{
			foreach (RuneSlot runeSlot in RuneSlots) {
				Image haloImage = runeSlot.GetComponent <Image> ();
				Color oldColor = haloImage.color;
				haloImage.color = new Color (oldColor.r, oldColor.g, oldColor.b, 1f);
			}
		}

		public void DisableRuneSlotHalo ()
		{
			foreach (RuneSlot runeSlot in RuneSlots) {
				Image haloImage = runeSlot.GetComponent <Image> ();
				Color oldColor = haloImage.color;
				haloImage.color = new Color (oldColor.r, oldColor.g, oldColor.b, 0f);
			}
		}

		public void EnableLineSlotHalo ()
		{
			foreach (LineSlot lineSlot in LineSlots) {
				Image haloImage = lineSlot.GetComponent <Image> ();
				Color oldColor = haloImage.color;
				haloImage.color = new Color (oldColor.r, oldColor.g, oldColor.b, 1f);
			}
		}

		public void DisableLineSlotHalo ()
		{
			foreach (LineSlot lineSlot in LineSlots) {
				Image haloImage = lineSlot.GetComponent <Image> ();
				Color oldColor = haloImage.color;
				haloImage.color = new Color (oldColor.r, oldColor.g, oldColor.b, 0f);
			}
		}

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
			foreach (RuneSlot runeSlot in RuneSlots) {
				runeSlot.fieldManager = this;
			}
			Canvas.ForceUpdateCanvases ();
			DrawExistingConnections ();
		}


		void DrawExistingConnections ()
		{
			foreach (LineSlot slot in LineSlots) {
				if (lineConnectionList.ContainsKey (slot.CardinalDirectionEnum)) {
					CardinalDirectionEnum targetDirection = lineConnectionList [slot.CardinalDirectionEnum];
					slot.CreateLineRenderer ();
					foreach (LineSlot target in LineSlots) {
						if (target.CardinalDirectionEnum.Equals (targetDirection)) {
							target.SetLineRendererTarget (slot);
						}
					}
				}
			}
		}

		public void Check ()
		{
			if (Validate ()) {
				GameManager.StaticListCleanup ();
				FieldImage.color = Color.green;
				Invoke ("GoToVictoryScreen", 2f);
			} else {
				GameManager.StaticListCleanup ();
				FieldImage.color = Color.red;
				Invoke ("GoToLossScreen", 2f);
			}
		}

		private void GoToVictoryScreen ()
		{
			Application.LoadLevel (8);
		}

		private void GoToLossScreen ()
		{
			Application.LoadLevel (9);
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
