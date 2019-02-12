using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball_Movement : MonoBehaviour
{
	public float speed;
	public float jump;
    public Text scoreText;
    public Text winText;

	private Rigidbody rb;
    private int score;
    private int play;
    private bool grounded;
    private bool canDoubleJump;

	void Start ()
    {
		rb = GetComponent<Rigidbody>();
        score = 0;
        play = 1;
        grounded = true;
        canDoubleJump = false;
        SetScoreText();
        winText.text = "";
	}

	void FixedUpdate ()
    {
		float moveHor = (Input.GetAxis("Horizontal"));
		float moveVert = (Input.GetAxis("Vertical"));

		Vector3 movement = new Vector3(moveHor,0.0f,moveVert);
        if(play == 1)
        {
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
                rb.AddForce(Vector3.up * jump);
                grounded = false;
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                rb.AddForce(Vector3.up * jump);
                canDoubleJump = false;
            }
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            score = score + 1;
            SetScoreText();
        }
    }

    void OnCollisionEnter(UnityEngine.Collision other)
    {
        if (other.gameObject.CompareTag("EndPlat"))
        {
            winText.text = "You Win! \nFinal Score: " + score.ToString();
            play = 0;
            //SetScoreText();
        }
        else
        {
            grounded = true;
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

}
