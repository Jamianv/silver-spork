using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour {

	[SerializeField]
	private int health;
	[SerializeField]
	private float deathLength = 1;
	private bool dead = false;

	private Animator animator;

	//Sound
	public AudioClip deathSound;
	private AudioSource source;
	[SerializeField]
	private float volLowRange = .01f;
	[SerializeField]
	private float volHighRange = .1f;

	public int Health {
		get {
			return health;
		}
	}
		
	private void Awake(){
		animator = GetComponent<Animator> ();
		source = GetComponent<AudioSource> ();
	}

	private void Start(){
		animator.SetInteger ("Health", health);

		StartCoroutine (DeathSound ());
	}
		
	void Update () {
		animator.SetInteger ("Health", health);
		if (health <= 0) {
			dead = true;
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
	IEnumerator DeathSound(){
		while (true) {
			if (dead) {
				source.pitch = 1;
				source.PlayOneShot (deathSound);
				yield return new WaitForSeconds (2f);
				dead = false;
			} else
				yield return null;
		}
	}
}
