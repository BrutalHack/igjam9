using UnityEngine;

namespace MiniGames.MouseCheese
{
	public class MouseAi : MonoBehaviour
	{
		public MouseLayout layout;
		public Vector3 targetPosition;
		public bool won;
		public bool moving;
		public float speed;

		void Start ()
		{
			this.transform.position = layout.getStartPosition ();
			this.targetPosition = this.transform.position;
		}

		void Update ()
		{
			if (won) {
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
				OnWin ();
			} else {
				if (moving) {
					moving = false;
					layout.ActivateButtons (true);
				}
			}
		}

		void OnWin ()
		{

		}

		public void showUnreachable ()
		{
			Debug.Log ("Unreachalbe tile");
		}
	}
}