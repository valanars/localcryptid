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
			print ("child collision");
		}
		//player dies upon collision w/ game obj tagged adult (goes to death screen)
		if (other.gameObject.tag == "Adult" && !hidden) {
			SceneManager.LoadScene ("Death Screen");
		}
		//player reaches end, loads win screen
		if (other.gameObject.tag == "Finish") {
			SceneManager.LoadScene ("Win Screen");
		}
	}

	void OnTriggerStay2D(Collider2D cover) {
	//player can hide when touching game obj tagged hide & holding spacebar
		if (Input.GetKey (KeyCode.Space) && cover.gameObject.tag == "Hide") {
			hidden = true;
		}

		if (Input.GetKeyUp (KeyCode.Space) && hidden == true) {
			hidden = false;
		}

		if (Input.GetKeyUp (KeyCode.W) && disguised == true) {
			disguised = false;
		}
	}

	void UpdateScore () {
		//add text and count it u goober
		scoreText.text = "Children Eaten: " + score;
	}
}
