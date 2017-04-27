using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

	[SerializeField]
	private float maxspeed = 7;
	[SerializeField]
	private float jumpTakeOffSpeed = 7;

	private SpriteRenderer spriteRenderer;
	private Animator animator;

	// Use this for initialization
	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
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

		bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
		if (flipSprite) {
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}

		animator.SetBool ("grounded", grounded);
		animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxspeed);

		targetVelocity = move * maxspeed;
	}
	private void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Enemy") {
			velocity.y = 10;
			/*Vector2 hit = Vector2.zero;
			hit.x = collision.rigidbody.velocity.x;
			targetVelocity = hit * 20;*/
		}
	}
}
