using UnityEngine;
using MainGame;

namespace MiniGames.MouseCheese
{
	public class MouseAi : MonoBehaviour
	{
		public MouseLayout layout;
		public SpriteRenderer QuestionMark;
		public Vector3 targetPosition;
		public bool moving;
		public float speed;
		float currentTime = 1;
		public float FadeTime = 2;
		public Color StartColor;
		public Color EndColor;
		public AudioSource moveAudio;
		public AudioSource unreachableAudio;

		void Start ()
		{
			this.transform.position = layout.getStartPosition ();
			this.targetPosition = this.transform.position;
		}

		void Update ()
		{
			currentTime += Time.deltaTime / FadeTime;
			QuestionMark.color = Color.Lerp (StartColor, EndColor, currentTime);

			Vector3 position = this.transform.position;
			if (Vector3.Distance (targetPosition, position) > 0.001) {
				if (!moving) {
					StartMoveSound ();
					moving = true;
				}
				this.transform.position = Vector3.MoveTowards (transform.position, targetPosition, speed * Time.deltaTime);
			} else if (layout.ReachedFinish ()) {
				layout.RemoveCheese ();
				OnWin ();
			} else {
				if (moving) {
					moving = false;
					StopMoveSound ();
					if (layout.RemainingCheese == 0) {
						layout.RemoveCheese ();
						OnLoose ();
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

		void OnLoose ()
		{
			GameManager.LoseLevel ();
		}

		public void showUnreachable ()
		{
			PlayUnreachableSound ();
			QuestionMark.color = StartColor;
			currentTime = 0;
		}

		void StartMoveSound ()
		{
			moveAudio.Play ();
		}

		void StopMoveSound ()
		{
			//TODO stop move sound, falls nötig
		}

		void PlayUnreachableSound ()
		{
			unreachableAudio.Play ();
		}
	}
}