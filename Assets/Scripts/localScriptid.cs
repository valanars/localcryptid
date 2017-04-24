using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class localScriptid : MonoBehaviour {

	public Text scoreText;
	private int kidsEaten;
    public ParticleSystem Cronchmeets; // this is just to define the particle system.

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

	public Camera cam;


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
		//cam.GetComponent<camScript> ();
	}
		
	//disguise and hidden set themselves to false so pls do not worr,,, my son it is okay
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
		if (Input.GetKeyDown (KeyCode.W) && hidden==false) {
			disguised = true; //if disguised is true, walkin' over a kid eats them. separate from disStay
			anim.SetBool ("disStay", true);
			anim.SetBool ("hideStay", false);
			print ("disguised");
		}
		if (Input.GetKeyUp (KeyCode.W) && hidden==false) {
			anim.SetBool ("disStay", false); //disengage the mcfucking disguise animation when no longer holdin down W
			disguised = false;
		}
		if (Input.GetKeyUp (KeyCode.Space) && hidden==false) {
			anim.SetBool ("hideStay", false); //disengage the mcfucking hide when no longer holding down spacebar
			hidden = false;
		}
		if (Input.GetKeyUp (KeyCode.A)) {
			anim.SetBool ("runLeft", false); //disengage the run left,,, u must c e a s e
		}
		if (Input.GetKeyUp (KeyCode.D)) {
			anim.SetBool ("runRight", false); //hush my child it is okay........ for run right has been disabled
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
            Instantiate(Cronchmeets, new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y), Quaternion.identity);
            // this code instantiates the particle effect (Cronchmeets is the name, and the particles spawn at the x and y coordinates of the child gameobject).
			sound.Play ();
			Destroy (other.gameObject);
			kidsEaten = kidsEaten + 1;
			UpdateEaten ();

			//reference script and engagE THE SHAKE
			camScript cs = cam.GetComponent<camScript> ();
			cs.Shake();
		}
		//player dies upon collision w/ game obj tagged adult (goes to death screen)
		if (other.gameObject.tag == "Adult" && !hidden) {
			SceneManager.LoadScene ("Death Screen");
		}
		//player reaches end, loads win screen
		if (other.gameObject.tag == "Finish" && kidsEaten < 11 && kidsEaten > 0)
			SceneManager.LoadScene ("Win Screen");

        if (other.gameObject.tag == "Finish" && kidsEaten == 0)
            SceneManager.LoadScene("Win Screen2");

        if (other.gameObject.tag == "Finish" && kidsEaten == 11)
            SceneManager.LoadScene("Win Screen3");
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

		if (disguised == true && cover.gameObject.tag == "Child") {
			Instantiate(Cronchmeets, new Vector3(cover.gameObject.transform.position.x, cover.gameObject.transform.position.y), Quaternion.identity);
			// this code instantiates the particle effect (Cronchmeets is the name, and the particles spawn at the x and y coordinates of the child gameobject).

			//reference script and engagE THE SHAKE
			camScript cs = cam.GetComponent<camScript> ();
			cs.Shake();


			sound.Play ();
			Destroy (cover.gameObject);
			kidsEaten = kidsEaten + 1;
			UpdateEaten ();
		}
	}

	void UpdateEaten () {
		//add text and count score u goober
		scoreText.text = "Kids Eaten: " + kidsEaten;
	}
}
