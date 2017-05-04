using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///script for shooting bullets from player towards mouse position
///currently this just uses unity's physics engine, may be good to
///switch over to custom 2D physics system. If you create a new shooting
///script it can inherit this one and override the fire function to modify 
///shooting behaviour
///
public class Shooting : MonoBehaviour {
	
	//bulletPrefab is the bullet object
	//speed controls bullet speed
	//bulletDistance is how far away the bullet spawns from the player
	//burst adjusts how many bullets fire per click
	//burstTime adjusts how fast the burst of bullets is

	//Physics variables
	[SerializeField]
	private GameObject bulletPrefab;
	[SerializeField]
	private float baseSpeed = 5f;
	[SerializeField]
	private float minSpeed = 2f;
	private float speed;
	private Vector2 direction;
	private Vector3 worldMousePos;

	//Mana Variables
	private PlayerMana playerMana;
	[SerializeField]
	private float manaRegenRate = 1.0f;
	private bool isRegen = false;
	private float manaCost;
	[SerializeField]
	private float baseManaCost = 5f;
	[SerializeField]
	private int maxManaCost = 15;

	//Charging variables
	[SerializeField]
	private float chargeRate = 10f;
	private bool isFire = false;
	private bool isCharging = false;
	private float chargeDamage;
	[SerializeField]
	private float baseDamage = 5f;
	[SerializeField]
	private int maxDamage = 15;
	[SerializeField]
	private float distFromPlayer;

	//Sound
	public AudioClip shootSound;
	private AudioSource source;
	private float volLowRange = 0.5f;
	private float volHighRange = 1f;

	private void Awake(){
		source = GetComponent<AudioSource> ();
		playerMana = GetComponent<PlayerMana> ();
	}

	void Start () {
	
	}

	/*White flash animation fully charged*/

	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			if (playerMana.Mana.CurrentVal > 0) {
				isCharging = true;
				chargeDamage = baseDamage;
				manaCost = baseManaCost;
				speed = baseSpeed;
			}
		}
		if (isCharging == true) {
			if (chargeDamage <= maxDamage) {
				chargeDamage += (chargeRate * Time.deltaTime);
			}
			if (manaCost <= playerMana.Mana.CurrentVal) {
				if(manaCost <= maxManaCost)
					manaCost += (chargeRate * Time.deltaTime);
			}
			if (speed > minSpeed) {
				//Charge Shot
				speed -= chargeRate * Time.deltaTime * 0.5f;
				//Arrowshot
				//speed += chargeRate * Time.deltaTime * 0.5f;
			}
			Debug.Log ("speed: " + speed);
		}
		if (Input.GetMouseButtonUp (0)) {
			isFire = true;
			playerMana.gameObject.SendMessage ("decreaseMana", manaCost);
			//mana.CurrentVal -= (int)manaCost;
			manaCost = baseManaCost;
		}


		if (playerMana.Mana.CurrentVal != playerMana.Mana.MaxVal && !isRegen) {
			StartCoroutine (RegainManaOverTime ());
		}
	}

	void FixedUpdate () {

		GetMouseDirection ();

		//Instantiate bullet locally
		if(isFire){

			float vol = Random.Range (volLowRange, volHighRange);
			source.PlayOneShot (shootSound, vol);

			GameObject bullet = (GameObject)Instantiate (bulletPrefab, transform.position + (Vector3)(direction*distFromPlayer), Quaternion.identity);
			bullet.transform.LookAt (worldMousePos);
			//add velocity to bullet
			bullet.GetComponent<Rigidbody2D> ().velocity = direction * speed;
			Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>());
			//change size depending on charge
			bullet.transform.localScale *= chargeDamage*.3f;
			//sed damage to enemy
			bullet.gameObject.SendMessage ("damageAmount", (int)chargeDamage);
			//reset charge to zero
			chargeDamage = baseDamage;
			isCharging = false;
			isFire = false;
			speed = baseSpeed;

		}
	}

	private IEnumerator RegainManaOverTime(){
		isRegen = true;
		while (playerMana.Mana.CurrentVal < playerMana.Mana.MaxVal) {
			playerMana.gameObject.SendMessage("increaseMana", 5);
			yield return new WaitForSeconds (manaRegenRate);
		}
		isRegen = false;
	}

	private void GetMouseDirection ()
	{
		//where the mouse is pointing
		worldMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		//direction from player to mouse
		direction = (Vector2)(worldMousePos - transform.position);
		direction.Normalize ();

	}


}
