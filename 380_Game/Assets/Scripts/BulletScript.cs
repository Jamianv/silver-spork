using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	[SerializeField]
	private int damage = 10;
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");

	}

	void OnBecameInvisible(){
		Destroy(this.gameObject);
	}
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject == player) {
			Physics2D.IgnoreCollision (player.GetComponent<Collider2D>(), GetComponent<Collider2D> ());
		}
		if (collision.gameObject.tag == "Floor") {
			Destroy (this.gameObject);
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
