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

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		source = GetComponent<AudioSource> ();
	}

	void OnBecameInvisible(){
		Destroy(this.gameObject);
	}
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject == player) {
			Physics2D.IgnoreCollision (player.GetComponent<Collider2D>(), GetComponent<Collider2D> ());
		}
		if (collision.gameObject.tag == "Floor") {
			float vol = Random.Range (volLowRange, volHighRange);
			source.PlayOneShot (impactSound, vol);
			Destroy (this.gameObject, impactSound.length);
		}
		if (collision.gameObject.tag == "Wall") {
			Destroy (this.gameObject);
		}
		if (collision.gameObject.tag == "Enemy") {
			Destroy (this.gameObject);
			collision.gameObject.SendMessage ("applyDamage", damage);
		}
		if (collision.gameObject.tag == "Slime") {
			Destroy (this.gameObject);
			collision.gameObject.SendMessage ("applyDamage", damage);
		}
		if (collision.gameObject.tag == "EnemyBullet")
			Destroy (this.gameObject);
	}

	private void damageAmount(int damage){
		this.damage = damage;
	}
}
