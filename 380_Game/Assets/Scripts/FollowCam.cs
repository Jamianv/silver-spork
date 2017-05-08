using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script allows player to move closer and closer towards corner. Work on check to fix this!

public class FollowCam : MonoBehaviour {

	public GameObject player;

	private Vector3 moveTemp;
	private Vector2 direction;

	[SerializeField] 
	private float initialSpeed = 3f;
	private float speed;
	private bool dead;
	[SerializeField] Vector2 minimumBoundary;
	[SerializeField] Vector2 maximumBoundary;

	[SerializeField] float movementThreshold = .3f;

	void Awake(){
		//player = GameObject.FindGameObjectWithTag ("Player");

	}
	void Start(){
		speed = initialSpeed;
	}
	void Update () {
		player = GameObject.FindGameObjectWithTag ("Player");

		if (player.transform.position.x > transform.position.x || player.transform.position.x > transform.position.x) {
			direction.x = (player.transform.position.x - this.transform.position.x);
		}if(player.transform.position.y > transform.position.y || player.transform.position.y > transform.position.y) {
			direction.y = (this.transform.position.y - player.transform.position.y);
		}
		direction.Normalize ();
		if (direction.x >= movementThreshold || direction.y >= movementThreshold) {
			moveTemp = player.transform.position;
			moveTemp.z = -1;
			transform.position = Vector3.MoveTowards (transform.position, moveTemp, speed * Time.fixedDeltaTime);
		}
		transform.position = new Vector3
			(
				Mathf.Max (transform.position.x, minimumBoundary.x),
				Mathf.Max (transform.position.y, minimumBoundary.y),
				transform.position.z
			);

			
	}
}
