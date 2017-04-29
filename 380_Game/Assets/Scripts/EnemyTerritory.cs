using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTerritory : MonoBehaviour {


	private GameObject player;
	private bool playerInTerritory;

	private BasicEnemy basicEnemy;

	void Awake(){
		basicEnemy = GetComponent<BasicEnemy> ();
	}
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerInTerritory = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (playerInTerritory == true)
			basicEnemy.MoveToPlayer ();
		if (playerInTerritory == false)
			basicEnemy.Rest ();
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject == player) {
			//basicEnemy.Move = new Vector2(1,0);
			playerInTerritory = true;
			Debug.Log ("In the knight's area!");
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if (collider.gameObject == player) {
			//basicEnemy.Move = new Vector2(0,0);
			playerInTerritory = false;
			Debug.Log ("Out of the knight's area!");
		}
	}
}
