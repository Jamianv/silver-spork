using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script allows player to move closer and closer towards corner. Work on check to fix this!

public class FollowCam : MonoBehaviour {

	[SerializeField] Transform character;

	private Vector3 moveTemp;

	[SerializeField] float speed = 3;
	[SerializeField] float ycenter = 20;
	[SerializeField] float xDifference;
	[SerializeField] float yDifference;
	[SerializeField] Vector2 minimumBoundary;
	[SerializeField] Vector2 maximumBoundary;

	[SerializeField] float movementThreshold = 3;

	void FixedUpdate () {

		if (character.transform.position.x > transform.position.x) {
			xDifference = character.transform.position.x - transform.position.x;
		} else {
			xDifference = transform.position.x - character.transform.position.x;
		}

		if (character.transform.position.y > transform.position.y) {
			yDifference = character.transform.position.y - transform.position.y;
		} else {
			yDifference = transform.position.y - character.transform.position.y;
		}

		if (xDifference >= movementThreshold || yDifference >= movementThreshold) {
			moveTemp = character.transform.position;
			moveTemp.y += ycenter;
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
