﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : MonoBehaviour {

	private Vector2 direction;
	private GameObject player;
	public BoxCollider2D sword;
	private float attackTimer = 1f;

	void Awake(){
		//sword = GetComponent<BoxCollider2D> ();
		sword.enabled = false;
	}

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		//find where player is and adjust sword collider to face them
		direction = (Vector2)(player.transform.position - this.transform.position);
		sword.offset = new Vector2(direction.normalized.x*.125f, sword.offset.y);
	}

	void OnTriggerStay2D(Collider2D other){
		//if player is in range activate sword collider, if player is in
		//sword collider damage player at intervals
		if (other.gameObject.tag == "Player") {
			sword.enabled = true;
			if (sword.IsTouching (other))
				Attack ();
		}


	}
	//apply damage at regular intervals
	private void Attack(){
		attackTimer -= Time.deltaTime;
		if (attackTimer <= 0) {
			player.SendMessage ("applyDamage", 5);
			attackTimer = 1f;
		}
	}
	//if player leaves reange disable sword collider
	void OnTriggerExit2D(Collider2D other){
		sword.enabled = false;
	}


}
