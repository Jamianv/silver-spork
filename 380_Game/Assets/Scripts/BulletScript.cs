using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	[SerializeField]
	private int damage = 10;
	private GameObject player;

	//Sound

	public AudioClip impactSound;
	private AudioSource source;

	[SerializeField]
	private float volLowRange = .01f;
	[SerializeField]
	private float volHighRange = .1f;

	private Animator animator;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		source = GetComponent<AudioSource> ();
		animator = GetComponent<Animator> ();
	}

	void OnBecameInvisible(){
		Destroy(this.gameObject);
	}
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject == player) {
			Physics2D.IgnoreCollision (player.GetComponent<Collider2D>(), GetComponent<Collider2D> ());
		}
		if (collision.gameObject.tag == "Floor") {
			Explode ();
			Destroy (this.gameObject, 0.5f);
		}
		if (collision.gameObject.tag == "Wall") {
			Explode ();
			Destroy (this.gameObject, 0.5f);
		}
		if (collision.gameObject.tag == "Enemy") {
			Explode ();
			Destroy (this.gameObject, 0.5f);
			collision.gameObject.SendMessage ("applyDamage", damage);
		}
		if (collision.gameObject.tag == "Slime") {
			Explode ();
			Destroy (this.gameObject, 0.5f);
			collision.gameObject.SendMessage ("applyDamage", damage);
		}
	}

	private void damageAmount(int damage){
		this.damage = damage;
	}

	private void Explode(){
		animator.SetBool ("explode", true);
		float vol = Random.Range (volLowRange, volHighRange);
		source.PlayOneShot (impactSound, vol);
	}
}
