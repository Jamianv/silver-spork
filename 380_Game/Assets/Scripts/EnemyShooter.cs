using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour {

	[SerializeField]
	private GameObject bulletPrefab;

	[SerializeField]
	private float bulletDistance = 2f;

	[SerializeField]
	private float speed = 5f;

	[SerializeField]
	private float shootSpeed = 1;

	private Vector2 direction;

	private GameObject player;

	private EnemyHealth enemyhealth;

	//Sound
	public AudioClip shootSound;
	private AudioSource source;
	[SerializeField]
	private float volLowRange = 0.5f;
	[SerializeField]
	private float volHighRange = 1f;

	void Awake(){
		source = GetComponent<AudioSource> ();
		enemyhealth = GetComponent<EnemyHealth> ();
	}

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		if (enemyhealth.Health > 0) {
			InvokeRepeating ("LaunchProjectile", 2f, shootSpeed);
		}

	}

	void FixedUpdate(){
		direction = (Vector2)(player.transform.position - this.transform.position);
		direction.Normalize ();
	}

	void LaunchProjectile(){

		float vol = Random.Range (volLowRange, volHighRange);
		source.PlayOneShot (shootSound, vol);

		GameObject bullet = (GameObject)Instantiate (bulletPrefab, transform.position + (Vector3)(direction * bulletDistance), Quaternion.identity);
		//add velocity to bullet
		bullet.GetComponent<Rigidbody2D> ().velocity = direction * speed;
	}
}
