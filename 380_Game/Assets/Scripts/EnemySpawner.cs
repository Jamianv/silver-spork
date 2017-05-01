using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	[SerializeField]
	private GameObject enemyPrefab;
	private GameObject enemy;

	private GameObject player;
	private bool playerInTerritory;

	private BoxCollider2D spawnTerritory;

	//Spawn when player is within area
	void Awake(){
		//spawnTerritory = GetComponent<BoxCollider2D> ();
	}

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerInTerritory = false;
		//InvokeRepeating ("Spawn", 2f, 10);
		StartCoroutine(Spawn());
	}

	void FixedUpdate(){

	}
	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject == player) {
			playerInTerritory = true;
			Debug.Log ("In Spawn Range!");
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject == player) {
			playerInTerritory = false;
			Debug.Log ("Out of Spawn Range!");
		}
	}

	/*void Spawn(){
		Instantiate (enemyPrefab, transform.position, Quaternion.identity);
	}

	/**/
	IEnumerator Spawn(){
		while (true) {
			if (playerInTerritory == true) {
				yield return new WaitForSeconds (5f);
				Instantiate (enemyPrefab, transform.position, Quaternion.identity);

			}
			if (playerInTerritory == false) {
				yield return null;
			}
		}
	}/**/
}
