using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //must be added to access UI elements like "Text"

public class ScoreKeeper : MonoBehaviour {
	public static int score = 0;
	private Text scoreText;


	// Use this for initialization
	void Start () {
		scoreText = GetComponent<Text> (); //grab Text UI element

	}

	public void scoreTracker(int points){
		Debug.Log ("Scored points");
		score += points;
		scoreText.text = score.ToString ();
	}

	public static void Reset(){
		score = 0;
		// scoreText.text = score.ToString (); //necessary if this method is not static
	}

	// Update is called once per frame
	void Update () {
		
	}
}
