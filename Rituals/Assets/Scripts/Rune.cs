using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MainGame
{

	public class Rune : MonoBehaviour
	{

		public Image SymbolImage;
		public Transform StartPosition;

		public void ResetPosition ()
		{
			this.gameObject.transform.position = StartPosition.position;
		}
	}
}
