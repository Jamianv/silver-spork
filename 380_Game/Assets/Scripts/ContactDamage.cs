using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			other.gameObject.SendMessage ("applyDamage", 5);
			other.gameObject.SendMessage ("KnockBack", 3);
		}

	}
}
