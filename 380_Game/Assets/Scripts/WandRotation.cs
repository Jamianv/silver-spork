using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandRotation : MonoBehaviour {

	private Vector3 mousePos;
	[SerializeField]
	private Transform target;
	private Vector3 wandPos;
	private float angle;

	void Update () {
		mousePos = Input.mousePosition;
		mousePos.z = 5.23f;
		wandPos = Camera.main.WorldToScreenPoint (target.position);
		mousePos.x = mousePos.x - wandPos.x;
		mousePos.y = mousePos.y - wandPos.y;
		angle = Mathf.Atan2 (mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle-45));
	}
}
