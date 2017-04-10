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
		Destroy (this.gameObject);
	}
}
