using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KnightEnemy : MonoBehaviour {

	//movement
	[SerializeField]
	private float maxSpeed = 5f;
	[SerializeField]
	private float speed = 3f;
	private GameObject player;
	private Vector2 move;
	private Vector2 direction;
	private Rigidbody2D rb2d;

	//Animation
	private SpriteRenderer spriteRenderer;
	private Animator animator;

	//health
	private EnemyHealth enemyHealth;

	private float dist;


	void Awake(){
		rb2d = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		enemyHealth = GetComponent<EnemyHealth> ();
	}

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");

		move = Vector2.zero;
		Rest ();
	}


	//MoveToPlayer gets called in FixedUpdate of Enemyterritory 
	public void MoveToPlayer(){
		
		move.x = (player.transform.position.x - this.transform.position.x);
		move.Normalize ();

		SpriteFlip ();

		//Running animation
		animator.SetFloat ("velocityX", Mathf.Abs (rb2d.velocity.x) / maxSpeed);

		//distance from player
		dist = Vector2.Distance (this.transform.position, player.transform.position);

		animator.SetFloat ("attackDist", dist);

		//stop moving if dead
		if (enemyHealth.Health <= 0)
			move.x = 0;

		if (rb2d.velocity.magnitude < maxSpeed) {
			rb2d.AddForce (move * speed);
			Debug.Log ("Adding Speed!");
		}
		if (rb2d.velocity.magnitude > maxSpeed) {
			Vector2 normal = new Vector2 (rb2d.velocity.normalized.x * maxSpeed, rb2d.velocity.y);
			rb2d.velocity = normal;
			Debug.Log ("Reducing Speed");
		}


	}

	//Rest gets called in fixed update of EnemyTerritory
	public void Rest(){
		animator.SetFloat ("velocityX", Mathf.Abs (rb2d.velocity.x) / maxSpeed);
		rb2d.velocity = new Vector2(0, rb2d.velocity.y);
	}

	private void SpriteFlip(){
		direction = (Vector2)(this.transform.position - player.transform.position);

		bool flipSprite = (spriteRenderer.flipX ? (direction.x < 0.01f) : (direction.x > 0.01f));

		if (flipSprite) {
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}
	}

}
