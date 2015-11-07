using UnityEngine;

namespace MiniGames.IceSliding
{
	public class DirectionButton : MonoBehaviour
	{
		public IcePlayer parent;
		public DirectionEnum direction;

		void OnMouseDown ()
		{
			parent.Move (direction);
		}
	}
}