using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenManager : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Backspace)) {
			SceneManager.LoadScene ("Start Screen");
		}

		if (Input.GetKey (KeyCode.Space)) {
			SceneManager.LoadScene ("Game Screen");
		}
	}
}
