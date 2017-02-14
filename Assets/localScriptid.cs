using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class localScriptid : MonoBehaviour {

	bool hidden = false;
	bool disguised = false;  

	// Use this for initialization
	void Start () {

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

	void OnTriggerEnter2D(Collider2D other){
		//when disguised, player destroys child object upon contact
		if (disguised == true && other.gameObject.tag == "Child") {
			Destroy (other.gameObject);
		}
		//player can hide when touching game obj tagged hide & holding spacebar
		if (Input.GetKey(KeyCode.Space) && other.gameObject.tag == "Hide") {
			hidden = true;
			print ("hidden");
		}
		//player dies upon collision w/ game obj tagged adult (scene resets)
		if (other.gameObject.tag == "Adult") {
			SceneManager.LoadScene ("Test Scene");
		}
	}
}
