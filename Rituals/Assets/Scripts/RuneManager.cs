using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MainGame
{

	public class RuneManager : MonoBehaviour
	{

		public Transform[] RunePositions = new Transform[8];
		public GameObject RunePrefab;
		private List<Rune> runes = new List<Rune> ();

		void Start ()
		{
	
		}

		void Update ()
		{
	
		}

		public void GenerateOrUpdateRunes (Symbol[] symbols)
		{
			while (runes.Count > 0) {
				Destroy (runes [0].gameObject);
				runes.RemoveAt (0);
			}
			for (int i = 0; i < symbols.Length; i++) {
				Rune rune = Instantiate (RunePrefab).gameObject.GetComponent <Rune> ();
				rune.gameObject.transform.SetParent (this.gameObject.transform, false);
				rune.StartPosition = RunePositions [i];
				rune.ResetPosition ();
				rune.SymbolImage.sprite = symbols [i].Sprite;
				runes.Add (rune);
			}
		}
	}
}
