using UnityEngine;
using System;
using MainGame;

namespace MiniGames.Lights
{
	public class LightsLayout : MonoBehaviour
	{
		public int minSize = 3;
		public int maxSize = 5;
		public int lightBulbsToToggle = 2;
		GameObject[,] lights;
		public Sprite lightSprite;
		const float multiplier = 1.28f;
		public bool won;

		void Awake ()
		{
			int size = UnityEngine.Random.Range (minSize, maxSize + 1);
			lights = new GameObject[size, size];
			for (int i = 0; i < lights.GetLength (0); i++) {
				for (int j = 0; j < lights.GetLength (1); j++) {
					GameObject light = new GameObject ("light " + i + " " + j);
					lights [i, j] = light;
					Light l = light.AddComponent<Light> ();
					l.On = true;
					LightButton lButton = light.AddComponent<LightButton> ();
					lButton.layout = this;
					lButton.position = new Position2d (i, j);
					BoxCollider2D clickCollider = light.AddComponent<BoxCollider2D> ();
					clickCollider.size = new Vector2 (multiplier, multiplier);
					SpriteRenderer renderer = light.AddComponent<SpriteRenderer> ();
					renderer.color = l.On ? Color.yellow : Color.grey;
					renderer.sprite = lightSprite;
					light.transform.parent = this.transform;
					light.transform.position = IntToFloat (i, j);
				}
			}
			for (int i = 0; i < lightBulbsToToggle; i++) {
				int x = UnityEngine.Random.Range (0, lights.GetLength (0));
				int y = UnityEngine.Random.Range (0, lights.GetLength (1));
				click (new Position2d (x, y));
			}
		}


		public void click (Position2d position)
		{
			PlayClickSound ();
			switchLight (position);
			Position2d up = new Position2d (position.x - 1, position.y);
			switchIfPresent (up);
			Position2d down = new Position2d (position.x + 1, position.y);
			switchIfPresent (down);
			Position2d left = new Position2d (position.x, position.y - 1);
			switchIfPresent (left);
			Position2d right = new Position2d (position.x, position.y + 1);
			switchIfPresent (right);
			bool on = true;
			foreach (GameObject light in lights) {
				if (!IsOn (light)) {
					on = false;
					break;
				}
			}
			if (on) {
				won = true;
				OnWin ();
			}

		}

		void PlayClickSound ()
		{
			//TODO sound
		}

		void OnWin ()
		{
			GameManager.WinLevel ();
		}

		private static bool IsOn (GameObject light)
		{
			return light.GetComponent<Light> ().On;
		}

		void switchIfPresent (Position2d up)
		{
			if (boundsCheck (up)) {
				switchLight (up);
			}
		}

		bool boundsCheck (Position2d pos)
		{
			return pos.x >= 0 && pos.y >= 0 && pos.x < lights.GetLength (0) && pos.y < lights.GetLength (1);
		}

		void switchLight (Position2d position)
		{
			if (won) {
				return;
			}
			Light l = lights [position.x, position.y].GetComponent<Light> ();
			l.On = !l.On;
			lights [position.x, position.y].GetComponent<SpriteRenderer> ().color = l.On ? Color.yellow : Color.gray;
		}

		static Vector3 IntToFloat (Position2d pos)
		{
			return IntToFloat (pos.x, pos.y);
		}

		static Vector3 IntToFloat (int x, int y)
		{
			return new Vector3 (y * multiplier, -x * multiplier);
		}
	}
}