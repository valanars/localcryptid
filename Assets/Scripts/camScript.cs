using UnityEngine;
using System.Collections;

public class camScript : MonoBehaviour {

	public GameObject player;

	Vector3 initialPos;
	Vector3 weightedDirect;

	float shakeTimer = 0;
	float thisMagnitude = 0.5f;

	bool screenShaking = false;

	Vector2 playerPos;

	//NOTE TO SELF: YOU CAN CHANGE OFFSET IF CAMERA IS AWKWARD
	void Start () {
		initialPos = playerPos;
	}

	// Update is called once per frame
	void Update ()
	{
			playerPos = player.transform.position;
			transform.position = new Vector3 (playerPos.x, playerPos.y, -10);
	}

	public void Shake(){
		StartCoroutine ("Screenshaker");
	}

	public IEnumerator Screenshaker(){
		
		float time = .15f;

		//shake camera
		while (time > 0.0f) {
			Debug.Log (time);
			Camera.main.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -7.2f) + (Vector3)Random.insideUnitCircle + Vector3.back * -2.0f;
			time -= Time.deltaTime;
			yield return 0;
		}

		//return cam to normal pos
		Camera.main.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -2.0f);
	}
}
