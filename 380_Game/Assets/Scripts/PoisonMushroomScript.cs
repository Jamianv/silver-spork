using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonMushroomScript : MonoBehaviour {

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player")
			other.gameObject.SendMessage ("applyDamage", 5);
	}
}
