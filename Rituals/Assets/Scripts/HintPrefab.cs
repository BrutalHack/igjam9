using UnityEngine;
using UnityEngine.UI;

namespace MainGame
{

	public class HintPrefab: MonoBehaviour
	{

		public Image FirstObjectImage;
		public Image RelationshipImage;
		public Image SecondObjectImage;

		public Hint hint;

		public GameObject Button;

		void Start ()
		{
			UpdateImages ();
		}

		public void SetHint (Hint Hint)
		{
			this.hint = Hint;
		}

		private void UpdateImages ()
		{
			FirstObjectImage.sprite = hint.SymbolASprite;
			RelationshipImage.sprite = hint.RelationshipSprite;
			SecondObjectImage.sprite = hint.SymbolBSprite;
		}

		public void StartMinigame ()
		{
			GameManager.SelectedHint = hint;
			Debug.Log ("Start Minigame " + GameManager.SelectedHint.MinigameScene);
			Application.LoadLevel (hint.MinigameScene);
		}
	}
}
