using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BigSlimeHealth : MonoBehaviour {
	[SerializeField]
	private int health;
	[SerializeField]
	private GameObject slimePrefab;

	//Sound
	public AudioClip deathSound;
	public AudioClip hurtSound;
	private AudioSource source;
	public AudioMixerGroup mixer;
	[SerializeField]
	private float volLowRange = .01f;
	[SerializeField]
	private float volHighRange = .1f;
	private bool dead = false;

	void Update () {
		if (health <= 0) {
			if (!dead) {
				float vol = Random.Range (volLowRange, volHighRange);
				PlayClipAtPoint (deathSound, gameObject.transform.position, vol, 1);
				dead = true;
			}
			for (int i = 0; i < 2; i++) {
				
				Instantiate (slimePrefab, this.transform.position + new Vector3(i*.2f, 0, 0), Quaternion.identity);
			}
			Destroy (this.gameObject);
		}
	}

	private void applyDamage(int damage){
		float vol = Random.Range (volLowRange, volHighRange);
		PlayClipAtPoint (deathSound, gameObject.transform.position, vol, 1);
		health -= damage;
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
