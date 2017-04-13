using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player health mana. this is the script where all manipulations 
/// of player health and mana will happen
/// </summary>

public class PlayerHealthMana : MonoBehaviour {

	[SerializeField]
	private Stat health;
	[SerializeField]
	private Stat mana;

	private void Awake(){
		health.Initialize ();
		mana.Initialize ();
	}

	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			mana.CurrentVal -= 10;
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Enemy")
			health.CurrentVal -= 10;
		
	}

}
