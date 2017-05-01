using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour {

	[SerializeField]
	private int health;
	[SerializeField]
	private float deathLength = 1;

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

	private void applyDamage(int damage){
		health -= damage;
	}

	IEnumerator Despawn(){
		yield return new WaitForSeconds (deathLength);
		Destroy (this.gameObject);
	}
}
