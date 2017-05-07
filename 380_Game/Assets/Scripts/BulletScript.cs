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

	private Rigidbody2D rb2d;

	[SerializeField]
	private float volLowRange = .01f;
	[SerializeField]
	private float volHighRange = .1f;

	private Animator animator;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
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
		if (collision.gameObject.tag == "Enemy") {
			Explode ();
			collision.gameObject.SendMessage ("applyDamage", damage);
		}
		if (collision.gameObject.tag == "Slime") {
			Explode ();
			collision.gameObject.SendMessage ("applyDamage", damage);
		}
		if (collision.gameObject.tag == "HitBox") {
			Explode ();
			collision.gameObject.SendMessage ("applyDamage", damage);
		}
	}

	private void damageAmount(int damage){
		this.damage = damage;
	}

	private void Explode(){
		rb2d.velocity = Vector2.zero;
		animator.SetBool ("explode", true);
		float vol = Random.Range (volLowRange, volHighRange);
		source.PlayOneShot (impactSound, vol);
		Destroy (this.gameObject, 0.5f);
	}
}
