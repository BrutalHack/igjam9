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
		private Color NoseColorDiff = new Color (30, 30, 30);

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
			nose.color = SkinColors [ActiveHairColor] - NoseColorDiff;
		}
	}
}
