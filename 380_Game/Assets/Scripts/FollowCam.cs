using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script allows player to move closer and closer towards corner. Work on check to fix this!

public class FollowCam : MonoBehaviour {

	private GameObject player;

	private Vector3 moveTemp;
	private Vector2 direction;

	[SerializeField] float speed = 3;
	[SerializeField] Vector2 minimumBoundary;
	[SerializeField] Vector2 maximumBoundary;

	[SerializeField] float movementThreshold = 3;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate () {

		if (player.transform.position.x > transform.position.x) {
			direction = (Vector2)(player.transform.position - this.transform.position);
		} else {
			direction = (Vector2)(this.transform.position - player.transform.position);
		}
	
		if (direction.x >= movementThreshold || direction.y >= movementThreshold) {
			moveTemp = player.transform.position;
			moveTemp.z = -1;
			transform.position = Vector3.MoveTowards (transform.position, moveTemp, speed * Time.fixedDeltaTime);
		}
		transform.position = new Vector3
			(
				Mathf.Clamp (transform.position.x, minimumBoundary.x, maximumBoundary.x),
				Mathf.Clamp (transform.position.y, minimumBoundary.y, maximumBoundary.y),
				transform.position.z
			);

			
	}
}
