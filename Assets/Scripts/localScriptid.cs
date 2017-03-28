using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class localScriptid : MonoBehaviour {

	public Text scoreText;
	private int kidsEaten;

	Animator anim;

	bool hidden = false;
	bool disguised = false; 

	public bool runLeft;
	public bool runRight;

	public bool hideStay;
	public bool disStay;

	public Vector3 pos;

	// Use this for initialization
	void Start () {
		kidsEaten = 0;
		UpdateEaten ();
		anim = GetComponent<Animator> ();
		anim.SetBool ("runRight", false);
		anim.SetBool ("runLeft", false);
		anim.SetBool ("disStay", false);
		anim.SetBool ("hideStay", false);
	}
	
	void Update () {
		if (Input.GetKey (KeyCode.A) && hidden==false){
			Move (-0.1f, 0);
			anim.SetBool ("runLeft", true);
			anim.SetBool ("runRight", false);
		}
		if (Input.GetKey (KeyCode.D) && hidden==false) {
			Move (0.1f, 0);
			anim.SetBool ("runLeft", false);
			anim.SetBool ("runRight", true);
		}
		if (Input.GetKey (KeyCode.W) && hidden==false) {
			disguised = true;
			anim.SetBool ("disStay", true);
			anim.SetBool ("hideStay", false);
			print ("disguised");
		}
		if (Input.GetKeyUp (KeyCode.W) && hidden==false) {
			anim.SetBool ("disStay", false);
		}
		if (Input.GetKeyUp (KeyCode.Space) && hidden==false) {
			anim.SetBool ("hideStay", false);
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
			kidsEaten = kidsEaten + 1;
			UpdateEaten ();
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
			anim.SetBool ("hideStay", true);
			anim.SetBool ("disStay", false);
		}

		if (Input.GetKeyUp (KeyCode.Space) && hidden == true) {
			hidden = false;
			anim.SetBool ("hideStay", false);
		}

		if (Input.GetKeyUp (KeyCode.W) && disguised == true) {
			disguised = false;
			anim.SetBool ("disStay", false);
		}
	}

	void UpdateEaten () {
		//add text and count it u goober
		scoreText.text = "Kids Eaten: " + kidsEaten;
	}
}
