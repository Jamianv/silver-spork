using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTerritory : MonoBehaviour {


	private GameObject player;
	private bool playerInTerritory;

	private KnightEnemy knightEnemy;

	public BoxCollider2D moveTerritory;
	public BoxCollider2D attackTerritory;

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
		attackTerritory = GetComponent<BoxCollider2D> ();
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

	void OnTriggerStay2D(Collider2D collider){
		if (collider.gameObject == player) {
			playerInTerritory = true;
			Debug.Log ("In the knight's area!");
		}/*
		if (collider == moveTerritory) {
			//knightEnemy.MoveToPlayer ();
			playerInTerritory = true;
		}/**/
	}

	void OnTriggerExit2D(Collider2D collider){
		if (collider.gameObject == player) {
			playerInTerritory = false;
			Debug.Log ("Out of the knight's area!");
		}
	}
}
