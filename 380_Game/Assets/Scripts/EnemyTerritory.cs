using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTerritory : MonoBehaviour {


	private GameObject player;
	private bool playerInTerritory;

	private KnightEnemy knightEnemy;
	private RedSlimeEnemy slimeEnemy;

	//private BoxCollider2D moveTerritory;

	void Awake(){
		slimeEnemy = GetComponent<RedSlimeEnemy> ();
		knightEnemy = GetComponent<KnightEnemy> ();
		//moveTerritory = GetComponent<BoxCollider2D> ();
	}
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerInTerritory = false;
	}
	
	// Update is called once per frame
	//you might want to modify this to utilize SendMessage()
	void FixedUpdate () {
		if (playerInTerritory == true) {
			Debug.Log ("PLayer on territory");
			if(knightEnemy!=null)
				knightEnemy.MoveToPlayer ();
			if(slimeEnemy!=null)
				slimeEnemy.MoveToPlayer ();
		}
		if (playerInTerritory == false) {
			if(knightEnemy!=null)
				knightEnemy.Rest ();
			if(slimeEnemy!=null)
				slimeEnemy.Rest ();
		}
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
