using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightEnemy : PhysicsObject {

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

	private Vector2 direction;

	[SerializeField]
	private GameObject swordPrefab;
	[SerializeField]
	private float instantiationTimer = 0;

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
		move.Normalize ();
		ComputeVelocity ();
	}
		
	public void Rest(){
		move.x = 0;
		ComputeVelocity ();
	}

	protected override void ComputeVelocity(){

		SpriteFlip ();

		//Running animation
		animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);

		//distance from player
		dist = Vector2.Distance (this.transform.position, player.transform.position);
		Debug.Log (dist);

		//if player is here then do all code for attacking
		if (dist < attackRange) {
			move.x = 0;
			animator.SetFloat ("attackDist", dist);
			CreatePrefab ();
		}
		animator.SetFloat ("attackDist", dist);

		//stop moving if dead
		if (enemyHealth.Health <= 0)
			move.x = 0;
		
		//calculate movement velocity
		targetVelocity = move * maxSpeed;
	}

	private void CreatePrefab(){
		instantiationTimer -= Time.deltaTime;
		if (instantiationTimer <= 0) {
			Instantiate (swordPrefab, transform.position, Quaternion.identity);
			instantiationTimer = 1f;
		}
	}

	private void SpriteFlip(){
		direction = (Vector2)(this.transform.position - player.transform.position);

		bool flipSprite = (spriteRenderer.flipX ? (direction.x < 0.01f) : (direction.x > 0.01f));

		if (flipSprite) {
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}
	}

}
