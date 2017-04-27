using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	[SerializeField]
	private int health;

	private Animator animator;

	private void Awake(){
		animator = GetComponent<Animator> ();
	}

	private void Start(){
		animator.SetInteger ("Health", health);
	}

	void Update () {
		if (health <= 0) {
			animator.SetInteger ("Health", health);
		    //new WaitForSeconds (2);
			//Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Bullet")
			health -= 10;
	}
}
