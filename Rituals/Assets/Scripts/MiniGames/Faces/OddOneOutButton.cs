using UnityEngine;
using System.Collections;

public class OddOneOutButton : MonoBehaviour
{
	public bool correctFace;

	void OnMouseDown ()
	{
		if (correctFace) {
			Debug.Log ("Correct Face! You won!");
		} else {
			Debug.Log ("Wrong Face! You lost!");
		}
	}
}
