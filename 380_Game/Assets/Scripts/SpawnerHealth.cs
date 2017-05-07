using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHealth : MonoBehaviour {

	[SerializeField]
	private GameObject explosionPrefab;
	private GameObject explosion;
	[SerializeField]
	private int health = 100;

	void Update () {
		if (health <= 0) {
			explosion = Instantiate (explosionPrefab, transform.position, Quaternion.identity);
			//Instantiate (Destroyed Sprite)
			Destroy(transform.parent.gameObject);
			Destroy (explosion, 0.833f);
		}
	}
	private void applyDamage(int damage){
		health -= damage;
	}
}
