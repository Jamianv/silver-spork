using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	[SerializeField]
	private int health;

	private void Awake(){
	}

	void Update () {
		if (health <= 0)
			Destroy (this.gameObject);
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Bullet")
			health -= 10;
	}
}
