using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Movement : MonoBehaviour
{
	private Rigidbody rb;
	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
		float moveHor = 2*(Input.GetAxis("Horizontal"));
		float moveVert = 2*(Input.GetAxis("Vertical"));

		Vector3 movement = new Vector3(moveHor,0.0f,moveVert);
		rb.AddForce(movement);
	}

}
