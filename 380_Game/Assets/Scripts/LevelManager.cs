using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject currentCheckpoint;


	private GameObject player;
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RespawnPlayer(){
		Debug.Log ("Player respawn");
		player.transform.position = currentCheckpoint.transform.position;
	}
}
