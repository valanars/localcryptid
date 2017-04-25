using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class honkIsGo : MonoBehaviour {

	AudioSource honk;

	// Use this for initialization
	void Start () {
		honk = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		//when disguised, player destroys child object upon contact and adds points to counter
		if (other.gameObject.tag == "Player") {
			honk.Play ();
		}
	}
}
