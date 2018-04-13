using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;

	private AudioSource music; 

	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>(); //set AudioSource equal to the one in the scene

			music.clip = startClip;// default clip when starting
			music.loop = true;
			music.Play ();
		}
		
	}

	void OnLevelWasLoaded(int level){ //native Unity method for scen specific logic

		Debug.Log ("MusicPlayer: loaded level" +level);


		music.Stop (); //stop previous music

		if (level == 0) {
			music.clip = startClip;
		} else if (level == 1) {
			music.clip = gameClip;
		} else if (level == 2) {
			music.clip = endClip;
		}

		music.loop = true;
		music.Play (); //play appropriate music
	}
}
