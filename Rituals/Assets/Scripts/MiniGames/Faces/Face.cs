using UnityEngine;
using System.Collections;

namespace MiniGames.Faces
{
	public class Face : MonoBehaviour
	{
		public int ActiveSkinColor;
		public int ActiveHairColor;
		public int ActiveHair;
		public int ActiveEyebrows;
		public int ActiveNose;
		public int ActiveMouth;

		[SerializeField]
		private Sprite EyeSprite;
		[SerializeField]
		private Sprite[] HairSprites;
		[SerializeField]
		private Sprite[] EyebrowSprites;
		[SerializeField]
		private Sprite[] NoseSprites;
		[SerializeField]
		private Sprite[] MouthSprites;
		[SerializeField]
		private Color[] SkinColors;
		[SerializeField]
		private Color[] HairColors;
		[SerializeField]
		private SpriteRenderer head;
		[SerializeField]
		private SpriteRenderer hair;
		[SerializeField]
		private SpriteRenderer eyebrow;
		[SerializeField]
		private SpriteRenderer nose;
		[SerializeField]
		private SpriteRenderer mouth;
		private Color NoseColorDiff = new Color (0.1f, 0.1f, 0.1f, 0.0f);

		// Use this for initialization
		void Start ()
		{
			
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnValidate ()
		{
			head.color = SkinColors [ActiveSkinColor];
			Debug.Log (SkinColors [ActiveSkinColor] + " - " + NoseColorDiff + " = " + (SkinColors [ActiveSkinColor] - NoseColorDiff));
			nose.color = SkinColors [ActiveSkinColor] - NoseColorDiff;
			hair.color = HairColors [ActiveHairColor];
			eyebrow.color = HairColors [ActiveHairColor];
			hair.sprite = HairSprites [ActiveHair];
			eyebrow.sprite = EyebrowSprites [ActiveEyebrows];
			nose.sprite = NoseSprites [ActiveNose];
			mouth.sprite = MouthSprites [ActiveMouth];
		}
	}
}
