using UnityEngine;
using MainGame;

namespace MiniGames.MouseCheese
{
	public class MouseAi : MonoBehaviour
	{
		public MouseLayout layout;
		public Vector3 targetPosition;
		public bool won;
		public bool lost;
		public bool moving;
		public float speed;

		void Start ()
		{
			this.transform.position = layout.getStartPosition ();
			this.targetPosition = this.transform.position;
		}

		void Update ()
		{
			if (won || lost) {
				return;
			}
		
			Vector3 position = this.transform.position;
			if (Vector3.Distance (targetPosition, position) > 0.001) {
				if (!moving) {
					moving = true;
				}
				this.transform.position = Vector3.MoveTowards (transform.position, targetPosition, speed * Time.deltaTime);
			} else if (layout.ReachedFinish () && !won) {
				won = true;
				layout.RemoveCheese ();
				OnWin ();
			} else {
				if (moving) {
					moving = false;
					if (layout.RemainingCheese == 0) {
						layout.RemoveCheese ();
						lost = true;
						Debug.Log ("lost");
					} else {
						layout.ActivateButtons (true);
					}
				}
			}
		}

		void OnWin ()
		{
			GameManager.WinLevel ();
		}

		public void showUnreachable ()
		{
			Debug.Log ("Unreachalbe tile");
		}
	}
}