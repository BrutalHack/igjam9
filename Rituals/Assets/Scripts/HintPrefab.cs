using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MainGame
{

	public class HintPrefab: MonoBehaviour
	{

		public Image FirstObjectImage;
		public Image RelationshipImage;
		public Image SecondObjectImage;

		public Sprite SymbolASprite;
		public Sprite RelationshipSprite;
		public Sprite SymboleBSprite;

		void Start ()
		{
			UpdateImages ();
		}

		private void UpdateImages ()
		{
			FirstObjectImage.sprite = SymbolASprite;
			RelationshipImage.sprite = RelationshipSprite;
			SecondObjectImage.sprite = SymboleBSprite;
		}
	}
}
