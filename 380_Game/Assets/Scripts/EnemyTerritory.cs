using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTerritory : MonoBehaviour {


	private GameObject player;
	private bool playerInTerritory;

	private KnightEnemy knightEnemy;

	private BoxCollider2D moveTerritory;

	public bool PlayerInTerritory {
		get {
			return playerInTerritory;
		}
		set {
			playerInTerritory = value;
		}
	}

	void Awake(){
		knightEnemy = GetComponent<KnightEnemy> ();
		moveTerritory = GetComponent<BoxCollider2D> ();
	}
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerInTerritory = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (playerInTerritory == true)
			knightEnemy.MoveToPlayer ();
		if (playerInTerritory == false)
			knightEnemy.Rest ();
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject == player) {
			playerInTerritory = true;
			Debug.Log ("In the knight's area!");
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject == player) {
			playerInTerritory = false;
			Debug.Log ("Out of the knight's area!");
		}
	}
}
