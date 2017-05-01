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

	private Animator animator;

	private void Awake(){
		health.Initialize ();
		animator = GetComponent<Animator> ();
	}

	void Update(){
		if (health.CurrentVal <= 0) {
			death ();
		}
		if (gameObject.transform.position.y < -10) {
			death ();
		}

	}

	void applyDamage(int damage){
		health.CurrentVal -= damage;
		StartCoroutine (Wait ());
	}

	IEnumerator Wait(){
		animator.SetBool ("hurt", true);
		yield return new WaitForSeconds (1.5f);
		animator.SetBool ("hurt", false);
	}

	void OnCollisionEnter2D(Collision2D collision){

		if (collision.gameObject.tag == "Health") {

			if(health.CurrentVal < health.MaxVal)
				Destroy (collision.gameObject);

			health.CurrentVal += 10;

		}
	}
	void death(){
		SceneManager.LoadScene (deathScene);
	}

}
