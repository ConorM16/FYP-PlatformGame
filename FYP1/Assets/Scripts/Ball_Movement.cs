using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Movement : MonoBehaviour
{
	public float speed;
	public float jump;

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
		float moveHor = (Input.GetAxis("Horizontal"));
		float moveVert = (Input.GetAxis("Vertical"));

		Vector3 movement = new Vector3(moveHor,0.0f,moveVert);
		rb.AddForce(movement*speed);
	}

	void Update () {
	/*	if(Input.GetKeyDown <KeyCode.Space>){
			GetComponent<Rigidbody> ().AddForce(Vector3.up*2000);
		}
	*/
		if (Input.GetButtonDown("Jump"))
		{
			rb.AddForce(Vector3.up * jump);
		}
	}

}
