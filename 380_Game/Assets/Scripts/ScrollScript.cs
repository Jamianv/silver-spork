using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour {

	public float speed = 0;
	private GameObject mainCam;
	private Vector2 direction;

	void Awake(){
		mainCam = GameObject.FindGameObjectWithTag ("MainCamera");
		direction = Vector2.zero;
	}

	void Update () {
		if (mainCam.GetComponent<Rigidbody2D>().velocity.x > 0.01f) {
			direction.x = (this.transform.position.x - mainCam.transform.position.x);
		} if (mainCam.GetComponent<Rigidbody2D>().velocity.x < 0.01f) {
			direction.x = (mainCam.transform.position.x - this.transform.position.x);		}
		gameObject.GetComponent<Renderer> ().material.mainTextureOffset = (Vector2)(direction*speed);
	}
}
