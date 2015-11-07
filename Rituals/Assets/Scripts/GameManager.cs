using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace MainGame
{
	public class GameManager : MonoBehaviour
	{

		public CardinalDirection[] CardinalDirections = new CardinalDirection [8];
		public Sprite[] SymbolSprites;
		public Sprite[] RelationshipSprites;
		public Transform hintParent;
		public GameObject hintPrefab;
		private HintPrefab[] hints;
		private Symbol[] symbols;
		private Connection[] connections;
		private const int CONNECTION_COUNT = 3;

		void Start ()
		{
			Init ();
		}

		void Update ()
		{
	
		}

		private void Init ()
		{
			SelectSymbols ();
			SelectConnections ();
			GenerateHints ();
		}

		private void SelectSymbols ()
		{
			List<Symbol> symbolList = new List<Symbol> ();
			List<int> selectedSymbols = new List<int> ();

			int selectedSymbolId;
			for (int i = 0; i < CardinalDirections.Length; i++) {
				do {
					selectedSymbolId = Random.Range (0, SymbolSprites.Length);
				} while(selectedSymbols.Contains (selectedSymbolId));
				selectedSymbols.Add (selectedSymbolId);
				symbolList.Add (new Symbol (CardinalDirections [i], SymbolSprites [selectedSymbolId]));
			}
			symbols = symbolList.ToArray ();
		}

		private void SelectConnections ()
		{
			List<Connection> selectedConnections = new List<Connection> ();
			int symboleAId = 0;
			int symboleBId = 0;
			Connection selectedConnection;
			for (int i = 0; i < CONNECTION_COUNT; i++) {
				do {
					do {
						symboleAId = Random.Range (0, symbols.Length);
						symboleBId = Random.Range (0, symbols.Length);
					} while(symboleAId == symboleBId);
					selectedConnection = new Connection (symboleAId, symboleBId);
				} while(selectedConnections.Contains (selectedConnection));
				selectedConnection.SetSymbolsByIds (symbols);
				selectedConnections.Add (selectedConnection);
			}
			connections = selectedConnections.ToArray ();
		}

		private void GenerateHints ()
		{
			List<HintPrefab> hintList = new List<HintPrefab> ();
			GeneratePositionHints (hintList);
			GenerateSymbolDirectionHints (hintList);
			GenerateSymbolSymbolHints (hintList);
			GenerateDirectionDirectionHints (hintList);
			hints = hintList.ToArray ();
		}

		private void GeneratePositionHints (List<HintPrefab> hintList)
		{
			foreach (Symbol symbol in symbols) {
				HintPrefab tempHintPrefab = Instantiate (hintPrefab).gameObject.GetComponent<HintPrefab> ();
				tempHintPrefab.SymbolASprite = symbol.Sprite;
				tempHintPrefab.RelationshipSprite = RelationshipSprites [0];
				tempHintPrefab.SymboleBSprite = symbol.Sprite;
				hintList.Add (tempHintPrefab);
			}
		}

		private void GenerateSymbolDirectionHints (List<HintPrefab> hintList)
		{
			foreach (Connection connection in connections) {
				HintPrefab tempHintPrefabOne = Instantiate (hintPrefab).gameObject.GetComponent<HintPrefab> ();
				tempHintPrefabOne.SymbolASprite = connection.symbolA.Sprite;
				tempHintPrefabOne.RelationshipSprite = RelationshipSprites [1];
				tempHintPrefabOne.SymboleBSprite = connection.symbolB.CardinalDirection.Sprite;
				hintList.Add (tempHintPrefabOne);

				HintPrefab tempHintPrefabTwo = Instantiate (hintPrefab).gameObject.GetComponent<HintPrefab> ();
				tempHintPrefabTwo.SymbolASprite = connection.symbolB.Sprite;
				tempHintPrefabTwo.RelationshipSprite = RelationshipSprites [1];
				tempHintPrefabTwo.SymboleBSprite = connection.symbolA.CardinalDirection.Sprite;
				hintList.Add (tempHintPrefabTwo);
			}
		}

		private void GenerateSymbolSymbolHints (List<HintPrefab> hintList)
		{
			foreach (Connection connection in connections) {
				HintPrefab tempHintPrefab = Instantiate (hintPrefab).gameObject.GetComponent<HintPrefab> ();
				tempHintPrefab.SymbolASprite = connection.symbolA.Sprite;
				tempHintPrefab.RelationshipSprite = RelationshipSprites [2];
				tempHintPrefab.SymboleBSprite = connection.symbolB.Sprite;
				hintList.Add (tempHintPrefab);
			}
		}

		private void GenerateDirectionDirectionHints (List<HintPrefab> hintList)
		{
			foreach (Connection connection in connections) {
				HintPrefab tempHintPrefab = Instantiate (hintPrefab).gameObject.GetComponent<HintPrefab> ();
				tempHintPrefab.SymbolASprite = connection.symbolA.CardinalDirection.Sprite;
				tempHintPrefab.RelationshipSprite = RelationshipSprites [2];
				tempHintPrefab.SymboleBSprite = connection.symbolB.CardinalDirection.Sprite;
				hintList.Add (tempHintPrefab);
			}
		}
	}
}
