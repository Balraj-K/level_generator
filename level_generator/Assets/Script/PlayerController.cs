﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Two Values for our player 
	// Speed and Force

	public float moveSpeed;
	public float jumpForce;

	public float speedMultiplier;
	public float speedIncreaseMilestone;
	private float speedMilestoneCount;

	// detect if player is on the ground
	public bool grounded;
	public bool onCeiling; // reverse gravity on ceiling

	public bool isUpsideDown;

	// LayerMask is the selection of layers available
	public LayerMask whatIsGround;
	public LayerMask whatIsCeiling;

	// rigid body object
	private Rigidbody2D myRigidBody;

	// GENERIC collider 
	private Collider2D myCollider;

	// accelerometer speed
	public float accelerationZ;

	// gyroscope rotation speed
	public float rotationSpeedX;

	// Use this for initialization
	void Start()
	{
		// searches for the Rigidbody Component on the character.
		myRigidBody = GetComponent<Rigidbody2D>();

		myCollider = GetComponent<Collider2D>();

		// set the screen orientation
		Screen.orientation = ScreenOrientation.LandscapeLeft;

		// always start on the ground
		isUpsideDown = false;

		// Disable screen rotation
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeRight = true;
		Screen.autorotateToLandscapeLeft = true;
		Screen.orientation = ScreenOrientation.AutoRotation;

		// enable gyroscope
		Input.gyro.enabled = true;

		speedMilestoneCount = speedIncreaseMilestone;
	}

	// Update is called once per frame
	void Update()
	{

		if (transform.position.x > speedMilestoneCount) {
			speedMilestoneCount += speedIncreaseMilestone;
			speedIncreaseMilestone += speedIncreaseMilestone * speedMultiplier;
			moveSpeed = moveSpeed + speedMultiplier;
		}

		// Sets the movement speed
		myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
		Screen.orientation = ScreenOrientation.AutoRotation;

		// checks if the jump button is pressed (screen is tapped)
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
		{
			if (grounded || onCeiling)
			{
				myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
			}
		}

		// add code for gyroscope
		// checks to see if the gyroscope is flicked up or down
		accelerationZ = Input.acceleration.z;
		//Debug.Log(accelerationZ);
		rotationSpeedX = Input.gyro.rotationRate.x;
		Debug.Log(rotationSpeedX);

		if ((!isUpsideDown && grounded) && /*(rotationSpeedX < -1.1 || rotationSpeedX > 1.1)*/ (Input.GetKeyDown("a")))
		{
			// change the gravity settings
			myRigidBody.gravityScale = -4;
			// change the jumpForce to be negative!
			jumpForce = -15;

			isUpsideDown = true;

		} else if ((isUpsideDown && grounded) && /*(rotationSpeedX < -1.1 || rotationSpeedX > 1.1)*/(Input.GetKeyDown("a")) )
		{
			// change gravity settings
			myRigidBody.gravityScale = 4;
			// adjust jumpForce
			jumpForce = 10;

			isUpsideDown = false;
		}

		// detects if the player is on the ground
		// checks if the layer is touching any of the other layers
		grounded = Physics2D.IsTouchingLayers(myCollider);

	}
}
