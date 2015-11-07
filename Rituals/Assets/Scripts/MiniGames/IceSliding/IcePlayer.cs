using UnityEngine;

namespace MiniGames.IceSliding
{
	public class IcePlayer : MonoBehaviour
	{
		public Layout layout;
		public GameObject Buttons;
		public Vector3 targetPosition;
		public float speed;
		public bool won;
		public bool moving;

		void Start ()
		{
			this.targetPosition = layout.getStartPosition ();
			this.transform.position = targetPosition;

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
					Buttons.SetActive (false);
				}
		
				this.transform.position = Vector3.MoveTowards (transform.position, targetPosition, speed * Time.deltaTime);
			} else if (layout.ReachedFinish () && !won) {
				won = true;
				OnWin ();
			} else {
				if (moving) {
					moving = false;
					Buttons.SetActive (true);
				}
				DirectionEnum dir = DirectionEnum.NORTH;
				bool anyInput = false;
				if (Input.GetKeyDown (KeyCode.W)) {
					dir = DirectionEnum.NORTH;
					anyInput = true;
				}
				if (Input.GetKeyDown (KeyCode.A)) {
					dir = DirectionEnum.WEST;
					anyInput = true;
				}
				if (Input.GetKeyDown (KeyCode.S)) {
					dir = DirectionEnum.SOUTH;
					anyInput = true;
				}
				if (Input.GetKeyDown (KeyCode.D)) {
					dir = DirectionEnum.EAST;
					anyInput = true;
				}
				if (anyInput) {
					targetPosition = layout.Move (dir);
				}
			}
		}

		void OnWin ()
		{
		
		}

		public void Move (DirectionEnum direction)
		{
			targetPosition = layout.Move (direction);
		}
	}
}
