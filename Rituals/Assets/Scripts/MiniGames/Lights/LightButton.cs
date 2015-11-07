using UnityEngine;
using System.Collections;

namespace MiniGames.Lights
{
	public class LightButton : MonoBehaviour
	{
		public LightsLayout layout;
		public Position2d position;

		void OnMouseDown ()
		{
			layout.click (position);
		}
	}
}