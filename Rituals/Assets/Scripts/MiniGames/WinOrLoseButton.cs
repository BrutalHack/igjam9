using UnityEngine;
using System.Collections;
using MainGame;

namespace MiniGames
{
	[RequireComponent (typeof(BoxCollider2D))]
	public class WinOrLoseButton : MonoBehaviour
	{
		public bool correctButton;

		void OnMouseDown ()
		{
			if (correctButton) {
				GameManager.WinLevel ();
			} else {
				GameManager.LoseLevel ();
			}
		}
	}
}