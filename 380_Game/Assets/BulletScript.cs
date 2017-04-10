using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		    
	}

	void OnBecameInvisible(){
		Destroy(this.gameObject);
	}
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Floor") {
			Destroy (this.gameObject);
		}
		if (collision.gameObject.tag == "Wall") {
			Destroy (this.gameObject);
		}
		if (collision.gameObject.tag == "Enemy") {
			Destroy (collision.gameObject);
			Destroy (this.gameObject);
		}
	}
}
