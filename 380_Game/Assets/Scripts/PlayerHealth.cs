using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Player health mana. this is the script where all manipulations 
/// of player health and mana will happen
/// </summary>

public class PlayerHealth : MonoBehaviour {

	[SerializeField]
	private Stat health;


	private void Awake(){
		health.Initialize ();
	}

	void Update(){
		if (health.CurrentVal <= 0) {
			SceneManager.LoadScene ("Level2");
		}
	}

	void OnCollisionEnter2D(Collision2D collision){

		if (collision.gameObject.tag == "Enemy")
			health.CurrentVal -= 10;

		if (collision.gameObject.tag == "Health") {

			if(health.CurrentVal < health.MaxVal)
				Destroy (collision.gameObject);

			health.CurrentVal += 10;

		}
		
	}

}
