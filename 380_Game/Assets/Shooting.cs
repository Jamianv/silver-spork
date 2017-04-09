using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	//bulletprefab for bullet object
	//speed variable controls bullet speed
	//bullet distance is how far away the bullet spawns from the player
	public GameObject bulletPrefab;
	public float speed = 5.0f;
	public float bulletDistance = 0.5f;
	private Vector2 direction;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			//where the mouse is pointing
			Vector3 worldMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			//direction the bullet will go in
			direction = (Vector2)(worldMousePos - transform.position);
			direction.Normalize ();

			//fire bullet
			fire ();
		}
	}

	void fire(){
		//Instantiate bullet locally
		GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position + (Vector3)(direction * bulletDistance), Quaternion.identity);

		//add velocity to bullet
		bullet.GetComponent<Rigidbody2D> ().velocity = direction * speed;
	}

}
