using UnityEngine;
using System.Collections.Generic;
using GT = MiniGames.GroundTypeEnum;
using System;
using UnityEngine.UI;


namespace MiniGames.MouseCheese
{
	public class MouseLayout : MonoBehaviour
	{
		public MouseAi Mouse;
		public GameObject ChildContainer;
		public GameObject currentCheeseButton;
		public Text cheeseTextField;
		public Sprite RockSprite;
		public Sprite StartSprite;
		public Sprite FinishSprite;
		public Sprite DirtSprite;
		public Sprite CheeseSprite;
		public int MaxCheese;
		public int RemainingCheese;
		Position2d mousePosition;
		GT[,] Landscape;
		List<GT[,]> boardList = new List <GT[,]> ();

		const float multiplier = 1.28f;

		void Awake ()
		{
			RemainingCheese = MaxCheese;
			boardList.Add (new GT[,] {
				{ GT.SRT, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.WAL, GT.DIR, GT.WAL, GT.DIR, GT.WAL, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.WAL, GT.DIR, GT.WAL, GT.DIR, GT.WAL, GT.DIR, GT.DIR, GT.WAL, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.DIR, GT.DIR, GT.WAL, GT.DIR, GT.WAL, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.DIR, GT.WAL, GT.WAL, GT.DIR, GT.WAL, GT.WAL, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.DIR, GT.DIR, GT.WAL, GT.DIR, GT.WAL, GT.WAL, GT.DIR, GT.WAL, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.WAL, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.DIR, GT.DIR, GT.WAL, GT.DIR, GT.FIN, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.WAL, GT.WAL, GT.DIR, GT.WAL, GT.WAL, GT.DIR, GT.DIR, GT.WAL, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.DIR, GT.WAL, GT.DIR, GT.WAL, GT.DIR, GT.WAL, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
			});
			boardList.Add (new GT[,] {
				{ GT.SRT, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.WAL, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.DIR, GT.DIR, GT.WAL, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.FIN, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.DIR, GT.WAL, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
				{ GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.DIR, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL, GT.WAL },
			});
			Landscape = boardList [UnityEngine.Random.Range (0, boardList.Count)];
		}

		void Start ()
		{
			for (int i = 0; i < Landscape.GetLength (0); i++) {
				for (int j = 0; j < Landscape.GetLength (1); j++) {
					GameObject child = new GameObject ("Child " + i + " " + j);
					SpriteRenderer render = child.AddComponent<SpriteRenderer> ();
					switch (Landscape [i, j]) {
					case GT.DIR:
						render.sprite = DirtSprite;
						break;
					case GT.WAL:
						render.sprite = RockSprite;
						break;
					case GT.SRT:
						render.sprite = StartSprite;
						break;
					case GT.FIN:
						render.sprite = DirtSprite;
						GameObject flag = new GameObject ("Flag " + i + " " + j);
						SpriteRenderer finishSpriteRenderer = flag.AddComponent<SpriteRenderer> ();
						finishSpriteRenderer.sortingOrder = 1;
						finishSpriteRenderer.sprite = FinishSprite;
						flag.transform.parent = child.transform;
						break;
					default:
						throw new ArgumentOutOfRangeException ();
					}
					if (!Landscape [i, j].Equals (GT.WAL)) {
						BoxCollider2D clicker = child.AddComponent<BoxCollider2D> ();
						clicker.size = new Vector2 (multiplier, multiplier);
						CheeseButton cheeseButton = child.AddComponent<CheeseButton> ();
						cheeseButton.mouseLayout = this;
						cheeseButton.position = new Position2d (i, j);
					}
					child.transform.parent = this.ChildContainer.transform;
					child.transform.position = IntToFloat (i, j);
				}
			}
		}

		public Vector3 getStartPosition ()
		{
			for (int i = 0; i < Landscape.GetLength (0); i++) {
				for (int j = 0; j < Landscape.GetLength (1); j++) {
					if (Landscape [i, j].Equals (GT.SRT)) {
						Position2d pos = new Position2d (i, j);
						mousePosition = pos;
						return IntToFloat (pos);
					}
				}
			}
			throw new ArgumentException ("no Start defined");
		}

		public bool ReachedFinish ()
		{
			return mousePosition != null && Landscape [mousePosition.x, mousePosition.y].Equals (GT.FIN);
		}

		static Vector3 IntToFloat (Position2d pos)
		{
			return IntToFloat (pos.x, pos.y);
		}

		static Vector3 IntToFloat (int x, int y)
		{
			return new Vector3 (y * multiplier, -x * multiplier);
		}

		public void ActivateButtons (bool active)
		{
			foreach (Transform child in ChildContainer.transform) {
				BoxCollider2D clickCollider = child.gameObject.GetComponent<BoxCollider2D> ();
				if (clickCollider) {
					clickCollider.enabled = active;
				}
			}
			if (active) {
				RemoveCheese ();
			}
		}

		public void RemoveCheese ()
		{
			Destroy (currentCheeseButton);
		}

		public void PutCheese (Position2d position, GameObject button)
		{
			if (position.x == mousePosition.x && position.y == mousePosition.y) {
				Mouse.showUnreachable ();	
			} else if (position.x != mousePosition.x && position.y != mousePosition.y) {
				Mouse.showUnreachable ();
			} else {
				Position2d newPos = new Position2d (mousePosition.x, mousePosition.y);
				Position2d positionChange = new Position2d (0, 0);
				if (mousePosition.x < position.x) {
					positionChange.x = 1;
				} else if (mousePosition.x > position.x) {
					positionChange.x = -1;
				} else if (mousePosition.y < position.y) {
					positionChange.y = 1;
				} else {
					positionChange.y = -1;
				}
				do {
					newPos.AddLocal (positionChange);
				} while(!Landscape [newPos.x, newPos.y].Equals (GT.WAL) && !(newPos.x == position.x && newPos.y == position.y));
				if ((newPos.x == position.x && newPos.y == position.y)) {
					Mouse.targetPosition = IntToFloat (newPos);
					mousePosition = newPos;
					this.ActivateButtons (false);
					PutCheese (button);
					RemainingCheese--;
					cheeseTextField.text = "" + RemainingCheese;
				} else {
					Mouse.showUnreachable ();
				}
			}
		}

		void PutCheese (GameObject button)
		{
			GameObject cheese = new GameObject ("Cheese");
			SpriteRenderer cheeseSpriteRenderer = cheese.AddComponent<SpriteRenderer> ();
			cheeseSpriteRenderer.sortingOrder = 1;
			cheeseSpriteRenderer.sprite = CheeseSprite;
			cheeseSpriteRenderer.color = Color.yellow;
			cheese.transform.SetParent (button.transform, false);
			currentCheeseButton = cheese;
		}
	}
}