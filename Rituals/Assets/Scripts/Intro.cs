using UnityEngine;
using System.Collections;
using MainGame;

public class Intro : MonoBehaviour
{
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.anyKeyDown) {
			GameManager.Abort ();
		}
	}
}
