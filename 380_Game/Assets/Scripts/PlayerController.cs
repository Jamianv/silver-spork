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

	private Vector2 direction;
	private Vector2 move;

	//Sound
	public AudioClip walkSound;
	private AudioSource source;
	private float volLowRange = .5f;
	private float volHighRange = 1.0f;

	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		source = GetComponent<AudioSource> ();
	}

	void Start(){
		StartCoroutine (WalkSound ());
	}
	
	protected override void ComputeVelocity(){
		move = Vector2.zero;

		move.x = Input.GetAxis ("Horizontal");

		if (Input.GetButtonDown ("Jump") && grounded) {
			velocity.y = jumpTakeOffSpeed;
		}
		else if(Input.GetButtonUp("Jump")){
			if (velocity.y > 0)
				velocity.y = velocity.y *  .5f;
		}

		GetMouseDirection ();

		bool flipSprite = (spriteRenderer.flipX ? (direction.x > 0.01f) : (direction.x < 0.01f));
		if (flipSprite) {
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}

		animator.SetBool ("grounded", grounded);
		animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxspeed);

		targetVelocity = move * maxspeed;
	}

	IEnumerator WalkSound(){
		while (true) {
			if (move.x != 0 && grounded) {
				float vol = Random.Range (volLowRange, volHighRange);
				source.PlayOneShot (walkSound, vol);
				yield return new WaitForSeconds (.2f);
			} else
				yield return null;
		}
	}

	private void KnockBack(float jump){
		velocity.y = jump;
		move.x = velocity.x*-100;
	}

	private void GetMouseDirection ()
	{
		//where the mouse is pointing
		Vector3 worldMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		//direction from player to mouse
		direction = (Vector2)(worldMousePos - transform.position);
		direction.Normalize ();

	}

}
