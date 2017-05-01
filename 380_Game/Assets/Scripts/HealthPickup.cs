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
		maxHealth = player.GetComponent<PlayerHealth> ().Health.MaxVal;
	}
	void Update(){
		currentHealth = player.GetComponent<PlayerHealth> ().Health.CurrentVal;
		//Debug.Log ("CurrentHealth: " + currentHealth);
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
