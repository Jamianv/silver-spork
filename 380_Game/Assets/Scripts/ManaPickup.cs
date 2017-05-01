using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : MonoBehaviour {

	private GameObject player;
	private float currentMana;
	private float maxMana;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Start(){
		maxMana = player.GetComponent<PlayerMana> ().Mana.MaxVal;
	}
	void Update(){
		currentMana = player.GetComponent<PlayerMana> ().Mana.CurrentVal;
		//Debug.Log ("Currentmana: " + currentMana);
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Player") {
			if (currentMana < maxMana) {
				collision.gameObject.SendMessage ("increaseMana", 50);
				Destroy (this.gameObject);
			}
		}
	}

}
