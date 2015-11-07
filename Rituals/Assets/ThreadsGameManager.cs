using UnityEngine;
using System.Collections;
using System.Threading;

namespace MiniGames.Threads
{
	public class ThreadsGameManager : MonoBehaviour
	{
		public ThreadPuzzle[] ThreadPuzzles;
		public WinOrLoseButton[] Buttons;
		public int ActivePuzzleId;

		// Use this for initialization
		void Start ()
		{
			ActivePuzzleId = Random.Range (0, ThreadPuzzles.Length);
			GetComponent<SpriteRenderer> ().sprite = ThreadPuzzles [ActivePuzzleId].PuzzleSprite;
			Buttons [ActivePuzzleId].correctButton = true;
		}
	}
}