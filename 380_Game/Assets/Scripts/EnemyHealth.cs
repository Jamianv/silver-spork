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

	private Rigidbody2D rb2dParent;
	private Rigidbody2D rb2d;
	public int Health {
		get {
			return health;
		}
	}

	private void Awake(){
		animator = GetComponent<Animator> ();
		parentAnimator = GetComponentInParent<Animator> ();
		source = GetComponent<AudioSource> ();
		rb2dParent = GetComponentInParent<Rigidbody2D> ();
		rb2d = GetComponent<Rigidbody2D> ();
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
			Despawn ();
		}
	}

	private void applyDamage(int damage){
		float vol = Random.Range (volLowRange, volHighRange);
		PlayClipAtPoint (hurtSound, gameObject.transform.position, vol, 1);
		health -= damage;
	}

	private void Despawn(){
		
		if (rb2d != null) {
			rb2d.velocity = Vector2.zero;
			Destroy (gameObject, .833f);
		}

		if (rb2dParent != null) {
			rb2dParent.velocity = Vector2.zero;
			Destroy (rb2dParent.transform.gameObject,0.833f);
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
}
