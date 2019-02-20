﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball_Movement : MonoBehaviour
{
	public float speed;
	public float jump;
    public Text scoreText;
    public Text winText;
    public Text scoreText2;
    public Text winText2;

    private Rigidbody rb;
    private GameObject player;
    private int score;
    private int play;
    Vector3 angle;

    private bool grounded;
    private bool canDoubleJump;

	void Start ()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        score = 0;
        play = 1;
        angle = new Vector3(0, 1, 0);
        grounded = true;
        canDoubleJump = false;
        SetScoreText();
        winText.text = "";
        winText2.text = "";
    }

	void FixedUpdate ()
    {
		float moveHor = (Input.GetAxis("Horizontal"));
		float moveVert = (Input.GetAxis("Vertical"));
        if (Input.GetKey("1")) angle = new Vector3(0, 1, 0);
        if (Input.GetKey("2")) angle = new Vector3(0, -1, 0); ;
        Quaternion deltaRotation = Quaternion.Euler(angle * Time.deltaTime);
        Vector3 movement = new Vector3(moveHor,0.0f,moveVert);
        if(play == 1)
        {
            rb.MoveRotation(rb.rotation * deltaRotation);
            rb.AddForce(movement*speed);
        }
		
	}

	void Update ()
    {
	/*	if(Input.GetKeyDown <KeyCode.Space>){
			GetComponent<Rigidbody> ().AddForce(Vector3.up*2000);
		}
	*/
		if (Input.GetButtonDown("Jump"))
		{
            if (grounded)
            {
                //rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                //rb.AddForce(new Vector3(0,jump,0));
                jumpNew();
                grounded = false;
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                //rb.AddForce(Vector3.up * jump);
                jumpNew();
                canDoubleJump = false;
            }
		}
        else if (Input.GetKey("q"))
        {
            Destroy(player);
            play = 0;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            score = score + 5;
            SetScoreText();
        }
    }

    void OnCollisionEnter(UnityEngine.Collision other)
    {
        if (other.gameObject.CompareTag("EndPlat"))
        {
            winText.text = "Final Score: " + score.ToString();
            winText2.text = "Final Score: " + score.ToString();
            play = 0;
            //SetScoreText();
        }
        else if(other.gameObject.CompareTag("Hurdle"))
        {
            score = score - 2;
            SetScoreText();
        }
        else
        {
            grounded = true;
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        scoreText2.text = "Score: " + score.ToString();
    }

    void jumpNew()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(new Vector3(0, jump, 0));
    }

}
