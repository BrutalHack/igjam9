using UnityEngine;
using System.Collections;

namespace MainGame
{

	[RequireComponent (typeof(AudioSource))]
	public class MusicManager : MonoBehaviour
	{
		[SerializeField]
		private AudioClip[] PlayList;
		[SerializeField]
		private int nowPlaying;
		private AudioSource audioSource;

		void Awake ()
		{
			DontDestroyOnLoad (this.gameObject);
			audioSource = GetComponent <AudioSource> ();
			audioSource.loop = false;
		}

		void Update ()
		{
			if (!audioSource.isPlaying || Input.GetKeyDown (KeyCode.Space)) {
				nowPlaying++;	
				if (nowPlaying >= PlayList.Length) {
					nowPlaying = 0;
				}
				PlayTrack ();
			}
		}

		void PlayTrack ()
		{
			audioSource.clip = PlayList [nowPlaying];
			audioSource.Play ();
		}
	}
}