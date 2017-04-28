using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Player health. this is the script where all manipulations 
/// of player health will happen
/// </summary>

public class PlayerHealth : MonoBehaviour {

	[SerializeField]
	private Stat health;
	[SerializeField]
	private string deathScene;

	private void Awake(){
		health.Initialize ();
	}

	void Update(){
		if (health.CurrentVal <= 0) {
			death ();
		}
		if (gameObject.transform.position.y < -10) {
			death ();
		}
	}

	void OnCollisionEnter2D(Collision2D collision){

		//if (collision.gameObject.tag == "Enemy")
			//health.CurrentVal -= 10;

		if (collision.gameObject.tag == "Health") {

			if(health.CurrentVal < health.MaxVal)
				Destroy (collision.gameObject);

			health.CurrentVal += 10;

		}
		if (collision.gameObject.tag == "EnemyBullet")
			health.CurrentVal -= 10;

		
	}
	void death(){
		SceneManager.LoadScene (deathScene);
	}

}
