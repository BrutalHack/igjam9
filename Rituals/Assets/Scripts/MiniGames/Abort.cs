using UnityEngine;
using MainGame;

namespace MiniGames
{
	public class Abort : MonoBehaviour
	{

		public void AbortGame ()
		{
			GameManager.Abort ();
		}

		void Update ()
		{
			if (Input.GetKeyDown (KeyCode.Escape)) {
				GameManager.Abort ();
			}
		}
	}
}