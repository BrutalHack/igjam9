using UnityEngine;
using MainGame;

namespace MiniGames
{
	public class Reset : MonoBehaviour
	{
		public void ResetGame ()
		{
			GameManager.LoseLevel ();
		}

	}
}