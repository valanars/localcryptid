using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

	public float minX;
	public float maxX;

	public float moveSpeed;

	//[nameAnim.SetBool ("ParameterName", boolean that it relates to)]
	//TURN OFF HAS EXIT TIME
	//Add conditions

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPos = transform.position;
		Vector3 newScale = transform.localScale;
		newScale.x *= -1;

		currentPos.x += moveSpeed * Time.deltaTime;
		if (currentPos.x > maxX) {
			currentPos.x = maxX;
			moveSpeed = -moveSpeed;
			transform.localScale = newScale;
		} else if (currentPos.x < minX) {
			currentPos.x = minX;
			moveSpeed = -moveSpeed;
			transform.localScale = newScale;
		}

		transform.position = currentPos;
	}
}
