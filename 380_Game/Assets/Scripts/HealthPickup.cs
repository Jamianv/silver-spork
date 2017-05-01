using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

	private GameObject player;
	private float currentHealth;
	private float maxHealth;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");

	}
	void Start(){
		currentHealth = player.GetComponent<PlayerHealth> ().Health.CurrentVal;
		maxHealth = player.GetComponent<PlayerHealth> ().Health.MaxVal;
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Player") {
			if (currentHealth < maxHealth) {
				collision.gameObject.SendMessage ("applyHealth", 20);
				Destroy (this.gameObject);
			}
		}
	}
}
