using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GT = GroundTypeEnum;
using System;

public class Layout : MonoBehaviour
{
	public Sprite RockSprite;
	public Sprite StartSprite;
	public Sprite FinishSprite;
	public Sprite IceSprite;
	public Sprite DirtSprite;
	Position2d playerPosition;
	GT[,] Landscape;
	List<GT[,]> boardList = new List <GT[,]> ();

	void Awake ()
	{
		boardList.Add (new GT[,] {
			{ GT.SRT, GT.ICE, GT.ICE, GT.ICE, GT.WAL, GT.ICE },
			{ GT.WAL, GT.ICE, GT.ICE, GT.ICE, GT.ICE, GT.ICE },
			{ GT.ICE, GT.ICE, GT.WAL, GT.ICE, GT.ICE, GT.ICE },
			{ GT.ICE, GT.ICE, GT.ICE, GT.ICE, GT.ICE, GT.FIN }
		});
		boardList.Add (new GT[,] {
			{ GT.ICE, GT.ICE, GT.ICE, GT.ICE, GT.WAL, GT.ICE },
			{ GT.WAL, GT.ICE, GT.ICE, GT.ICE, GT.ICE, GT.ICE },
			{ GT.ICE, GT.ICE, GT.WAL, GT.ICE, GT.ICE, GT.FIN },
			{ GT.SRT, GT.ICE, GT.ICE, GT.ICE, GT.ICE, GT.ICE }
		});
		Landscape = boardList [UnityEngine.Random.Range (0, boardList.Count)];
	}

	void Start ()
	{
		
		for (int i = 0; i < Landscape.GetLength (0); i++) {
			for (int j = 0; j < Landscape.GetLength (1); j++) {
				GameObject child = new GameObject ("Child " + i + " " + j);
				SpriteRenderer render = child.AddComponent<SpriteRenderer> ();
				render.color = Color.green;
				switch (Landscape [i, j]) {
				case GT.ICE:
					render.sprite = IceSprite;
					break;
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
					render.sprite = FinishSprite;
					break;
				default:
					throw new ArgumentOutOfRangeException ();
				}
				child.transform.parent = this.transform;
				child.transform.position = intToFloat (i, j);
			}
		}
	}

	public Vector3 getStartPosition ()
	{
		for (int i = 0; i < Landscape.GetLength (0); i++) {
			for (int j = 0; j < Landscape.GetLength (1); j++) {
				if (Landscape [i, j].Equals (GT.SRT)) {
					Position2d pos = new Position2d (i, j);
					playerPosition = pos;
					return intToFloat (pos);
				}
			}
		}
		throw new ArgumentException ("no Start defined");
	}

	public bool ReachedFinish ()
	{
		return playerPosition != null && Landscape [playerPosition.x, playerPosition.y].Equals (GT.FIN);
	}

	Vector3 intToFloat (Position2d pos)
	{
		return intToFloat (pos.x, pos.y);
	}

	Vector3 intToFloat (int x, int y)
	{
		return new Vector3 (y, -x);
	}

	public  Vector3 Move (DirectionEnum direction)
	{
		Position2d newPos = new Position2d (playerPosition.x, playerPosition.y);
		Position2d positionChange = new Position2d (0, 0);
		switch (direction) {
		case DirectionEnum.NORTH:
			positionChange.x = -1;
			break;
		case DirectionEnum.EAST:
			positionChange.y = 1;
			break;
		case DirectionEnum.SOUTH:
			positionChange.x = 1;
			break;
		case DirectionEnum.WEST:
			positionChange.y = -1;
			break;
		default:
			throw new System.ArgumentOutOfRangeException ();
		}
		do {
			newPos.AddLocal (positionChange);
		} while (posInBounds (newPos)
		         && Landscape [newPos.x, newPos.y].Equals (GT.ICE));
		if (!posInBounds (newPos) || Landscape [newPos.x, newPos.y].Equals (GT.WAL)) {
			newPos.AddLocal (positionChange.negateLocal ());
		}
		playerPosition = newPos;
		return intToFloat (newPos);
	}

	bool posInBounds (Position2d newPos)
	{
		return newPos.x >= 0 && newPos.y >= 0 && newPos.x < Landscape.GetLength (0) && newPos.y < Landscape.GetLength (1);
	}
}
