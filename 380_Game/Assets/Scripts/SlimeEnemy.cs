using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour {
	
	[SerializeField]
	private float maxspeed = 3.0f;
	[SerializeField]
	private float speed = 3.0f;

	//for movement
	private Rigidbody2D rb2d;
	private Vector2 move;
	private Vector2 dir;
	private float changeTimer = Random.Range(4,10);
	private float jumpTimer = Random.Range(1,10);

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
		dir = new Vector2 (Random.Range (-10, 10), 0);
	}

	void FixedUpdate(){
		if (rb2d.velocity.magnitude < maxspeed) {
			rb2d.AddForce (dir * speed);	
		}
		if (rb2d.velocity.magnitude > maxspeed) {
			Vector2 normal = new Vector2 (rb2d.velocity.normalized.x * maxspeed, rb2d.velocity.y);
			rb2d.velocity = normal;
		}
		if (enemyHealth.Health <= 0)
			rb2d.velocity = new Vector2 (0, 0);
		changeDirection ();
		Jump ();
		move = rb2d.velocity;
	}

	void Update(){
		bool flipSprite = (spriteRenderer.flipX ? (rb2d.velocity.x > 0.01f) : (rb2d.velocity.x < 0.01f));

		if (flipSprite) {
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}

		animator.SetFloat ("velocityX", Mathf.Abs (rb2d.velocity.x) / maxspeed);
	}

	private void Jump(){
		jumpTimer -= Time.deltaTime;
		if (jumpTimer <= 0) {
			rb2d.velocity = new Vector2 (0, 3);
			jumpTimer = Random.Range (1, 10);
		}
	}

	private void changeDirection(){
		changeTimer -= Time.deltaTime;
		if (changeTimer <= 0) {
			dir = new Vector2 (Random.Range (-10, 10), 0);
			changeTimer = Random.Range(4,10);
		}
	}
}