using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonMushroomScript : MonoBehaviour {

	private GameObject player;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject == player) {
			StartCoroutine (Hurt ());
		}
	}
	IEnumerator Hurt(){
		//player.gameObject.SendMessage ("applyDamage", 5);
		player.gameObject.SendMessage ("KnockBack",20);
		yield return new WaitForSeconds (1f);
	}
}
