using UnityEngine;
using System.Collections;

namespace MiniGames.Faces
{
	public class Face : MonoBehaviour
	{
		public Sprite EyeSprite;
		public Sprite[] HairSprites;
		public Sprite[] EyebrowSprites;
		public Sprite[] NoseSprites;
		public Sprite[] MouthSprites;
		public Color SkinColor;
		public SpriteRenderer head;
		public SpriteRenderer hair;
		public SpriteRenderer eyebrow;
		public SpriteRenderer nose;
		public SpriteRenderer mouth;

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
			head.color = SkinColor;
		}
	}
}
