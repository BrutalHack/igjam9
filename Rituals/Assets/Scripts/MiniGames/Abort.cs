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
	}
}