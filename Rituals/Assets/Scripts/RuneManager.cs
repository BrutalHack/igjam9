using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace MainGame
{

	public class RuneManager : MonoBehaviour
	{
		public FieldManager FieldManager;
		public Camera camera;
		public Sprite[] RuneSprites = new Sprite[8];
		public Transform[] RunePositions = new Transform[8];
		public GameObject RunePrefab;
		private List<Rune> runes = new List<Rune> ();

		public void GenerateOrUpdateRunes ()
		{
			while (runes.Count > 0) {
				Destroy (runes [0].gameObject);
				runes.RemoveAt (0);
			}
			for (int i = 0; i < GameManager.SymbolList.Count; i++) {
				Rune rune = Instantiate (RunePrefab).gameObject.GetComponent <Rune> ();
				rune.StartParent = RunePositions [i];
				rune.ResetPosition ();
				rune.gameObject.GetComponent <Image> ().sprite = RuneSprites [Random.Range (0, RuneSprites.Length)];
				rune.SymbolImage.sprite = GameManager.SymbolList [i].Sprite;
				rune.Symbol = GameManager.SymbolList [i];
				if (FieldManager.symbolPositionList.ContainsKey (rune.Symbol)) {
					CardinalDirectionEnum position = FieldManager.symbolPositionList [rune.Symbol];
					foreach (RuneSlot slot in FieldManager.RuneSlots) {
						if (slot.CardinalDirectionEnum.Equals (position)) {
							rune.gameObject.transform.SetParent (slot.gameObject.transform, false);
						}
					}
				}
				runes.Add (rune);
				rune.GetComponent<DragHandler> ().camera = this.camera;
			}
		}

		public void ResetRunePositions ()
		{
			foreach (Rune rune in runes) {
				rune.ResetPosition ();
			}
		}

	}
}
