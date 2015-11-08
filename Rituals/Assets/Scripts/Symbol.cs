using UnityEngine;
using System.Collections;

namespace MainGame
{
	[System.Serializable]
	public class Symbol
	{
		public CardinalDirection CardinalDirection;
		public Sprite Sprite;

		public Symbol (CardinalDirection CardinalDirection, Sprite Sprite)
		{
			this.CardinalDirection = CardinalDirection;
			this.Sprite = Sprite;
		}
	}
}
