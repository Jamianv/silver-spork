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

	private GameObject[] player;

	private EnemyHealth enemyhealth;

	void Awake(){
		enemyhealth = GetComponent<EnemyHealth> ();
	}

	void Start(){
		player = GameObject.FindGameObjectsWithTag ("Player");

		if (enemyhealth.Health > 0) {
			InvokeRepeating ("LaunchProjectile", 2f, shootSpeed);
		}
	}

	void FixedUpdate(){
		direction = (Vector2)(player[0].transform.position - this.transform.position);
		direction.Normalize ();
	}

	void LaunchProjectile(){
		GameObject bullet = (GameObject)Instantiate (bulletPrefab, transform.position + (Vector3)(direction * bulletDistance), Quaternion.identity);
		//add velocity to bullet
		bullet.GetComponent<Rigidbody2D> ().velocity = direction * speed;
	}
}
