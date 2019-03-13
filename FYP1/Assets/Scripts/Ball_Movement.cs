﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.SceneManagement;

public class Ball_Movement : MonoBehaviour
{
	public float speed;
	public float jump;
    public Text scoreText;
    public Text winText;
    public Text scoreText2;
    public Text winText2;
    public Camera myCam;
    public Light myLight;

    private Rigidbody rb;
    //private Transform cam;
    private GameObject player;
    private int score;
    private int play;
    private Vector3 camDirection;
    Vector3 angle;

    private bool grounded;
    private bool canDoubleJump;
    private bool move;
    private bool paused;
    private bool shadowOn;

    void Start ()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        camDirection = myCam.transform.forward;
        //cam = Camera.main.transform;
        score = 0;
        play = 1;
        move = false;
        paused = false;
        angle = new Vector3(0, 1, 0);
        grounded = true;
        canDoubleJump = false;
        SetScoreText();
        setWinText("");
        shadowOn = true;
//        StartCoroutine("changeShadows");
    }

	void FixedUpdate ()
    {
		float moveHor = (Input.GetAxis("Horizontal"));
		float moveVert = (Input.GetAxis("Vertical"));
        if (Input.GetKey("1"))
        {
            Rot(1);
        }
        if (Input.GetKey("2"))
        {
            Rot(-1);
        }
        Quaternion deltaRotation = Quaternion.Euler(angle * Time.deltaTime);
        Vector3 movement = new Vector3(moveHor,0.0f,moveVert);
        //Vector3 movement2 = new Vector3(cam.TransformDirection);
        if(play == 1)
        {
            if(move)
            {
                rb.MoveRotation(rb.rotation * deltaRotation);
            }
            if (Input.GetKey(KeyCode.P))
            {
                camDirection = myCam.transform.forward;
                movement = camDirection * speed * Time.deltaTime;
                //rigidbody.AddForce(movement);
                rb.AddForce(movement);
                //rb.AddForce(cam.TransformDirection * speed);
                //rb.AddForce(Vector3.forward * speed);
                //transform.position += Vector3.forward * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.O))
            {
                camDirection = myCam.transform.forward*-1;
                movement = camDirection * speed * Time.deltaTime;
                //rigidbody.AddForce(movement);
                rb.AddForce(movement);
                //rb.AddForce(cam.TransformDirection * speed);
                //rb.AddForce(Vector3.forward * speed);
                //transform.position += Vector3.forward * Time.deltaTime * speed;
            }
            rb.AddForce(movement*speed);
            move = false;
        }

		
	}

	void Update ()
    {
		if (Input.GetButtonDown("Jump"))
		{
            if (grounded)
            {
                JumpNew();
                grounded = false;
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                //rb.AddForce(Vector3.up * jump);
                JumpNew();
                canDoubleJump = false;
            }
		}
        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKey("q"))
        {
            GameOver();
        }
        if (Input.GetKey("t"))
        {
            PauseGame();
        }
        if (Input.GetKey("n"))
        {
            StartCoroutine("changeShadows");
        }
        if (transform.position.y <= -4)
        {
            GameOver();
        }
	}

 /*   void LateUpdate()
    {
        if (Input.GetKey("n"))
        {
            changeShadows();
        }
    }*/

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
  //          winText.text = "Final Score: " + score.ToString();
   //         winText2.text = "Final Score: " + score.ToString();
            setWinText("Final Score: " + score.ToString());
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

    void setWinText(string message)
    {
        winText.text = message;
        winText2.text = message;
    }

    void JumpNew()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(new Vector3(0, jump, 0));
    }

    void Rot(int degrees)
    {
        move = true;
        angle = new Vector3(0, degrees, 0);
    }

    void GameOver()
    {
        Destroy(player);
        setWinText("Game Over");
        play = 0;
    }

    void PauseGame()
    {
        if (!paused)
        {
            Time.timeScale = 0;
            paused = true;
            setWinText("Game Paused");
        }
        else
        {
            setWinText("");
            Time.timeScale = 1;
            paused = false;
        }
    }

    IEnumerator changeShadows()
    {
        if (shadowOn)
        {
            myLight.shadows = LightShadows.None;
            shadowOn = false;
            setWinText("Shading Off");
            yield return new WaitForSeconds(1);
            setWinText("");
        }
        else
        {
            myLight.shadows = LightShadows.Soft;
            shadowOn = true;
            setWinText("Shading On");
            yield return new WaitForSeconds(1);
            setWinText("");
        }
    }

}
