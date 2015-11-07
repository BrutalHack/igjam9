using UnityEngine;

namespace MiniGames.MouseCheese
{
	public class CheeseButton : MonoBehaviour
	{
		public MouseLayout mouseLayout;
		public Position2d position;

		void OnMouseDown ()
		{
			mouseLayout.PutCheese (position, this.gameObject);
		}
	}
}