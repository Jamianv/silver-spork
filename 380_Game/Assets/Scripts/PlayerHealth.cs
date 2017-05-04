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

	//Sound
	public AudioClip hurtSound;
	private AudioSource source;
	[SerializeField]
	private float volLowRange = .01f;
	[SerializeField]
	private float volHighRange = .1f;

	public Stat Health {
		get {
			return health;
		}
	}

	private void Awake(){
		health.Initialize ();
		animator = GetComponent<Animator> ();
		source = GetComponent<AudioSource> ();
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
		source.PlayOneShot (hurtSound);
		health.CurrentVal -= damage;
		//TODO: hurt animation is buggy
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
