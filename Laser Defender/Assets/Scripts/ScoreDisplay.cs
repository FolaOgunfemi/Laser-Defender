using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {
	public static int matchCounter = 0;

	public static int highScore = 0;


	// Use this for initialization
	void Start () {
		Text scoreText = GetComponent<Text> ();
		scoreText.text = ScoreKeeper.score.ToString (); //grab Score from ScoreKeeper class and display in the "text" element in inspector of this gameObject

		if (highScore < ScoreKeeper.score) { //If the static high score is less than the current score, we will set them equal to eachother and reset the score(but not the high score)
			highScore = ScoreKeeper.score;
		}
		if (ScoreKeeper.score != 0) {
			matchCounter++;
			ScoreKeeper.Reset ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
