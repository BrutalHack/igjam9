using UnityEngine;

public class DirectionButton : MonoBehaviour
{
	IcePlayer parent;
	public DirectionEnum direction;

	void Awake ()
	{
		parent = this.transform.parent.GetComponentInParent<IcePlayer> ();
	}


	void OnMouseDown ()
	{
		parent.Move (direction);
	}
}
