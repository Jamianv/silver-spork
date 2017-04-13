﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

	//!!!ALL PUBLIC FIELDS SHOULD BE CHANGED TO PRIVATE AND SERIALIZED FOR TESTING PURPOSES!!!///
	public float maxspeed = 7;
	public float jumpTakeOffSpeed = 7;

	// Use this for initialization
	void Start () {
		
	}
	
	protected override void ComputeVelocity(){
		Vector2 move = Vector2.zero;

		move.x = Input.GetAxis ("Horizontal");

		if (Input.GetButtonDown ("Jump") && grounded) {
			velocity.y = jumpTakeOffSpeed;
		}
		else if(Input.GetButtonUp("Jump")){
			if (velocity.y > 0)
				velocity.y = velocity.y *  .5f;
		}

		targetVelocity = move * maxspeed;
	}
}