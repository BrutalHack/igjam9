using UnityEngine;

namespace minigames.icesliding
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