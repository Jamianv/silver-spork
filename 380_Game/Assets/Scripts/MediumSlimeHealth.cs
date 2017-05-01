using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumSlimeHealth : MonoBehaviour {
	[SerializeField]
	private int health;
	[SerializeField]
	private GameObject slimePrefab;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			
			for (int i = 0; i < 3; i++) {
				Instantiate (slimePrefab, this.transform.position + new Vector3(i*.2f, 0, 0), Quaternion.identity);
			}
			Destroy (this.gameObject);

		}
	}
	private void applyDamage(int damage){
		health -= damage;
	}

}
