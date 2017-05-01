using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Player") {
			collision.gameObject.SendMessage ("increaseMana", 50);
			Destroy (this.gameObject);
		}
	}

}
