using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {

	public Text scoreText;
	private int score;

	// Use this for initialization
	void Start () {
		score = 0;
		UpdateScore ();
	}
	
	public void AddScore (int newScore) {
		score += newScore;
		UpdateScore ();
	}

	void UpdateScore () {
		scoreText.text = "Score: " + score;
	}
}
