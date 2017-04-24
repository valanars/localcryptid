using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour {

	public GameObject player;


	//late called after Update each frame
	void LateUpdate () {
		transform.position = player.transform.position;
	}
}