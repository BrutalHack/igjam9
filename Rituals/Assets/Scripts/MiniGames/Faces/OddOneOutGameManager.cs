using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniGames.Faces;
using UnityEngine.Assertions;

namespace MiniGames.OddOneOut
{
	public class OddOneOutGameManager : MonoBehaviour
	{
		public int FaceFieldWidth;
		public int FaceFieldHeight;
		public GameObject FacePrefab;

		private List<Face> faces;

		// Use this for initialization
		void Start ()
		{
			CreateFaces ();
			CreateAnOddFace ();
		}

		void CreateFaces ()
		{
			faces = new List<Face> ();
			GameObject facePreset = Instantiate (FacePrefab, Vector3.zero, Quaternion.identity) as GameObject;
			Vector2 prefabSize = facePreset.GetComponent<BoxCollider2D> ().size;
			float prefabExtent = prefabSize.x / 2;
			Vector3 firstPosition = GetFirstPositionOffset (prefabExtent);
			Vector3 nextPosition = firstPosition;
			facePreset.GetComponent<Face> ().Randomize ();
			for (int y = 0; y < FaceFieldHeight; y++) {
				for (int x = 0; x < FaceFieldWidth; x++) {
					GameObject newFaceObject = Instantiate (facePreset, nextPosition, Quaternion.identity) as GameObject;
					newFaceObject.AddComponent<OddOneOutButton> ();
					Face newFace = newFaceObject.GetComponent<Face> ();
					faces.Add (newFace);
					nextPosition += new Vector3 (prefabExtent * 2, 0.0f);
				}
				nextPosition = new Vector3 (firstPosition.x, nextPosition.y - prefabExtent * 2);
			}
			Destroy (facePreset);
		}

		void CreateAnOddFace ()
		{
			int randomFaceId = Random.Range (0, faces.Count);
			//Exclude hair, because it is too easy... :(
			int randomFacePart = Random.Range (1, 4);
			faces [randomFaceId].RandomizeFacePart (randomFacePart);
			faces [randomFaceId].GetComponent<OddOneOutButton> ().correctFace = true;
		}

		Vector3 GetFirstPositionOffset (float prefabExtent)
		{
			return new Vector3 (this.transform.position.x - (FaceFieldWidth - 1) * prefabExtent,
				this.transform.position.y + (FaceFieldHeight - 1) * prefabExtent);
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
	}
}