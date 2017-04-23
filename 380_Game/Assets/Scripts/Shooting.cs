using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///script for shooting bullets from player towards mouse position
///currently this just uses unity's physics engine, may be good to
///switch over to custom 2D physics system. If you create a new shooting
///script it can inherit this one and override the fire function to modify 
///shooting behaviour
///

//!!!ALL PUBLIC FIELDS SHOULD BE CHANGED TO PRIVATE AND SERIALIZED FOR TESTING PURPOSES!!!///
public class Shooting : MonoBehaviour {

	//bulletPrefab is the bullet object
	//speed controls bullet speed
	//bulletDistance is how far away the bullet spawns from the player
	//burst adjusts how many bullets fire per click
	//burstTime adjusts how fast the burst of bullets is
	[SerializeField]
	private GameObject bulletPrefab;
	[SerializeField]
	private float speed = 5.0f;
	[SerializeField]
	private float bulletDistance = 0.5f;
    [SerializeField]
	private int burst = 1;
	[SerializeField]
	private float burstTime = 0.5f;
	[SerializeField]
	private Stat mana;
	[SerializeField]
	private float manaRegenRate = 1.0f;


	private Vector2 direction;
	private bool isFire = false;
	private bool isRegen = false;


	private void Awake(){
		mana.Initialize ();
	}

	void Start () {
		
	}
	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			if (mana.CurrentVal > 0) {
				isFire = true;
				mana.CurrentVal -= 10;
			}
		}
		if (Input.GetMouseButton (0)) {
			//Code for charge shot goes here
		}
		if (mana.CurrentVal != mana.MaxVal && !isRegen) {
			StartCoroutine (RegainManaOverTime ());
		}
	}
	private IEnumerator RegainManaOverTime(){
		isRegen = true;
		while (mana.CurrentVal < mana.MaxVal) {
			mana.CurrentVal += 5;
			yield return new WaitForSeconds (manaRegenRate);
		}
		isRegen = false;
	}

	// fixed Update is for physics manipulations
	void FixedUpdate () {

		//where the mouse is pointing
		Vector3 worldMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		//direction the bullet will go in
		direction = (Vector2)(worldMousePos - transform.position);
		direction.Normalize ();

		//Instantiate bullet locally
		if(isFire){
			fire ();
			isFire = false;
		}
	}


	//fire uses this coroutine so that it can get access to WaitForSeconds
	//which is used for burstfire
	void fire(){
		StartCoroutine (fireAsync());
	}

	IEnumerator fireAsync(){
		for (int i = 0; i < burst; i++) {
			GameObject bullet = (GameObject)Instantiate (bulletPrefab, transform.position + (Vector3)(direction * bulletDistance), Quaternion.identity);

			//add velocity to bullet
			bullet.GetComponent<Rigidbody2D> ().velocity = direction * speed;

			yield return new WaitForSeconds (burstTime); 
		}
	}

}
