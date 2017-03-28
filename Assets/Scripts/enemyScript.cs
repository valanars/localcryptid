using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

	public float minX;
	public float maxX;

	public float moveSpeed;

	// Update is called once per frame
	//Car moves back and forth b/w a min X coordinate and a max X coordinate. 
	void Update () {
		Vector3 currentPos = transform.position;
		Vector3 newScale = transform.localScale; //car sprite to flip 
		newScale.x *= -1;

		currentPos.x += moveSpeed * Time.deltaTime;
		if (currentPos.x > maxX) {
			currentPos.x = maxX;
			moveSpeed = -moveSpeed;
			transform.localScale = newScale; //car sprite flips
		} else if (currentPos.x < minX) {
			currentPos.x = minX;
			moveSpeed = -moveSpeed;
			transform.localScale = newScale; //car sprite flips
		}

		transform.position = currentPos;
	}
}
