using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MainGame
{

	public class Rune : MonoBehaviour
	{

		public Image SymbolImage;
		public Transform StartParent;
		public Symbol Symbol;

		public void ResetPosition ()
		{
			this.gameObject.transform.SetParent (StartParent, false);
			this.gameObject.transform.localPosition = Vector3.zero;
		}
	}
}
