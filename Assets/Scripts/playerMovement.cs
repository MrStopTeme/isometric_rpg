using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

	GameObject player;
	public float movementSpeed = 2f;

	void Start () {
		player = gameObject;

	}
	
	// Update is called once per frame
	void Update () {
		/*
		Vector2 moveDir;
		if (Input.GetButton (KeyCode.W) || Input.GetButton (KeyCode.UpArrow)) {
			if (Input.GetButton (KeyCode.D) || Input.GetButton (KeyCode.RightArrow))
				moveDir = new Vector2 (2, 1);
			else if (Input.GetButton (KeyCode.A) || Input.GetButton (KeyCode.LeftArrow))
				moveDir = new Vector2 (-2, 1);
			else
				moveDir = new Vector2 (0, 1);
		}
		*/


		Vector3 moveDir = new Vector3 (2*Input.GetAxis ("Horizontal"), Input.GetAxis("Vertical"), 0);
		moveDir *= movementSpeed;
		player.transform.position += moveDir * Time.deltaTime;

	}
}
