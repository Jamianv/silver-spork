using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEnemyCollision : MonoBehaviour {
	private GameObject[] enemies;
	// Use this for initialization
	void Start () {
		
	}
	
	void Update(){
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		for (int i = 0; i < enemies.Length; i++) {
			Physics2D.IgnoreCollision (enemies [i].GetComponent<Collider2D> (), GetComponent<Collider2D> ());	
		}
	}
}
