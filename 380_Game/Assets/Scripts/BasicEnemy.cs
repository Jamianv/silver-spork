using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : PhysicsObject {

	[SerializeField]
	private float maxSpeed = 5f;
	[SerializeField]
	private float attackRange = 1f;
	private float dist;

	private GameObject player;
	private Vector2 move;

	private SpriteRenderer spriteRenderer;
	private Animator animator;

	private EnemyHealth enemyHealth;

	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		enemyHealth = GetComponent<EnemyHealth> ();
	}

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		Rest ();
	}
		
	public void MoveToPlayer(){
		move = Vector2.zero;
		move.x = (this.transform.position.x - player.transform.position.x);
		dist = Vector2.Distance (this.transform.position, player.transform.position);
		Debug.Log (dist);
		move.Normalize ();
		ComputeVelocity ();
	}
		
	public void Rest(){
		move.x = 0;
		ComputeVelocity ();
	}

	protected override void ComputeVelocity(){

		Animations ();

		if (dist < attackRange) {
			move.x = 0;
			animator.SetBool ("attack", true);
		}
		animator.SetBool ("attack", false);

		if (enemyHealth.Health <= 0)
			move.x = 0;

		targetVelocity = move * maxSpeed;
	}

	private void Animations(){
		bool flipSprite = (spriteRenderer.flipX ? (move.x < 0.01f) : (move.x > 0.01f));

		if (flipSprite) {
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}

		animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);
	}
}
