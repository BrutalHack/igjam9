using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace MainGame
{
	public class GameManager : MonoBehaviour
	{
		public bool shuffle = false;
		public bool hintsOpenOnStart = false;
		public static List<Symbol> SymbolList = new List<Symbol> ();
		public static List<Connection> ConnectionList = new List<Connection> ();
		public static List<Hint> HintList = new List<Hint> ();

		public static Dictionary<Hint, bool> HintMap = new Dictionary<Hint, bool> ();
		public static Hint SelectedHint;
		public static bool WonGame = false;
		private readonly List<int> minigameTempList = new List<int> ();

		public CardinalDirection[] CardinalDirections = new CardinalDirection [8];
		public Sprite[] SymbolSprites;
		public Sprite[] RelationshipSprites;
		public Transform hintParent;
		public GameObject hintPrefab;
		public RuneManager RuneManager;
		public int[] MinigameScenes;
		private const int CONNECTION_COUNT = 4;
		private const int SHUFFLE_COUNT = 50;
		private readonly List<HintPrefab> hintPrefabs = new List<HintPrefab> ();

		void Start ()
		{
			if (HintMap.Count == 0) {
				GenerateStaticLists ();
			}
			GenerateGUI ();
			OpenHints ();
		}

		#region Static List Generation

		private void GenerateStaticLists ()
		{
			StaticListCleanup ();
			FillSymbolList ();
			FillConnectionList ();
			FillHintList ();
			FillHintMap ();
		}

		public static void StaticListCleanup ()
		{
			WonGame = false;
			SymbolList.Clear ();
			ConnectionList.Clear ();
			HintList.Clear ();
			HintMap.Clear ();
			SelectedHint = null;
			FieldManager.lineConnectionList.Clear ();
			FieldManager.symbolPositionList.Clear ();
		}

		private void FillSymbolList ()
		{
			List<int> symbolIds = new List<int> ();
			for (int i = 0; i < SymbolSprites.Length; i++) {
				symbolIds.Add (i);
			}
			for (int i = 0; i < CardinalDirections.Length; i++) {
				int randomPosition = Random.Range (0, symbolIds.Count);
				int selectedSymbolId = symbolIds [randomPosition];
				symbolIds.RemoveAt (randomPosition);
				SymbolList.Add (new Symbol (CardinalDirections [i], SymbolSprites [selectedSymbolId]));
			}
			if (shuffle) {
				ShuffleSymbolList ();
			}
		}

		private void ShuffleSymbolList ()
		{
			for (int i = 0; i < SHUFFLE_COUNT; i++) {
				Symbol tempSymbol = SymbolList [Random.Range (0, SymbolList.Count)];
				SymbolList.Remove (tempSymbol);
				SymbolList.Add (tempSymbol);
			}
		}

		private void FillConnectionList ()
		{
			List<int> symbolIds = new List<int> ();
			for (int i = 0; i < SymbolList.Count; i++) {
				symbolIds.Add (i);
			}
			while (symbolIds.Count > 0) {
				int randomPositionA = Random.Range (0, symbolIds.Count);
				int symbolAPosition = symbolIds [randomPositionA];
				symbolIds.RemoveAt (randomPositionA);
				int randomPositionB = Random.Range (0, symbolIds.Count);
				int symbolBPosition = symbolIds [randomPositionB];
				symbolIds.RemoveAt (randomPositionB);
				ConnectionList.Add (new Connection (SymbolList [symbolAPosition], SymbolList [symbolBPosition]));
			}
		}

		private void FillHintList ()
		{
			CreatePositionHints ();
			CreateSymbolDirectionHints ();
			CreateSymbolSymbolHints ();
			if (shuffle) {
				ShuffleHintList ();
			}
		}

		private void CreatePositionHints ()
		{
			foreach (Symbol symbol in SymbolList) {
				HintList.Add (new Hint (symbol.Sprite, RelationshipSprites [0],
					symbol.CardinalDirection.Sprite, GetMinigame ()));
			}
		}

		private void CreateSymbolDirectionHints ()
		{
			foreach (Connection connection in ConnectionList) {
				HintList.Add (new Hint (connection.SymbolA.Sprite, RelationshipSprites [1], 
					connection.SymbolB.CardinalDirection.Sprite, GetMinigame ()));

				HintList.Add (new Hint (connection.SymbolB.Sprite, RelationshipSprites [1],
					connection.SymbolA.CardinalDirection.Sprite, GetMinigame ()));
			}
		}

		private void CreateSymbolSymbolHints ()
		{
			foreach (Connection connection in ConnectionList) {
				HintList.Add (new Hint (connection.SymbolA.Sprite, RelationshipSprites [1],
					connection.SymbolB.Sprite, GetMinigame ()));
			}
		}

		private void ShuffleHintList ()
		{
			for (int i = 0; i < SHUFFLE_COUNT; i++) {
				Hint tempHint = HintList [Random.Range (0, HintList.Count)];
				HintList.Remove (tempHint);
				HintList.Add (tempHint);
			}
		}

		private void FillHintMap ()
		{
			foreach (Hint hint in HintList) {
				HintMap.Add (hint, hintsOpenOnStart);
			}
		}

		private int GetMinigame ()
		{
			if (minigameTempList.Count == 0) {
				minigameTempList.AddRange (MinigameScenes);
			}
			int randomPosition = Random.Range (0, minigameTempList.Count);
			int selectedMinigame = minigameTempList [randomPosition];
			minigameTempList.RemoveAt (randomPosition);
			return selectedMinigame;
		}

		#endregion

		#region GUI Generation

		private void GenerateGUI ()
		{
			GenerateHintPrefabs ();
			GenerateRunePrefabs ();
		}

		private void GenerateHintPrefabs ()
		{
			hintPrefabs.Clear ();
			foreach (Hint hint in HintList) {
				HintPrefab tempHintPrefab = Instantiate (hintPrefab).gameObject.GetComponent<HintPrefab> ();
				tempHintPrefab.gameObject.transform.SetParent (hintParent, false);
				tempHintPrefab.SetHint (hint);
				hintPrefabs.Add (tempHintPrefab);
			}
		}

		private void GenerateRunePrefabs ()
		{
			RuneManager.GenerateOrUpdateRunes ();
		}

		#endregion

		private void OpenHints ()
		{
			foreach (HintPrefab hintPrefab in hintPrefabs) {
				bool isOpen = false;
				if (HintMap.TryGetValue (hintPrefab.hint, out isOpen)) {
					if (isOpen) {
						hintPrefab.Button.gameObject.SetActive (false);
					}
				}
			}
		}

		public static void WinLevel ()
		{
			HintMap [SelectedHint] = true;
			Application.LoadLevel (1);
		}

		public static void LoseLevel ()
		{
			Application.LoadLevel (Application.loadedLevel);
		}

		public static void Abort ()
		{
			Application.LoadLevel (1);
		}

		public void OpenAbout ()
		{
			Application.LoadLevel (7);
		}
	}
}
