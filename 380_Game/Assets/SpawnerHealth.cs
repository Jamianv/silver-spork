using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHealth : MonoBehaviour {
	[SerializeField]
	private int health;

	void Start () {
		health = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			//Instantiate(explosion)
			//Instantiate (Destroyed Sprite)
			Destroy(transform.parent.gameObject);
		}
	}
	private void applyDamage(int damage){
		health -= damage;
	}
}
