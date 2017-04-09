using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	

	public float speed = 2.0f;
	private Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();

	}
	
	// called every fixed framerate frame put physics rigidbody manipulations here
	void FixedUpdate () {
		

		//horizontal movement
		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector2 movement = new Vector2 (moveHorizontal, rb2d.velocity.y);
		rb2d.velocity = (movement * speed);
	}




}
