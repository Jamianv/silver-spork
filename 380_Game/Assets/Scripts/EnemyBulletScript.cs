using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour {
	[SerializeField]
	private int damage = 5;

	//Sound
	public AudioClip impactSound;
	private AudioSource source;

	private Rigidbody2D rb2d;

	[SerializeField]
	private float volLowRange = .01f;
	[SerializeField]
	private float volHighRange = .1f;

	private Animator animator;

	void Start(){
		source = GetComponent<AudioSource> ();
		animator = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void OnBecameInvisible(){
		Destroy(this.gameObject);
	}
	void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.tag == "Floor") {
			Explode ();
		}
		if (collision.gameObject.tag == "Wall") {
			Explode ();
		}
		if (collision.gameObject.tag == "Player") {
			Explode ();
			collision.gameObject.SendMessage ("applyDamage", damage);
		}
		if (collision.gameObject.tag == "Bullet")
			Explode ();
	}

	private void Explode(){
		rb2d.velocity = Vector2.zero;
		animator.SetBool ("explode", true);
		float vol = Random.Range (volLowRange, volHighRange);
		source.PlayOneShot (impactSound, vol);
		Destroy (this.gameObject,0.5f);
	}
}