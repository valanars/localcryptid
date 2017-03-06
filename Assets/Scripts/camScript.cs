using UnityEngine;
using System.Collections;

public class camScript : MonoBehaviour {

	public GameObject player;

	public Vector3 offset; //store offset dist b/w player and camera

	//NOTE TO SELF: YOU CAN CHANGE OFFSET IF CAMERA IS AWKWARD
	void Start ()
	{
		//offset = transform.position - player.transform.position;
	}

	//late called after Update each frame
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
