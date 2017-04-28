using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : PhysicsObject {
	
	[SerializeField]
	private float maxspeed = 3.0f;

	//for movement
	private Vector2 move;

	//for animations
	private SpriteRenderer spriteRenderer;
	private Animator animator;

	private EnemyHealth enemyHealth;

	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		enemyHealth = GetComponent<EnemyHealth> ();
	}

	void Start () {
		move = Vector2.zero;
		move.x = -1;
	}
		
	protected override void ComputeVelocity(){

		bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));

		if (flipSprite) {
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}

		animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxspeed);

		if (enemyHealth.Health <= 0)
			move.x = 0;
		

		targetVelocity = move * maxspeed;
	}


	void OnCollisionEnter2D(Collision2D collision){

		if (collision.gameObject.tag == "Wall")
			move.x *= -1;
	}
}