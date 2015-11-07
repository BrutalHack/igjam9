using UnityEngine;
using System.Collections;

namespace MiniGames
{
	[RequireComponent (typeof(BoxCollider2D))]
	public class WinOrLoseButton : MonoBehaviour
	{
		public bool correctButton;

		void OnMouseDown ()
		{
			if (correctButton) {
				Debug.Log ("Correct Button! You won!");
			} else {
				Debug.Log ("Wrong Button! You lost!");
			}
		}
	}
}