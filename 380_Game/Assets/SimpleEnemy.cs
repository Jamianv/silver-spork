using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : PhysicsObject {
	public float maxspeed = 3.0f;

	Vector2 move;
	// Use this for initialization
	void Start () {
		move = Vector2.zero;
		move.x = -1;
	}
	
	protected override void ComputeVelocity(){
		
		targetVelocity = move * maxspeed;
	}
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Wall")
			move.x *= -1;
		if (collision.gameObject.tag == "Player")
			Destroy (collision.gameObject);
	}
}
