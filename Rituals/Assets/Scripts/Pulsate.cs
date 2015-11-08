using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace MainGame
{
	
	public class Pulsate : MonoBehaviour
	{
		public float speed;
		public float pulsateSize;

		SpriteRenderer spriteRenderer;
		Vector3 baseScale;

		// Use this for initialization
		void Start ()
		{
			spriteRenderer = GetComponent<SpriteRenderer> ();
			baseScale = transform.localScale;
		}
	
		// Update is called once per frame
		void Update ()
		{
			transform.localScale = baseScale +
			(Vector3.one * pulsateSize * (float)Math.Sin (Time.timeSinceLevelLoad * speed));
		}
	}
}