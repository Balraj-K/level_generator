using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// public PlayerController to attach it to the player
	public PlayerController thePlayer;

	// position of playerController
	private Vector3 lastPlayerPosition;
	// to control how much the camera should move
	private float distanceToMove;

	// Use this for initialization
	void Start()
	{
		thePlayer = FindObjectOfType<PlayerController>();
	}

	// Update is called once per frame
	void Update()
	{
		// the amount the camera should move
		distanceToMove = thePlayer.transform.position.x - lastPlayerPosition.x;

		// move the camera by the distance to move
		// this says the transform of the current object that THIS specific script is attached to.
		transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);

		// is equal to our playerController object
		lastPlayerPosition = thePlayer.transform.position;
	}
}
