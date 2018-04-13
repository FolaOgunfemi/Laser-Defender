using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDiplay : MonoBehaviour {
	private Text highScoreText;

	// Use this for initialization
	void Start () {
		highScoreText = GetComponent<Text> ();

		if (ScoreDisplay.matchCounter > 0) {

			highScoreText.text = ("High Score: " + ScoreDisplay.highScore);
			//
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
