using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class EnemyHealth : MonoBehaviour {

	[SerializeField]
	private int health;
	[SerializeField]
	private float deathLength = 1;
	private bool dead = false;

	private Animator animator;
	private Animator parentAnimator;

	//Sound
	public AudioClip deathSound;
	public AudioClip hurtSound;
	private AudioSource source;
	public AudioMixerGroup mixer;
	[SerializeField]
	private float volLowRange = .01f;
	[SerializeField]
	private float volHighRange = .1f;

	public int Health {
		get {
			return health;
		}
	}

	private void Awake(){
		animator = GetComponent<Animator> ();
		parentAnimator = GetComponentInParent<Animator> ();
		source = GetComponent<AudioSource> ();
	}

	private void Start(){
		if (animator != null) {
			animator.SetInteger ("Health", health);
		}
		if (parentAnimator != null) {
			parentAnimator.SetInteger ("Health", health);
		}
	}
		
	void Update () {
		if (animator != null) {
			animator.SetInteger ("Health", health);
		}
		if (parentAnimator != null) {
			parentAnimator.SetInteger ("Health", health);
		}
		if (health <= 0) {
			if (!dead) {
				float vol = Random.Range (volLowRange, volHighRange);
				PlayClipAtPoint (deathSound, gameObject.transform.position, vol, 1);
				dead = true;
			}
			StartCoroutine (Despawn ());
		}
	}

	private void applyDamage(int damage){
		float vol = Random.Range (volLowRange, volHighRange);
		PlayClipAtPoint (hurtSound, gameObject.transform.position, vol, 1);
		health -= damage;
	}

	IEnumerator Despawn(){
		yield return new WaitForSeconds (deathLength);
		Destroy (gameObject);
		if (transform.parent.gameObject != null) {
			Destroy (transform.parent.gameObject);
		}
	}

	GameObject PlayClipAtPoint(AudioClip clip, Vector3 position, float volume, float pitch){
		GameObject obj = new GameObject();
		obj.transform.position = position;
		obj.AddComponent<AudioSource>();
		obj.GetComponent<AudioSource> ().outputAudioMixerGroup = mixer;
		obj.GetComponent<AudioSource> ().pitch = pitch;
		obj.GetComponent<AudioSource>().PlayOneShot(clip, volume);
		Destroy (obj, clip.length / pitch);
		return obj;
	}

	/*IEnumerator DeathSound(){
		while (true) {
			if (dead) {
				source.pitch = 1;
				source.PlayOneShot (deathSound);
				yield return new WaitForSeconds (2f);
				dead = false;
			} else
				yield return null;
		}
	}*/
}
