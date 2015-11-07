using UnityEngine;
using System.Collections;

namespace MiniGames.Faces
{
	public class Face : MonoBehaviour
	{
		public int ActiveSkinColor = 0;
		public int ActiveHairColor = 0;
		public int ActiveHair = 0;
		public int ActiveEyebrows = 0;
		public int ActiveNose = 0;
		public int ActiveMouth = 0;

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
			this.SetSkinColor (ActiveSkinColor);
			this.SetHairColor (ActiveHairColor);
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnValidate ()
		{
			this.SetSkinColor (ActiveSkinColor);
			this.SetHairColor (ActiveHairColor);
			SetHair (ActiveHair);
			SetEyebrows (ActiveEyebrows);
			SetNose (ActiveNose);
			SetMouth (ActiveMouth);
		}

		public void SetConfiguration (int newSkinColor, int newHairColor, int newHair, 
		                              int newEyebrows, int newNose, int newMouth)
		{
			SetSkinColor (newSkinColor);
			SetHairColor (newHairColor);
			SetHair (newHair);
			SetEyebrows (newEyebrows);
			SetNose (newNose);
			SetMouth (newMouth);
		}

		public void Randomize ()
		{
			SetSkinColor (Random.Range (0, SkinColors.Length));
			SetHairColor (Random.Range (0, HairColors.Length));
			for (int i = 0; i < 4; i++) {
				RandomizeFacePart (i);
			}

		}

		public void RandomizeFacePart (int facePartId)
		{
			int randomValue = -1;
			do {
				randomValue = Random.Range (0, GetFacePartCount (facePartId));
			} while(randomValue == GetFacePartValue (facePartId));
			SetFacePart (facePartId, randomValue);
		}

		private int GetFacePartCount (int facePartId)
		{
			switch (facePartId) {
			case 0:
				return HairSprites.Length;
			case 1:
				return EyebrowSprites.Length;
			case 2:
				return NoseSprites.Length;
			case 3:
				return MouthSprites.Length;
			default:
				return -1;
			}
		}

		private int GetFacePartValue (int facePartId)
		{
			switch (facePartId) {
			case 0:
				return ActiveHair;
			case 1:
				return ActiveEyebrows;
			case 2:
				return ActiveNose;
			case 3:
				return ActiveMouth;
			default:
				return -1;
			}
		}

		public void SetFacePart (int facePartId, int newValue)
		{
			switch (facePartId) {
			case 0:
				SetHair (newValue);
				break;
			case 1:
				SetEyebrows (newValue);
				break;
			case 2:
				SetNose (newValue);
				break;
			case 3:
				SetMouth (newValue);
				break;
			}
		}

		public void SetSkinColor (int newSkinColor)
		{
			head.color = SkinColors [newSkinColor];
			nose.color = SkinColors [newSkinColor] - NoseColorDiff;
			ActiveSkinColor = newSkinColor;
		}

		public void SetHairColor (int newHairColor)
		{
			hair.color = HairColors [newHairColor];
			eyebrow.color = HairColors [newHairColor];
			ActiveHairColor = newHairColor;
		}

		public void SetHair (int newHair)
		{
			hair.sprite = HairSprites [newHair];
			ActiveHair = newHair;
		}

		public void SetEyebrows (int newEyebrows)
		{
			eyebrow.sprite = EyebrowSprites [newEyebrows];
			ActiveEyebrows = newEyebrows;
		}

		public void SetNose (int newNose)
		{
			nose.sprite = NoseSprites [newNose];
			ActiveNose = newNose;
		}

		public void SetMouth (int newMouth)
		{
			mouth.sprite = MouthSprites [newMouth];
			ActiveMouth = newMouth;
		}
	}
}