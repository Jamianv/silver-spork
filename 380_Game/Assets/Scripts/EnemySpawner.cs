using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	[SerializeField]
	private GameObject enemyPrefab;
	private GameObject enemy;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", 2f, 10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void Spawn(){
		enemy = Instantiate (enemyPrefab, transform.position, Quaternion.identity);
		enemy.AddComponent<Animator> ();
	}
}
