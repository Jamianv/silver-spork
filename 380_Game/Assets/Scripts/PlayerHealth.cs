using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Player health. this is the script where all manipulations 
/// of player health will happen
/// </summary>

public class PlayerHealth : MonoBehaviour {

	public LevelManager levelManager;

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

	private bool dead = false;



	public Stat Health {
		get {
			return health;
		}
	}

	private void Awake(){
		levelManager = FindObjectOfType<LevelManager> ();
		health.Initialize ();
		animator = GetComponent<Animator> ();
		source = GetComponent<AudioSource> ();
	}

	void Update(){
		if (health.CurrentVal <= 0) {
			if (!dead) {
				death ();
				dead = true;
			}
		}
		if (gameObject.transform.position.y < -10) {
			//death ();
			if (!dead) {
				death ();
				dead = true;
			}
		}
		if (dead) {
			health.CurrentVal = health.MaxVal;
			dead = false;
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
		yield return new WaitForSeconds (.167f);
		animator.SetBool ("hurt", false);
	}
	void death(){
		levelManager.RespawnPlayer ();
		//SceneManager.LoadScene (deathScene);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "deathfloor")
			death ();
	}

}
