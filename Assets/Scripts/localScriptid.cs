using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class localScriptid : MonoBehaviour {

	public Text scoreText;
	private int kidsEaten;

	Animator anim;

	ParticleSystem party;

	bool hidden = false; //hide mechanic
	bool disguised = false; //disguise mechanic

	public bool runLeft; //use to set anim state
	public bool runRight; //use to set anim state

	public bool hideStay; //use to set anim state
	public bool disStay; //use to set anim state

	AudioSource sound;

	public Vector3 pos;

	// Use this for initialization
	void Start () {
		kidsEaten = 0; //scoring shit
		UpdateEaten (); //more scores

		anim = GetComponent<Animator> (); //start with all animation states set to false = idle is base state
		anim.SetBool ("runRight", false);
		anim.SetBool ("runLeft", false);
		anim.SetBool ("disStay", false);
		anim.SetBool ("hideStay", false);

		sound = GetComponent<AudioSource> ();
	}

	//OK SO I FIGURED OUT THE ISSUE???? IF YOU STOP MOVING IT DOESNT SET ANYTHIG TO FALSE
	//btw disguise and hidden set themselves to false so pls do not worr,,, my son it is okay
	//im 12 ears old yoly fuck
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
			disguised = true; //if disguised is true, walkin' over a kid eats them. separate from disStay
			anim.SetBool ("disStay", true);
			anim.SetBool ("hideStay", false);
			print ("disguised");
		}
		if (Input.GetKeyUp (KeyCode.W) && hidden==false) {
			anim.SetBool ("disStay", false); //disengage the mcfucking disguise animation when no longer holdin down W
		}
		if (Input.GetKeyUp (KeyCode.Space) && hidden==false) {
			anim.SetBool ("hideStay", false); //disengage the mcfucking hide when no longer holding down spacebar
		}


	}

	//it's the move code great googly moogly
	void Move (float x, float y) {
		Vector3 pos = transform.position;
		pos.x += x;
		pos.y += y;
		transform.position = pos;
	}

	void OnTriggerEnter2D(Collider2D other) {
		//when disguised, player destroys child object upon contact and adds points to counter
		if (disguised == true && other.gameObject.tag == "Child") {
			sound.Play ();
			Destroy (other.gameObject);
			kidsEaten = kidsEaten + 1;
			UpdateEaten ();
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
			anim.SetBool ("disStay", false); //cannot hide and disguise at the same time
		}

		if (Input.GetKeyUp (KeyCode.Space) && hidden == true) {
			hidden = false; //disengages the hide STATE
		}

		if (Input.GetKeyUp (KeyCode.W) && disguised == true) {
			disguised = false; //disengages disguise STATE
		}
	}

	void UpdateEaten () {
		//add text and count score u goober
		scoreText.text = "Kids Eaten: " + kidsEaten;
	}
}
