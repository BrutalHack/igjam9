using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MainGame;

public class FadeText : MonoBehaviour
{
	public Text[] texts;
	public Color endColor;
	public Color startColor;
	public float FadeTime;
	public float currentTime = 0;
	public bool FirstPage = true;
	public bool fadeFirstPage = false;
	public int i = 0;

	void Awake ()
	{
		foreach (Text text in texts) {
			//text.color = new Color (startColor.r, startColor.g, startColor.g, startColor.a);
			text.color = startColor;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
			
		currentTime += Time.deltaTime / FadeTime;
		if (FirstPage && fadeFirstPage) {
			texts [0].color = Color.Lerp (endColor, startColor, currentTime);
			texts [1].color = Color.Lerp (endColor, startColor, currentTime);
			texts [2].color = Color.Lerp (endColor, startColor, currentTime);
			if (texts [2].color.a == 0) {
				FirstPage = false;
				currentTime = 0;
			}
		} else if (texts [i].color.a < 1) {
			texts [i].color = Color.Lerp (startColor, endColor, currentTime);
		} else {
			currentTime = 0;
			i++;
			if (i == 3 && FirstPage) {
				fadeFirstPage = true;
			}
			if (i == 5) {
				GameManager.Abort ();
			}
		}

	}
}
