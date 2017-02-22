using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class localScriptid : MonoBehaviour {

	public Text scoreText;
	private int score;

	bool hidden = false;
	bool disguised = false;  

	public Vector3 pos;

	// Use this for initialization
	void Start () {
		score = 0;
		UpdateScore ();
	}
	
	void Update () {
		if (Input.GetKey (KeyCode.A))
			Move (-0.1f, 0);
		if (Input.GetKey (KeyCode.D))
			Move (0.1f, 0);
		if (Input.GetKey (KeyCode.W)) {
			disguised = true;
			print ("disguised");
		}
	}

	void Move (float x, float y) {
		Vector3 pos = transform.position;
		pos.x += x;
		pos.y += y;
		transform.position = pos;
	}

	void OnTriggerEnter2D(Collider2D other) {
		//when disguised, player destroys child object upon contact and adds points to counter
		if (disguised == true && other.gameObject.tag == "Child") {
			Destroy (other.gameObject);
			score = score + 1;
			UpdateScore ();
		}
		//player dies upon collision w/ game obj tagged adult (scene resets)
		if (other.gameObject.tag == "Adult") {
			transform.position = pos;
			//SceneManager.LoadScene ("Test Scene");
		}
		if (other.gameObject.tag == "Checkpoint") {
			pos = other.transform.position;
		}
	}

	void OnTriggerStay2D(Collider2D cover) {
	//player can hide when touching game obj tagged hide & holding spacebar
		if (Input.GetKey (KeyCode.Space) && cover.gameObject.tag == "Hide") {
			hidden = true;
			print ("hidden");
		}
	}

	void UpdateScore () {
		//add text and count it u goober
		scoreText.text = "Score: " + score;
	}
}
