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

	public Stat Health {
		get {
			return health;
		}
	}

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
		StartCoroutine (HurtAnim ());
	}
	void applyHealth(int increase){
		health.CurrentVal += increase;
	}

	IEnumerator HurtAnim(){
		animator.SetBool ("hurt", true);
		yield return new WaitForSeconds (1.5f);
		animator.SetBool ("hurt", false);
	}
	void death(){
		SceneManager.LoadScene (deathScene);
	}

}
