using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour {

	[SerializeField]
	private int health;

	private Animator animator;

	public int Health {
		get {
			return health;
		}
	}

	private void Awake(){
		animator = GetComponent<Animator> ();

	}

	private void Start(){
		animator.SetInteger ("Health", health);
	}

	void Update () {
		if (health <= 0) {
			animator.SetInteger ("Health", health);
			StartCoroutine (Despawn ());
		}
	}

	IEnumerator Despawn(){
		yield return new WaitForSeconds (2);
		Destroy (this.gameObject);
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Bullet")
			health -= 10;
	}
}
