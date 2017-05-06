using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {


	[SerializeField]
	private GameObject enemyPrefab;

	private GameObject player;
	private bool playerInTerritory;



	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerInTerritory = false;
		StartCoroutine(Spawn());

	}

	void FixedUpdate(){

	}

	void Update(){
		
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
