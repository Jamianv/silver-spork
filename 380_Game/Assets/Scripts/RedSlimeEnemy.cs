using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSlimeEnemy : MonoBehaviour {

	[SerializeField]
	private float maxSpeed = 1f;
	[SerializeField]
	private float speed = 1f;
	[SerializeField]
	private float chaseSpeed = 5f;

	//for movement
	private GameObject player;
	private Rigidbody2D rb2d;
	private Vector2 dir;
	private Vector2 move;
	private float changeTimer;
	private float jumpTimer;

	//for animations
	private SpriteRenderer spriteRenderer;
	private Animator animator;

	private EnemyHealth enemyHealth;

	//Sound
	public AudioClip walkSound;
	private AudioSource source;
	[SerializeField]
	private float volLowRange = .01f;
	[SerializeField]
	private float volHighRange = .1f;
	[SerializeField]
	private float repeatRate = 1f;

	void Awake(){
		rb2d = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		enemyHealth = GetComponent<EnemyHealth> ();
		source = GetComponent<AudioSource> ();
	}

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");

		move = Vector2.zero;
		Rest ();


		changeTimer = Random.Range (4, 10);
		jumpTimer = Random.Range (1, 10);
		dir = new Vector2 (Random.Range (-10, 10), 0);
		StartCoroutine (SlimeWalkSound ());
	}

	//MoveToPlayer gets called in FixedUpdate of Enemyterritory 
	public void MoveToPlayer(){

		move.x = (player.transform.position.x-this.transform.position.x);
		move.Normalize ();

		SpriteFlip ();

		//Running animation
		animator.SetFloat ("velocityX", Mathf.Abs (rb2d.velocity.x) / maxSpeed);

		//move towards player
		if (rb2d.velocity.magnitude < maxSpeed) {
			rb2d.AddForce (move * chaseSpeed);
			Debug.Log ("Adding Speed!");
		}
		if (rb2d.velocity.magnitude > maxSpeed) {
			Vector2 normal = new Vector2 (rb2d.velocity.normalized.x * maxSpeed, rb2d.velocity.y);
			rb2d.velocity = normal;
			Debug.Log ("Reducing Speed");
		}

		//if dead set speed to zero
		if (enemyHealth != null) {
			if (enemyHealth.Health <= 0)
				rb2d.velocity = new Vector2 (0, 0);
		}
		//Jump ();

	}

	//Rest gets called in fixed update of EnemyTerritory
	public void Rest(){
		SpriteFlip ();
		//add force if too slow
		if (rb2d.velocity.magnitude < maxSpeed) {
			rb2d.AddForce (dir * speed);	
		}
		//remove speed if too fast
		if (rb2d.velocity.magnitude > maxSpeed) {
			Vector2 normal = new Vector2 (rb2d.velocity.normalized.x * maxSpeed, rb2d.velocity.y);
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
		return;
	}

	private void SpriteFlip(){
		dir = (Vector2)(this.transform.position - player.transform.position);

		bool flipSprite = (spriteRenderer.flipX ? (dir.x < 0.01f) : (dir.x > 0.01f));

		if (flipSprite) {
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}
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

	IEnumerator SlimeWalkSound(){
		while (true) {
			if (rb2d.velocity.x > 0.01 || rb2d.velocity.x < -0.01) {
				float vol = Random.Range (volLowRange, volHighRange);
				source.PlayOneShot (walkSound, vol);
				yield return new WaitForSeconds (repeatRate);
			} else
				yield return null;
		}
	}
}
