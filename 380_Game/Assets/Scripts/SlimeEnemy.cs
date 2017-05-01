using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class SlimeEnemy : MonoBehaviour {
	
	[SerializeField]
	private float maxspeed = 3.0f;
	[SerializeField]
	private float speed = 3.0f;

	//for movement
	private Rigidbody2D rb2d;
	private Vector2 dir;
	private float changeTimer;
	private float jumpTimer;

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
		rb2d = GetComponent<Rigidbody2D> ();
		changeTimer = Random.Range (4, 10);
		jumpTimer = Random.Range (1, 10);
		dir = new Vector2 (Random.Range (-10, 10), 0);
	}

	//Note put this stuff in rest and the follow stuff from knight enemy in the move to player function//
	void FixedUpdate(){
		//add force if too slow
		if (rb2d.velocity.magnitude < maxspeed) {
			rb2d.AddForce (dir * speed);	
		}
		//remove speed if too fast
		if (rb2d.velocity.magnitude > maxspeed) {
			Vector2 normal = new Vector2 (rb2d.velocity.normalized.x * maxspeed, rb2d.velocity.y);
			rb2d.velocity = normal;
		}
		//if dead set speed to zero
		if (enemyHealth != null) {
			if (enemyHealth.Health <= 0)
				rb2d.velocity = new Vector2 (0, 0);
		}
		//change directions at random
		changeDirection ();
		//jump at random times
		Jump ();
	}

	void Update(){

		//flip sprite
		bool flipSprite = (spriteRenderer.flipX ? (rb2d.velocity.x > 0.01f) : (rb2d.velocity.x < 0.01f));

		if (flipSprite) {
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}

		//animate sprite
		animator.SetFloat ("velocityX", Mathf.Abs (rb2d.velocity.x) / maxspeed);
	}

	//jump at random time
	private void Jump(){
		jumpTimer -= Time.deltaTime;
		if (jumpTimer <= 0) {
			rb2d.velocity = new Vector2 (0, Random.Range(3,5));
			jumpTimer = Random.Range (1, 10);
		}
	}

	//change directions randomly
	private void changeDirection(){
		changeTimer -= Time.deltaTime;
		if (changeTimer <= 0) {
			dir = new Vector2 (Random.Range (-10, 10), 0);
			changeTimer = Random.Range(4,10);
		}
	}
}